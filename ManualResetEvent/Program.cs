using ManualResetEvent manualResetEvent = new ManualResetEvent(false);

Console.WriteLine("press enter to release all threads");

for (int i = 1; i <= 3; i++)
{
    Thread thread = new Thread(work);
    thread.Name = $"Worker {i}";
    thread.Start();
}

Console.ReadLine();

manualResetEvent.Set();


void work()
{
    Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for signal");
    manualResetEvent.WaitOne();
    Thread.Sleep(2000);
    Console.WriteLine($"{Thread.CurrentThread.Name} is released");
}

