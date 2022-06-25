using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using OnlineStore.Monitoring;

namespace OnlineStore;

public abstract class RevenueMonitor
{
    protected abstract IEnumerable<PerformanceRecord> FetchRecords(DateTime date);

    private const string COSMOS_KEY =
        "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
    private const string COSMOS_URI = "https://localhost:8081";
    private const string DATABASE_ID = "OnlineStore";
    private const string PERFROMANCE_CONTAINER = "Perfromance";
    private const int WEEKS_AGO = -7;

    public async Task MonitorAllCustomers()
    {
        var dateTime = DateTime.Now;
        var lastHourRecords = FetchRecords(dateTime);
        foreach (var lastHourRecord in lastHourRecords)
        {
            await MonitorSingleCustomer(lastHourRecord, dateTime);
        }
    }

    private async Task MonitorSingleCustomer(PerformanceRecord lastHourRecord, DateTime dateTime)
    {
        var cosmosClient = new CosmosClient(COSMOS_URI, COSMOS_KEY);
        var container = cosmosClient.GetContainer(DATABASE_ID, PERFROMANCE_CONTAINER);
        var query = container.GetItemLinqQueryable<PerformanceRecord>();
        query
            .Where(record => record.CustomerId == lastHourRecord.CustomerId)
            .Where(record => record.Time == dateTime.AddDays(1 * WEEKS_AGO) 
                             || record.Time == dateTime.AddDays(2 * WEEKS_AGO) 
                             || record.Time == dateTime.AddDays(3 * WEEKS_AGO) 
                             || record.Time == dateTime.AddDays(4 * WEEKS_AGO));
        using FeedIterator<PerformanceRecord>
            recordsIterator = container.GetItemQueryIterator<PerformanceRecord>(query.ToQueryDefinition());
        var performanceRecords = new List<PerformanceRecord>();
        while (recordsIterator.HasMoreResults)
        {
            FeedResponse<PerformanceRecord> currentResultSet = await recordsIterator.ReadNextAsync();
            foreach (PerformanceRecord performanceRecord in currentResultSet)
            {
                performanceRecords.Add(performanceRecord);
            }
        }

        var averageTraffic = performanceRecords.Average(record => record.Traffic);
        var averageRevenue = performanceRecords.Average(record => record.Revenue);
        if (lastHourRecord.Revenue < averageRevenue / 2)
        {
            if (lastHourRecord.Traffic < averageTraffic / 2)
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("support@vix.com");
                message.To.Add(new MailAddress(lastHourRecord.CustomerEmail));
                message.Subject = "Alert: a significant drop in performance";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = $"Hello, you have a drop of more than 50% in traffic and Revenue";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; 
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("support@vix.com", "password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            else
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("support@vix.com");
                message.To.Add(new MailAddress("itziksaban@vix.com"));
                message.Subject = "Alert: a significant drop in performance";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = $"Hello, customer {lastHourRecord.CustomerId} has a drop in revenue, but not in traffic. This is probably a bug.";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("support@vix.com", "password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

            }
        }
    }
}