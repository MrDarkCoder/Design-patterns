namespace design_pattern_c_._04Singleton
{
    internal class Singleton
    {
        void main()
        {
            //  MemoryCache.Create();


            #region sync problem
            /*
            int size = 8;

            Task[] tasks = new Task[size];

            for (int i = 0; i < size; i++)
            {
                tasks[i] = Task.Run(() => MemoryCache.Create());
            }

            Task.WaitAll(tasks);
            */
            #endregion

            #region sync acess problem
            int size = 10;
            Task[] tasks = new Task[size];
            for (int i = 0; i < size; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    var c = MemoryCache.Create();
                    if (c.AquireKey("job_id", "job1"))
                    {
                        Console.WriteLine("Big Operation");
                    }
                });
            }
            Task.WaitAll(tasks);
            #endregion
        }
    }

    //public class MemoryCache
    //{
    //    private static int i = 0;
    //    private static MemoryCache _instance;
    //    private static object _cacheLock = new object();

    //    private MemoryCache()
    //    {
    //        Console.WriteLine($"Created {i++}");
    //    }

    //    //public static MemoryCache Create()
    //    //{
    //    //    return _instance ?? (_instance = new MemoryCache());
    //    //}

    //    // using lock
    //    public static MemoryCache Create()
    //    {
    //        if (_instance == null)
    //        {
    //            lock (_cacheLock)
    //            {
    //                if (_instance == null)
    //                {
    //                    return _instance = new MemoryCache();
    //                }
    //            }
    //        }

    //        return _instance;
    //    }

    //}

    public class MemoryCache
    {
        private static MemoryCache cache;
        private static object cacheLock = new object();

        private readonly Dictionary<string, string> _registry;

        private MemoryCache() => _registry = new Dictionary<string, string>();

        public static MemoryCache Create()
        {
            if (cache == null)
            {
                lock (cacheLock)
                {
                    if (cache == null)
                    {
                        return cache = new MemoryCache();
                    }
                }
            }

            return cache;
        }

        public bool Contains(string key, string value)
        {
            return _registry.Contains(KeyValuePair.Create(key, value));
        }

        public void Write(string key, string value)
        {
            _registry[key] = value;
        }

        public bool AquireKey(string key, string value)
        {
            lock (cacheLock)
            {
                if (Contains("job_id", "job1"))
                {
                    return false;
                }

                Write("job_id", "job1");

                return true;
            }
        }
    }
}
