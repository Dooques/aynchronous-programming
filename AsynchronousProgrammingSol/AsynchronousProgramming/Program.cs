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
                return "Hello...";
            });

            var world = Task.Run(async () =>
            {
                await Task.Delay(3000);
                return "...World.";
            });
           
            var combinedTasks = Task.WhenAll([hello, world]);
            await combinedTasks;
            Console.WriteLine(string.Join("", combinedTasks.Result));
        }
    }
}
