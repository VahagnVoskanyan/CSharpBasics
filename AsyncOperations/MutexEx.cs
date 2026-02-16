namespace CSharpBasics.AsyncOperations
{
    // 'Mutex' can be used for cross-process thread synchronization while 'lock' and 'Monitor'
    // can only be used for in-process thread synchronization.
    internal class MutexEx
    {
        public void Run()
        {
            Thread thread_1 = new(Ex1);
            Thread thread_2 = new(Ex1);

            thread_1.Start();
            thread_2.Start();

            // blocks the calling thread 
            thread_1.Join();
            thread_2.Join();

            Console.WriteLine($"Final total values is: {totalValues}");

            totalValues = 0;
            thread_1 = new(Ex2);
            thread_2 = new(Ex2);

            thread_1.Start();
            thread_2.Start();

            // blocks the calling thread 
            thread_1.Join();
            thread_2.Join();

            Console.WriteLine($"Final total values using 'Mutex' is: {totalValues}");
        }

        readonly string filePath1 = "totalValues1.txt";
        readonly string filePath2 = "totalValues2.txt";
        int totalValues = 0;

        private void Ex1()
        {
            for (int index = 0; index < 2000; index++)
            {
                // read from file
                using (var readFileStream =
                        new FileStream(filePath1, FileMode.OpenOrCreate,
                                FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(readFileStream))
                {
                    string fileContent = reader.ReadToEnd();
                    totalValues = string.IsNullOrEmpty(fileContent) ?
                                  0 : int.Parse(fileContent);
                }

                totalValues++;

                // write to file
                using (var writeFileStream = new FileStream(filePath1,
                          FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                using (var writer = new StreamWriter(writeFileStream))
                {
                    writer.Write(totalValues);
                }
            }
        }

        // Mutex
        private void Ex2()
        {
            using (var mutex = new Mutex(false, $"GlobalMutext:{filePath2}"))
            {
                for (int index = 0; index < 2000; index++)
                {
                    mutex.WaitOne();

                    try
                    {
                        // read from file
                        using (var readFileStream = new FileStream
                            (filePath2, FileMode.OpenOrCreate,
                              FileAccess.Read, FileShare.ReadWrite))
                        using (var reader = new StreamReader(readFileStream))
                        {
                            string fileContent = reader.ReadToEnd();
                            totalValues = string.IsNullOrEmpty(fileContent)
                                ? 0 : int.Parse(fileContent);
                        }

                        // increment the value
                        totalValues++;

                        // write to file
                        using (var writeFileStream = new FileStream(filePath2,
                                    FileMode.OpenOrCreate, FileAccess.Write,
                                      FileShare.ReadWrite))
                        using (var writer = new StreamWriter(writeFileStream))
                        {
                            writer.Write(totalValues);
                        }
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
            }
        }
    }
}
