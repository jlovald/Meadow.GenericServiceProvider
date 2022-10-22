using System;
using System.Threading;
using GenericServiceProvider;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;
using Meadow.Peripherals.Leds;
using MeadowApp.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace MeadowApp;

// Change F7FeatherV2 to F7FeatherV1 for V1.x boards
public class MeadowApp : App<F7FeatherV2, MeadowApp>
{
    private RgbPwmLed onboardLed;

    public MeadowApp()
    {
        var host = new GenericServiceProviderBuilder()
            .AddConfiguration("appsettings.json")
            .ConfigureServices((config, services) =>
            {
                services.Configure<WorkerSettings>(config.GetSection(nameof(WorkerSettings)));
                services.AddSingleton<MockWorker>();
            }).Build();
        var worker = host.GetRequiredService<MockWorker>();
        worker.Run();

        Initialize();
        CycleColors(TimeSpan.FromMilliseconds(1000));
    }

    private void Initialize()
    {
        Console.WriteLine("Initialize hardware...");

        onboardLed = new RgbPwmLed(Device,
            Device.Pins.OnboardLedRed,
            Device.Pins.OnboardLedGreen,
            Device.Pins.OnboardLedBlue,
            IRgbLed.CommonType.CommonAnode);
    }

    private void CycleColors(TimeSpan duration)
    {
        Console.WriteLine("Cycle colors...");

        while (true)
        {
            ShowColorPulse(Color.Blue, duration);
            ShowColorPulse(Color.Cyan, duration);
            ShowColorPulse(Color.Green, duration);
            ShowColorPulse(Color.GreenYellow, duration);
            ShowColorPulse(Color.Yellow, duration);
            ShowColorPulse(Color.Orange, duration);
            ShowColorPulse(Color.OrangeRed, duration);
            ShowColorPulse(Color.Red, duration);
            ShowColorPulse(Color.MediumVioletRed, duration);
            ShowColorPulse(Color.Purple, duration);
            ShowColorPulse(Color.Magenta, duration);
            ShowColorPulse(Color.Pink, duration);
        }
    }

    private void ShowColorPulse(Color color, TimeSpan duration)
    {
        onboardLed.StartPulse(color, duration / 2);
        Thread.Sleep(duration);
        onboardLed.Stop();
    }

    private void ShowColor(Color color, TimeSpan duration)
    {
        Console.WriteLine($"Color: {color}");
        onboardLed.SetColor(color);
        Thread.Sleep(duration);
        onboardLed.Stop();
    }
}