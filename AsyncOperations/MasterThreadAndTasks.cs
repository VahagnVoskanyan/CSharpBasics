using System;

namespace CSharpBasics.AsyncOperations
{
    public static class MasterThreadAndTasks
    {
        public static void Run()
        {
            Console.WriteLine("Before");

            Console.WriteLine("Thread check 1: " + Environment.CurrentManagedThreadId);

            var task = new Task(() =>
            {
                Console.WriteLine("Thread check 2: " + Environment.CurrentManagedThreadId);
                Console.WriteLine("Adding Number Start");
                var result = AddNumbers(3, 4);
                Console.WriteLine("Adding Number Finish");
            });
            // Schedules the task on the ThreadPool. Can on thread from threadPool
            task.Start();

            Console.WriteLine("After");

            Console.WriteLine("Thread check 3: " + Environment.CurrentManagedThreadId);

            //Task.WaitAny(task);
            task.Wait(); // Wait for the task to complete

            Console.WriteLine("Thread check 4: " + Environment.CurrentManagedThreadId);

            Console.ReadLine(); // Wait 
        }

        static int AddNumbers(int a, int b)
        {
            // This method adds two numbers synchronously.
            Console.WriteLine("Adding numbers on thread: " + Environment.CurrentManagedThreadId);
            return a + b;
        }

        static int totalValues = 0;
        private static readonly object _LockT = new();
        private static readonly object _LockTM = new();
        private static readonly object _LockTMW = new();
        private static readonly Mutex _Mutex = new();

        public static void Run1()
        {
            Thread thread_1 = new(IncreaseValue);
            Thread thread_2 = new(IncreaseValue);

            thread_1.Start();
            thread_2.Start();

            // blocks the calling thread 
            thread_1.Join();
            thread_2.Join();

            Console.WriteLine($"Final total values is: {totalValues}");

            totalValues = 0;
            thread_1 = new(IncreaseValueLock);
            thread_2 = new(IncreaseValueLock);

            thread_1.Start();
            thread_2.Start();

            // blocks the calling thread 
            thread_1.Join();
            thread_2.Join();

            Console.WriteLine($"Final total values with 'lock' is: {totalValues}");

            totalValues = 0;
            thread_1 = new(IncreaseValueMonitor);
            thread_2 = new(IncreaseValueMonitor);

            thread_1.Start();
            thread_2.Start();

            // blocks the calling thread 
            thread_1.Join();
            thread_2.Join();

            Console.WriteLine($"Final total values with 'Monitor' is: {totalValues}");

            thread_1 = new(IncreaseValueMonitorWait);
            thread_2 = new(IncreaseValueMonitorWait);

            thread_1.Start();
            thread_2.Start();

            // blocks the calling thread 
            thread_1.Join();
            thread_2.Join();

            totalValues = 0;
            thread_1 = new(IncreaseValueMutex);
            thread_2 = new(IncreaseValueMutex);

            thread_1.Start();
            thread_2.Start();

            // blocks the calling thread 
            thread_1.Join();
            thread_2.Join();

            Console.WriteLine($"Final total values with 'Mutex' is: {totalValues}");
        }

        private static void IncreaseValue()
        {
            Console.WriteLine("Increasing Value on thread: " + Environment.CurrentManagedThreadId);
            for (int index = 0; index < 100000; index++)
            {
                // critical section
                totalValues = totalValues + 1;
            }
        }

        // lock
        private static void IncreaseValueLock()
        {
            Console.WriteLine("Increasing Value on thread: " + Environment.CurrentManagedThreadId);
            for (int index = 0; index < 100000; index++)
            {
                lock (_LockT)
                {
                    // critical section
                    totalValues = totalValues + 1;
                }
            }
        }

        // Monitor
        private static void IncreaseValueMonitor()
        {
            Console.WriteLine("Increasing Value on thread: " + Environment.CurrentManagedThreadId);
            for (int index = 0; index < 100000; index++)
            {
                // LockTaken Pattern.
                // What 'lock' does under the hood.
                bool lockTaken = false;
                try
                {
                    Monitor.Enter(_LockTM, ref lockTaken);
                    // critical section
                    totalValues = totalValues + 1;
                }
                finally
                {
                    if (lockTaken)
                        Monitor.Exit(_LockTM);
                }
            }
        }

        // Monitor with TryEnter
        private static void IncreaseValueMonitorWait()
        {
            if (Monitor.TryEnter(_LockTMW, 500))
            {
                try
                {
                    // critical section: 
                    Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId} is processing something.");

                    // simulate long running processing
                    Thread.Sleep(1000);

                    totalValues = totalValues + 1;
                }
                finally
                {
                    Monitor.Exit(_LockTMW);
                }
            }
            else
            {
                Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId} gives up to wait to enter critical section");
            }
        }

        // Mutex
        // 'Mutex' can be used for cross-process thread synchronization while 'lock' and 'Monitor'
        // can only be used for in-process thread synchronization.
        private static void IncreaseValueMutex()
        {
            Console.WriteLine("Increasing Value on thread: " + Environment.CurrentManagedThreadId);
            for (int index = 0; index < 100000; index++)
            {
                _Mutex.WaitOne();
                try
                {
                    // critical section
                    totalValues = totalValues + 1;
                }
                finally
                {
                    _Mutex.ReleaseMutex();
                }
            }
        }
    }
}
