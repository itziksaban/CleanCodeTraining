using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OnlineStore
{
    public static class MonitorFunction
    {
        [FunctionName(nameof(MonitorRevenue))]
        public static void MonitorRevenue([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var revenueMonitor = new FileBasedRevenueMonitor();
            revenueMonitor.MonitorAllCustomers();
        }
    }
}
