public class Journal
{
    private readonly List<string> entries = new();
    private static int count = 0;


    public int AddEntry(string entry)
    {
        entries.Add($"{++count}: {entry}");
        return count; // memento
    }

    public void RemoveEntry(int index)
    {
        entries.RemoveAt(index);
    }

    public override string ToString()
    {
        return String.Join(Environment.NewLine, entries);
    }

    // not good!!
    //public void Save(string filename)
    //{
    //    File.WriteAllText(filename, ToString());
    //}

    //public static Journal Load(string filename)
    //{

    //}

    //public void Load(Uri uri)
    //{

    //}
}
