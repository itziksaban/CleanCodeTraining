using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AzureDevOps.Tasks;

public class TasksFunctions
{
    private TasksRepository _tasksRepository;

    public TasksFunctions()
    {
        _tasksRepository = new TasksRepository();
    }

    [FunctionName(nameof(GetTaskDeviation))]
    public async Task<IActionResult> GetTaskDeviation(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "{taskId}")]
        HttpRequest req,
        ILogger log, int taskId)
    {
        var task = _tasksRepository.GetTask(taskId);
        var taskDeviation = TaskDeviation(task.Status, task.Estimate, task.StartDate);
        return new OkObjectResult(taskDeviation);
    }

    public static int TaskDeviation(TaskStatus taskStatus, int taskEstimate, DateTime taskStartDate)
    {
        var dueDate = taskStartDate.AddDays(taskEstimate);
        var dueDateAlreadyPassed = dueDate < DateTime.Now;
        if (taskStatus == TaskStatus.Open && dueDateAlreadyPassed)
        {
            return (int) (DateTime.Now - dueDate).TotalDays;
        }

        return 0;
    }
}