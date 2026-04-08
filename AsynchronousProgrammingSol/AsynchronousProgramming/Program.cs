namespace AsynchronousProgramming
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Task Started");
            await HelloWorld();
            Console.WriteLine("Task Finished");
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
    }
}
