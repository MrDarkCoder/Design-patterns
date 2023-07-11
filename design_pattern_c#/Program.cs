using design_pattern_c_._04Singleton;


#region sync problem

int size = 8;

Task[] tasks = new Task[size];

for (int i = 0; i < size; i++)
{
    tasks[i] = Task.Run(() => MemoryCache.Create());
}

Task.WaitAll(tasks);

#endregion