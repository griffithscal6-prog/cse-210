//I have chosen to Improve the process of saving and loading to save as a .csv file that could be opened in Excel (making sure to account for quotation marks and commas correctly in my content.)
//I have also chosen to implement a way to save the time that an entry is entered
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

        string date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");

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
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("Date,Prompt,Response");

                foreach (Entry entry in _entries)
                {
                    writer.WriteLine(
                        $"{EscapeCsv(entry.Date)},{EscapeCsv(entry.Prompt)},{EscapeCsv(entry.Response)}"
                    );
                }
            }

            Console.WriteLine("Journal saved successfully as CSV!\n");
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
            string[] lines = File.ReadAllLines(filename);
            List<Entry> loadedEntries = new List<Entry>();

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                List<string> fields = ParseCsvLine(lines[i]);

                if (fields.Count != 3)
                {
                    throw new FormatException(
                        $"Invalid CSV data on line {i + 1}."
                    );
                }

                Entry entry = new Entry(
                    fields[0],
                    fields[1],
                    fields[2]
                );

                loadedEntries.Add(entry);
            }

            _entries = loadedEntries;
            Console.WriteLine("Journal loaded successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading journal: {ex.Message}\n");
        }
    }

private string EscapeCsv(string value)
{
    value ??= "";

    return "\"" + value.Replace("\"", "\"\"") + "\"";
}

private List<string> ParseCsvLine(string line)
{
    List<string> fields = new List<string>();
    bool insideQuotes = false;
    string field = "";

    for (int i = 0; i < line.Length; i++)
    {
        char c = line[i];

        if (c == '"')
        {
            if (insideQuotes && i + 1 < line.Length
                && line[i + 1] == '"')
            {
                field += '"';
                i++;
            }
            else
            {
                insideQuotes = !insideQuotes;
            }
        }
        else if (c == ',' && !insideQuotes)
        {
            fields.Add(field);
            field = "";
        }
        else
        {
            field += c;
        }
    }

    if (insideQuotes)
    {
        throw new FormatException("Unclosed quotation mark in CSV.");
    }

    fields.Add(field);
    return fields;
}

}
