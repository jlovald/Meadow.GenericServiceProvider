﻿using System.Threading;
using Meadow;

namespace MeadowApp;

internal class Program
{
    private static IApp app;

    public static void Main(string[] args)
    {
        // instantiate and run new meadow app
        app = new MeadowApp();

        Thread.Sleep(Timeout.Infinite);
    }
}