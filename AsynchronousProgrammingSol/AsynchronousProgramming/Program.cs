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
            var rnd = new Random();
            //int randomnum1 = rnd.Next(1,11);
            //int randomnum2 = rnd.Next(1,11);

            var hello = Task.Run(async () =>
            {
                await Task.Delay(rnd.Next(1000, 10001));
                return "Hello...";
            });

            var world = Task.Run(async () =>
            {
                await Task.Delay(rnd.Next(1000, 10001));
                return "...World.";
            });
            try
            {
                CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken token = source.Token;

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var combinedTasks = Task.WhenAll([hello, world]);
                await combinedTasks;
                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds > 5000)
                {
                    source.Cancel();
                    throw new Exception("Too long over 5 seconds.");
                }
                Console.WriteLine(string.Join("", combinedTasks.Result));
            }
            catch ( Exception e) 
            {
                Console.WriteLine(e);
            }
        }
    }
}
