using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OnlineStore.Reservations
{
    public static class OrdersFunction
    {
        private static OrderCostsCalculator _orderCostsCalculator;

        static OrdersFunction()
        {
            _orderCostsCalculator = new OrderCostsCalculator(new ProductRepository(), new DistanceCalculator());
        }

        [FunctionName(nameof(GetDeliveryCost))]
        public static async Task<IActionResult> GetDeliveryCost(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = string.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }
            var order = JsonConvert.DeserializeObject<DeliverableOrder>(requestBody);
            if (order.OrderType == OrderType.SelfCollected)
            {
                return new BadRequestErrorMessageResult("Self collection does not have a delivery cost");
            }
            var cost = _orderCostsCalculator.CalcDeliveryCost(order);
            return new OkObjectResult(cost);
        }

        [FunctionName(nameof(TotalCost))]
        public static async Task<IActionResult> TotalCost(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = string.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }
            dynamic order = JsonConvert.DeserializeObject(requestBody);
            var cost = _orderCostsCalculator.CalcTotalCost(order);
            return new OkObjectResult(cost);
        }
    }
}
