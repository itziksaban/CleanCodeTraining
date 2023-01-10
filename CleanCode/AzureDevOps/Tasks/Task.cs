using System;

namespace AzureDevOps.Tasks;

public class Task
{
    public TaskStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public int Estimate { get; set; }
    public int FeatureId { get; set; }
    public string Name { get; set; }
}
