using OnlineStore.Monitoring;
using System;
using System.Collections.Generic;
using System.IO;

namespace OnlineStore;

public class FileBasedRevenueMonitor : RevenueMonitor
{
    protected override IEnumerable<PerformanceRecord> FetchRecords(DateTime date)
    {
        var readLines = File.ReadLines($"{date.Year}/{date.Month}/{date.DayOfWeek}/{date.Hour}.txt");
        foreach (var line in readLines)
        {
            var lineValues = line.Split("|");
            yield return new PerformanceRecord
            {
                CustomerId = lineValues[0],
                Revenue = double.Parse(lineValues[1]),
                Traffic = int.Parse(lineValues[2])
            };
        }
    }
}