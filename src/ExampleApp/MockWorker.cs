using System;
using System.Threading;
using MeadowApp.Settings;
using Microsoft.Extensions.Options;

namespace MeadowApp;

public class MockWorker
{
    private readonly WorkerSettings workerSettings;

    public MockWorker(IOptions<WorkerSettings> workerSettings)
    {
        this.workerSettings = workerSettings.Value;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine(workerSettings.Message);
            Thread.Sleep(workerSettings.Delay);
        }
    }
}