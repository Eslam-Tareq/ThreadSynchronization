namespace SignalingWithAutoResetEvent
{
    internal class Program
    {
        static AutoResetEvent autoResetEvent = new(false);
        static void Main(string[] args)
        {

            for (int i = 0; i < 3; i++)
            {
                Thread thread = new Thread(Worker);
                thread.Name = $"Worker {i + 1} ";
                thread.Start();
            }
            string? input;
            Console.WriteLine("server is running enter go to proceed");
            while (true)
            {
                input = Console.ReadLine();
                if (input == "go")
                {
                    autoResetEvent.Set();
                }
            }
        }
        static void Worker()
        {
            while (true)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for signal to be set");

                autoResetEvent.WaitOne();

                Console.WriteLine($"{Thread.CurrentThread.Name} is proceeded");
                Thread.Sleep(2000);

            }
        }
    }

}
