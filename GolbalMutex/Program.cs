namespace GlobalMutex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            using (var mutex = new Mutex(false, "GlobalMutex"))
            {

                for (int i = 0; i < 100; i++)
                {
                    mutex.WaitOne();
                    try
                    {
                        int counter = ReadCounter(filePath);
                        counter++;
                        WriteCounter(filePath, counter);
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }

                }
            }

            Console.WriteLine("Process finished.");
            Console.ReadLine();



        }
        static string filePath = "counter.txt";
        static int ReadCounter(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                return string.IsNullOrEmpty(content) ? 0 : int.Parse(content);
            }
        }

        static void WriteCounter(string filePath, int counter)
        {
            using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(counter);
            }
        }
    }
}

