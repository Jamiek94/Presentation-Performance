using System;
using Database.Models;

namespace Consumer
{
    class Program
    {
        static void Main()
        {
            var azureQueue = new AzureQueue(new PerformancePresentationContext());

            Console.WriteLine("Starting consumer app...");

            azureQueue.Start();

            Console.ReadLine();
        }
    }
}