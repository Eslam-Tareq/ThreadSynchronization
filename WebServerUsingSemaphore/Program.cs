namespace WebServerUsingSemaphore

{
    internal class Program
    {
        static Queue<string?> requestQueue = new Queue<string?>();
        static SemaphoreSlim semaphore = new SemaphoreSlim(initialCount: 3, maxCount: 3);
        static Lock Lock = new Lock();
        static void Main(string[] args)
        {
            Thread thread = new Thread(() => Monitoring());
            thread.Start();

            Console.WriteLine("server is running. type  'exit' to stop");
            while (true)
            {
                string? input = Console.ReadLine();
                if (input?.ToLower() == "exit")
                {
                    break;
                }
                lock (Lock)
                {
                    requestQueue.Enqueue(input);
                }

            }
        }
        static void Monitoring()
        {
            while (true)
            {
                if (requestQueue.Count > 0)
                {
                    string? input;
                    lock (Lock)
                    {
                        input = requestQueue.Dequeue();
                    }
                    semaphore.Wait();
                    Thread thread = new Thread(() => ProcessInput(input));
                    thread.Start();
                }
                Thread.Sleep(100);

            }
        }
        static void ProcessInput(string? input)
        {
            try
            {
                Thread.Sleep(5000);
                Console.WriteLine($"\nyour input is {input}\n");
            }
            finally
            {
                var pervCount = semaphore.Release();
                Console.WriteLine($"thead with id {Thread.CurrentThread.ManagedThreadId} and the previous count ={pervCount} ");
            }

        }
    }
}
