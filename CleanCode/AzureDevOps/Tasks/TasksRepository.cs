using System;
using System.Collections;
using System.Collections.Generic;

namespace AzureDevOps.Tasks;

internal class TasksRepository
{
    private Dictionary<int, List<MyTask>> _features;

    public TasksRepository()
    {
        _features = new Dictionary<int, List<MyTask>>
        {
            {
                7,
                new List<MyTask>
                {
                    new MyTask
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new MyTask
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new MyTask
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new MyTask
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new MyTask
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                }
            },
            {
                21,
                new List<MyTask>
                {
                    new MyTask
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new MyTask
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new MyTask
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new MyTask
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                    new MyTask
                    {
                        Status = TaskStatus.Open,
                        StartDate = new DateTime(2022, 5, 21),
                        Estimate = 7
                    },
                }
            },
        };
    }

    public MyTask GetTask(int taskId)
    {
        throw new NotImplementedException();
    }
}