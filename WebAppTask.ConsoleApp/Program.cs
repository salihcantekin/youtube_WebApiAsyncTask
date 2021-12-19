using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WebAppTask.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            do
            {
                AddLog("App is running...");

                Console.Write("Request Type (Sync = 0, Async = 1): ");
                int requestType = int.Parse(Console.ReadLine());

                Console.Write("How many request: ");
                int requestCount = int.Parse(Console.ReadLine());

                var sw = Stopwatch.StartNew();

                var tasks = requestType == 0 ? GetSyncTasks(requestCount) : GetAsyncTasks(requestCount);

                await Task.WhenAll(tasks);

                sw.Stop();

                AddLog($"Total Time: {sw.ElapsedMilliseconds} MS");

            } while (Console.ReadKey().Key == ConsoleKey.R);

        }

        public static IEnumerable<Task> GetSyncTasks(int howMany)
        {
            var result = new List<Task>();

            var client = new WebApiClient();

            for (int i = 0; i < howMany; i++)
                result.Add(client.CallSync());

            return result;
        }

        public static IEnumerable<Task> GetAsyncTasks(int howMany)
        {
            var result = new List<Task>();

            var client = new WebApiClient();

            for (int i = 0; i < howMany; i++)
                result.Add(client.CallAsync());

            return result;
        }

        private static void AddLog(string logStr)
        {
            logStr = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] - {logStr}";
            Console.WriteLine(logStr);
        }
    }
}
