using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureDevOps.Tasks;
using Microsoft.Azure.Cosmos.Linq;
using Task = AzureDevOps.Tasks.Task;

namespace Functions
{
    public class FeatureFunctions
    {
        private const string COSMOS_KEY = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private const string COSMOS_URI = "https://localhost:8081";
        private const string DATABASE_ID = "AzureDevOps";
        private const string TASK_CONTAINER = "Tasks";
        private const string FEATURE_CONTAINER = "Features";
        private FeatureRepository _featureRepository;
        private TasksRepository _tasksRepository;

        public FeatureFunctions()
        {
            _featureRepository = new FeatureRepository();
            _tasksRepository = new TasksRepository();
        }

        [FunctionName(nameof(CalcFeatureDeviation))]
        public async Task<IActionResult> CalcFeatureDeviation(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "{featureId}")] HttpRequest req,
            ILogger log, int featureId)
        {
            var cosmosClient = new CosmosClient(COSMOS_URI, COSMOS_KEY);
            var container = cosmosClient.GetContainer(DATABASE_ID, TASK_CONTAINER);
            var sqlQueryText = $"SELECT * FROM task WHERE task.featureId = { featureId }";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            using FeedIterator<Task> tasksIterator = container.GetItemQueryIterator<Task>(queryDefinition);
            var tasks = new List<Task>();
            while (tasksIterator.HasMoreResults)
            {
                FeedResponse<Task> currentResultSet = await tasksIterator.ReadNextAsync();
                foreach (Task task in currentResultSet)
                {
                    tasks.Add(task);
                }
            }

            int featureDeviation = 0;
            foreach (var task in tasks)
            {
                var taskDeviation = TasksFunctions.TaskDeviation(task.Status, task.Estimate, task.StartDate);
                featureDeviation += taskDeviation;
            }
            return new OkObjectResult(featureDeviation);
        }

        [FunctionName(nameof(GenerateTasksFile))]
        public async Task<IActionResult> GenerateTasksFile(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "GetTasks/{featureId}")] HttpRequest req,
            ILogger log, int featureId)
        {
            var tasksStreamer = new TasksStreamer();
            var cosmosClient = new CosmosClient(COSMOS_URI, COSMOS_KEY);
            var container = cosmosClient.GetContainer(DATABASE_ID, TASK_CONTAINER);
            var sqlQueryText = $"SELECT * FROM task WHERE task.featureId = { featureId }";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            using FeedIterator<Task> tasksIterator = container.GetItemQueryIterator<Task>(queryDefinition);
            var tasks = new List<Task>();
            while (tasksIterator.HasMoreResults)
            {
                FeedResponse<Task> currentResultSet = await tasksIterator.ReadNextAsync();
                foreach (Task task in currentResultSet)
                {
                    tasks.Add(task);
                }
            }

            tasksStreamer.OpenStream("all_tasks.txt");
            foreach (var task in tasks)
            {
                await tasksStreamer.WriteAsync(task);
            }

            tasksStreamer.FlushAsync();

            return new OkObjectResult("OK");
        }

        [FunctionName(nameof(GetFeaturesWithHighDeviation))]
        public async Task<IActionResult> GetFeaturesWithHighDeviation(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "{deviationThreshold}")] HttpRequest req,
            ILogger log, int deviationThreshold)
        {
            var cosmosClient = new CosmosClient(COSMOS_URI, COSMOS_KEY);
            var featuresContainer = cosmosClient.GetContainer(DATABASE_ID, FEATURE_CONTAINER);
            var featuresQueryText = $"SELECT * FROM feature WHERE feature.status = 'Open'";
            using FeedIterator<Feature> featuresIterator = featuresContainer.GetItemQueryIterator<Feature>(new QueryDefinition(featuresQueryText));
            var features = new List<Feature>();
            while (featuresIterator.HasMoreResults)
            {
                FeedResponse<Feature> currentResultSet = await featuresIterator.ReadNextAsync();
                foreach (Feature feature in currentResultSet)
                {
                    features.Add(feature);
                }
            }

            List<Feature> featuresWithHighDeviation = new List<Feature>();
            var tasksContainer = cosmosClient.GetContainer(DATABASE_ID, TASK_CONTAINER);
            foreach (var feature in features)
            {
                int featureDeviation = 0;
                var sqlQueryText = $"SELECT * FROM task WHERE task.featureId = { feature.Id }";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                using FeedIterator<Task> tasksIterator = tasksContainer.GetItemQueryIterator<Task>(queryDefinition);
                var tasks = new List<Task>();
                while (tasksIterator.HasMoreResults)
                {
                    FeedResponse<Task> currentResultSet = await tasksIterator.ReadNextAsync();
                    foreach (Task task in currentResultSet)
                    {
                        tasks.Add(task);
                    }
                }

                foreach (var task in tasks)
                {
                    var taskDeviation = TasksFunctions.TaskDeviation(task.Status, task.Estimate, task.StartDate);
                    featureDeviation += taskDeviation;
                }

                if (featureDeviation > deviationThreshold)
                {
                    featuresWithHighDeviation.Add(feature);
                }
            }

            return new OkObjectResult(featuresWithHighDeviation);
        }
    }
}
