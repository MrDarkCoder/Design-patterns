
### Single responsiblitiy principle

> Any particular class should have just a single reason to change! 

Let say we are having a Journal class, which as job to store journal entries, add/remove

``` c#
//Journal.cs
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
}
```

Now, we may want to store these entries to differenct storage, like file system, or db.

Typically we tend to write it inside the journal class as new function SaveToFile() - 
but is this related to journal thing?, what if we may change the persistance to different format.

So, we need to create a separate class to persistance system.

``` c#
// Persistence.cs
public class Persistence
{
    public void SaveToFile(Journal journal, string filename, bool overWrtie = false)
    {
        if (overWrtie || !File.Exists(filename))
        {
            File.WriteAllText(filename, journal.ToString());
        }
    }
}
```


``` c#
// Program.cs!
var journal = new Journal();

journal.AddEntry("I cried today");
journal.AddEntry("I ate a bug");

Console.WriteLine(journal);

var persistence = new Persistence();
var filename = @"D:\journal.txt";

persistence.SaveToFile(journal, filename, true);
```