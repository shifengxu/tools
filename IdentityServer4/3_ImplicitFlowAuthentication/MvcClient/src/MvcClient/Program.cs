﻿using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MvcClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "MVC Client";

            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build().Run();
        }
    }
}