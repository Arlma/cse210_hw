using System;

public class Entry
{
    public string _date;
    public string _prompt;
    public string _response;

    public Entry(string prompt, string response)
    {
        _date = DateTime.Now.ToShortDateString();
        _prompt = prompt;
        _response = response;
    }

    public void Display()
    {
        Console.WriteLine($"Date: {_date} | Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}\n");
    }

    public string GetSaveString()
    {
        return $"{_date}|{_prompt}|{_response}";
    }

    public static Entry FromSaveString(string line)
    {
        string[] parts = line.Split('|');
        if (parts.Length == 3)
        {
            Entry entry = new Entry(parts[1], parts[2]);
            entry._date = parts[0]; // keep original date
            return entry;
        }
        return null;
    }
}
