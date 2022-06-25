using System;
using System.Collections;
using System.Collections.Generic;

namespace AzureDevOps.Tasks;

internal class TasksRepository
{
    private Dictionary<int, List<Task>> _features;

    public TasksRepository()
    {
        _features = new Dictionary<int, List<Task>>
        {
            {
                7,
                new List<Task>
                {
                    new Task
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new Task
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new Task
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new Task
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new Task
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                }
            },
            {
                21,
                new List<Task>
                {
                    new Task
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new Task
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new Task
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new Task
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new Task
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                }
            },
        };
    }

    public Task GetTask(int taskId)
    {
        throw new NotImplementedException();
    }
}