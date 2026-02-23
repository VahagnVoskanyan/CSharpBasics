namespace CSharpBasics.AsyncOperations
{
    internal class LockEx
    {
        static int totalValues = 0;
        private readonly object _LockT = new();
        private readonly ReaderWriterLockSlim _lock = new();
        private Dictionary<int, string> _cache = [];
        private readonly object _LockTM = new();
        private readonly object _LockTMW = new();
        private readonly Mutex _Mutex = new();

        public void Run()
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

            TestReaderWriterLock();

            Console.ReadLine();

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

        public void TestReaderWriterLock()
        {
            int writerCount = 5;
            int readerCount = 20;
            int operationsPerWriter = 100;

            var tasks = new List<Task>();

            // Barrier forces simultaneous start
            var barrier = new Barrier(writerCount + readerCount);

            // Writers
            for (int w = 0; w < writerCount; w++)
            {
                int localW = w;
                tasks.Add(Task.Run(() =>
                {
                    barrier.SignalAndWait(); // All threads start together

                    for (int i = 0; i < operationsPerWriter; i++)
                    {
                        ValueLockW(i, localW.ToString());
                    }
                }));
            }

            // Readers
            for (int r = 0; r < readerCount; r++)
            {
                tasks.Add(Task.Run(() =>
                {
                    barrier.SignalAndWait();

                    for (int i = 0; i < operationsPerWriter; i++)
                    {
                        ValueLockR(i);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            ValidateResults(operationsPerWriter);
        }

        private void IncreaseValue()
        {
            Console.WriteLine("Increasing Value on thread: " + Environment.CurrentManagedThreadId);
            for (int index = 0; index < 100000; index++)
            {
                // critical section
                totalValues = totalValues + 1;
            }
        }

        // lock
        private void IncreaseValueLock()
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

        private void ValueLockW(int key, string value)
        {
            bool lockAcquired = false;
            try
            {
                // During the write operation other threads (readers or writers)
                // are blocked.
                _lock.EnterWriteLock();
                lockAcquired = true;
                _cache[key] = value;
            }
            finally
            {
                if (lockAcquired)
                    _lock.ExitWriteLock();
            }
        }

        private string? ValueLockR(int key)
        {
            bool lockAcquired = false;
            try
            {
                _lock.EnterReadLock();
                lockAcquired = true;
                string? result = _cache.TryGetValue(key, out var value) ?
                        value : null;

                return result;
            }
            finally
            {
                if (lockAcquired)
                    _lock.ExitReadLock();
            }
        }

        private void ValidateResults(int expectedKeys)
        {
            _lock.EnterReadLock();
            try
            {
                if (_cache.Count != expectedKeys)
                    Console.WriteLine("!! Race condition detected! Missing keys.");

                Console.WriteLine("SUCCESS - No race condition detected.");
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        // Monitor
        private void IncreaseValueMonitor()
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
        private  void IncreaseValueMonitorWait()
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
        private void IncreaseValueMutex()
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
