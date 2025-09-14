using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\nNo journal entries found.\n");
            return;
        }

        Console.WriteLine("\n--- Journal Entries ---");
        foreach (Entry e in _entries)
        {
            e.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry e in _entries)
            {
                writer.WriteLine(e.GetSaveString());
            }
        }
        Console.WriteLine($"Journal saved to {filename}\n");
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine($"No file found with name {filename}.\n");
            return;
        }

        _entries.Clear();
        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            Entry entry = Entry.FromSaveString(line);
            if (entry != null)
            {
                _entries.Add(entry);
            }
        }
        Console.WriteLine($"Journal loaded from {filename}\n");
    }
}
