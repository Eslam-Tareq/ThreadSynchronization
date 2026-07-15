

namespace WebServerSeatBooking
{
    internal class Program
    {
        static Lock @lock = new Lock();
        static int avaliableTickeks=10;
        static Queue<string?> requestQueue = new Queue<string?>();
        //static void Main(string[] args)
        //{
        //    Thread thread = new Thread(() => Monitoring());
        //    thread.Start();

        //    Console.WriteLine("server is running. \r\n type b to book \r\n type b to cancel  type 'exit' to stop \r\n");
        //    while (true)
        //    {
        //        Console.Write("enter your message     ");
        //        string? input = Console.ReadLine();
        //        if (input?.ToLower() == "exit")
        //        {
        //            break;
        //        }
        //        requestQueue.Enqueue(input);

        //    }
        //}

        static void Main(string[] args)
        {
            Thread[] threads = new Thread[3];
            threads[0] = new Thread(() => ProcessBookingWithLock("b"));
            threads[1] = new Thread(() => ProcessBookingWithLock("b"));
            threads[2] = new Thread(() => ProcessBookingWithLock("b"));

            foreach (var item in threads)
            {
                item.Start();
            }
            foreach (var item in threads)
            {
                item.Join();
            }

            Console.WriteLine(avaliableTickeks);

        }
        static void Monitoring()
        {
            while (true)
            {
                if (requestQueue.Count > 0)
                {
                    string? input = requestQueue.Dequeue();
                    Thread thread = new Thread(() => ProcessBooking(input));
                    thread.Start();
                }
                Thread.Sleep(100);

            }
        }
        static void ProcessBooking(string? input)
        {
            Thread.Sleep(2000);
            lock (@lock)
            {
                if (input == "b")
                {
                    if (avaliableTickeks > 0)
                    {
                        avaliableTickeks--;
                        Console.WriteLine();
                        Console.WriteLine($"you have book a seat and still there are {avaliableTickeks} seats avaliable");
                    }
                }

                else if (input == "c")
                {

                    if (avaliableTickeks < 10)
                    {
                        avaliableTickeks++;
                        Console.WriteLine();
                        Console.WriteLine($"you have cancelled a seat and there are {avaliableTickeks} seats avaliable");
                    }
                }

            }
        }
        static void ProcessBookingWithLock(string? input)
        {
            Thread.Sleep(2000);
            
                if (input == "b")
                {
                    if (avaliableTickeks > 0)
                    {
                        avaliableTickeks--;
                        Console.WriteLine();
                        Console.WriteLine($"you have book a seat and still there are {avaliableTickeks} seats avaliable");
                    }
                }

                else if (input == "c")
                {

                    if (avaliableTickeks < 10)
                    {
                        avaliableTickeks++;
                        Console.WriteLine();
                        Console.WriteLine($"you have cancelled a seat and there are {avaliableTickeks} seats avaliable");
                    }
                }

            
        }
    }
}
