using System;

namespace AzureDevOps.Tasks;

public class Task
{
    public Task()
    {

    }
    public TaskStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public int Estimate { get; set; }
    public int FeatureId { get; set; }
    public string Name { get; set; }

    public int CalcDeviation()
    {
        int taskDeviation = 0;
        if (Status != TaskStatus.Closed)
        {
            var dueDate = StartDate.AddDays(Estimate);
            if (dueDate > DateTime.Now)
            {
                taskDeviation = (int)(dueDate - DateTime.Now).TotalDays;
            }
        }

        return taskDeviation;
    }
}