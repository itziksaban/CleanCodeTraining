using System;

namespace OnlineStore.Monitoring;

public class PerformanceRecord
{
    public string CustomerId { get; set; }
    public DateTime Time { get; set; }
    public int Traffic { get; set; }
    public double Revenue { get; set; }
    public string CustomerEmail { get; set; }
}