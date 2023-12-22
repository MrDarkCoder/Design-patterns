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
