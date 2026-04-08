using System.Diagnostics;

namespace AsynchronousProgramming
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Task Started");
            //await HelloWorld();
            //Console.WriteLine("Task Finished");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await HelloWorldDelay();
            stopwatch.Stop();
            Console.WriteLine($"This code executed in {stopwatch.ElapsedMilliseconds}ms");
        }

        static async Task HelloWorld()
        {
            var helloWorld = Task.Run(async () =>
            {
                await Task.Delay(2000);
                Console.WriteLine("Hello World.");
            });

            await helloWorld;
        }

        static async Task HelloWorldDelay() 
        {
            var hello = Task.Run(async () =>
            {
                await Task.Delay(3000);
                Console.WriteLine("Hello...");
            });

            var world = Task.Run(async () =>
            {
                await Task.Delay(3000);
                Console.WriteLine("...World.");
            });

            await hello;
            await world;
        }
    }
}
