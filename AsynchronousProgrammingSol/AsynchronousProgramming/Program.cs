using System.Diagnostics;
using System.Numerics;

namespace AsynchronousProgramming
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Console.WriteLine("Task Started");
            // await HelloWorld();
            // Console.WriteLine("Task Finished");
            // await HelloWorldDelay();

            string data = "85671 34262 92143 50984 24515 68356 77247 12348 56789 98760";
            string story = "Mary had a little lamb, its fleece was white as snow.";

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var factorialData = Task.Run(() => data.Split(' ').Select(n => BigInteger.Parse(n)).ToList());
            var getFactorial = await factorialData.ContinueWith(async factorial =>
                    await Task.Run(() => factorial.Result.Select(f =>
                    {
                        var factorialResult = Exercises.CalculateFactorial(f);
                        Console.WriteLine(factorialResult);
                        return factorialResult;
                    }).ToList()));

            var songWordList = Task.Run(() => story.Split(' ').ToList());

            var printSongWords = await songWordList.ContinueWith(async task =>
            {
                await Task.Run(() => task.Result.ForEach(w =>
                {
                    PrintWordWithDelay(w);
                }));
            });

            var list = Task.WhenAll([getFactorial, printSongWords]);
            await list;

            stopwatch.Stop();
            Console.WriteLine($"This code executed in {stopwatch.ElapsedMilliseconds}ms");

            // Accessing the secret file 
            string filePath = @"SuperSecretFile.txt";
            Console.WriteLine(AsyncFileManager.ReadFile(filePath));
        }

        static void PrintWordWithDelay(string word)
        {
            var executeBlock = Task.Run(async () =>
            {
                await Task.Delay(1000);
                Console.WriteLine(word);
            });

            executeBlock.Wait();
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
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
