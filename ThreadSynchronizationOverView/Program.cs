namespace ThreadSynchronizationOverView
{
    internal class Program
    {
        static int Counter = 0;
        static Lock _lock = new Lock();
        
        static void Main(string[] args)
        {
            Thread thread1 = new Thread(Increment);
            Thread thread2 = new Thread(Increment);

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();


            Console.WriteLine($"final counter value = {Counter}");

        }
        static void Increment()
        {
            for (int i = 0; i < 100000; i++)
            {
                lock (_lock)
                {
                    Counter += 1;
                }
            }
        }
    }
}
