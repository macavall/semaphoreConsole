using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static SemaphoreSlim semaphore = new SemaphoreSlim(2); // Semaphore to allow 2 tasks at a time

    static async Task Main(string[] args)
    {
        List<Task> tasks = new List<Task>();

        // Create 10 tasks
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Task.Run(async () =>
            {
                await ExecuteTask();
            }));
        }

        await Task.WhenAll(tasks);
        Console.WriteLine("All tasks completed.");
    }

    static async Task ExecuteTask()
    {
        await semaphore.WaitAsync();
        try
        {
            Console.WriteLine($"Task {Task.CurrentId} started.");
            // Simulate some work
            await Task.Delay(2000);
            Console.WriteLine($"Task {Task.CurrentId} completed.");
        }
        finally
        {
            semaphore.Release();
        }
    }
}
