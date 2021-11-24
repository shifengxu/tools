using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "API";

            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build().Run();
        }
    }
}