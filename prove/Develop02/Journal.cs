
using System.Text.Json;

namespace Journal;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();
    private List<string> _prompts = new List<string>();
    private Random _random = new Random();

    public Journal()
    {
        string filename = "questions.json";

        if (File.Exists(filename))
        {
            string json = File.ReadAllText(filename);

            using JsonDocument document = JsonDocument.Parse(json);
            JsonElement questions = document.RootElement.GetProperty("questions");

            foreach (JsonElement question in questions.EnumerateArray())
            {
                _prompts.Add(question.GetString());
            }
        }
    }

    public void WriteEntry()
    {
        if (_prompts.Count == 0)
        {
            Console.WriteLine("No prompts are available.\n");
            return;
        }

        int index = _random.Next(_prompts.Count);
        string prompt = _prompts[index];

        Console.WriteLine(prompt);
        Console.Write("> ");
        string response = Console.ReadLine();

        string date = DateTime.Now.ToShortDateString();

        Entry entry = new Entry(date, prompt, response);
        _entries.Add(entry);

        Console.WriteLine("Entry added successfully!");
    }

    public void Display()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("Your journal is empty.\n");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void Save()
    {
        Console.Write("What filename would you like to save to? ");
        string filename = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Filename cannot be empty.\n");
            return;
        }

        try
        {
            string json = JsonSerializer.Serialize(
                _entries,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(filename, json);
            Console.WriteLine("Journal saved successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving journal: {ex.Message}\n");
        }
    }

    public void Load()
    {
        Console.Write("What filename would you like to load? ");
        string filename = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Filename cannot be empty.\n");
            return;
        }

        try
        {
            string json = File.ReadAllText(filename);

            List<Entry> loadedEntries =
                JsonSerializer.Deserialize<List<Entry>>(json);

            if (loadedEntries == null)
            {
                Console.WriteLine("Invalid journal file.\n");
                return;
            }

            _entries = loadedEntries;
            Console.WriteLine("Journal loaded successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading journal: {ex.Message}\n");
        }
    }
}
