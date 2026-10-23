
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Word;

class Program
{
    static async Task Main(string[] args)
    {
        // Exceeded the core requirements by connecting the program to the Open Scripture API.
        // Users can enter a scripture reference, including a single verse or a range of
        // verses, and the program retrieves the requested scripture text from the API.
        // The program validates the requested verses and allows users to try again if
        // the reference is invalid or the API cannot retrieve the scripture. It then
        // combines the requested verses and uses them in the scripture memorization
        // activity, allowing users to hide words and reveal the complete scripture.

        using HttpClient client = new HttpClient();

        Scripture? scripture = null;

        while (scripture == null)
        {
            Console.Write(
                "Enter a scripture reference (example: John 3:16 " +
                "or Proverbs 3:5-6): "
            );

            string input = Console.ReadLine() ?? "";

            Match match = Regex.Match(
                input.Trim(),
                @"^(?<book>.+)\s+(?<chapter>\d+):" +
                @"(?<startVerse>\d+)(?:-(?<endVerse>\d+))?$"
            );

            if (!match.Success)
            {
                Console.WriteLine(
                    "Invalid format. Try John 3:16 or Proverbs 3:5-6."
                );
                continue;
            }

            string book = match.Groups["book"].Value.Trim();

            if (!int.TryParse(
                    match.Groups["chapter"].Value, out int chapter) ||
                !int.TryParse(
                    match.Groups["startVerse"].Value, out int startVerse))
            {
                Console.WriteLine("Invalid chapter or verse number.");
                continue;
            }

            int endVerse = startVerse;

            if (match.Groups["endVerse"].Success &&
                !int.TryParse(
                    match.Groups["endVerse"].Value, out endVerse))
            {
                Console.WriteLine("Invalid ending verse.");
                continue;
            }

            if (chapter < 1 || startVerse < 1 ||
                endVerse < startVerse)
            {
                Console.WriteLine(
                    "Chapter and verse numbers must be positive, " +
                    "and the end verse cannot come before the start verse."
                );
                continue;
            }

            string bookId = Regex.Replace(book, @"[\s.]", "")
                                 .ToLowerInvariant();

            List<string> verseTexts = new List<string>();
            string actualBook = book;
            bool validReference = true;

            for (int verse = startVerse; verse <= endVerse; verse++)
            {
                string url =
                    "https://openscriptureapi.org/api/scriptures/v1/lds/en" +
                    $"/book/{bookId}/{chapter}/{verse}";

                try
                {
                    HttpResponseMessage response =
                        await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(
                            $"Could not find {book} {chapter}:{verse}. " +
                            "Please check the reference."
                        );

                        validReference = false;
                        break;
                    }

                    string json =
                        await response.Content.ReadAsStringAsync();

                    using JsonDocument document =
                        JsonDocument.Parse(json);

                    JsonElement root = document.RootElement;

                    string returnedBook =
                        root.GetProperty("book").GetString()!;

                    int returnedChapter =
                        root.GetProperty("chapter").GetInt32();

                    int returnedVerse =
                        root.GetProperty("verse").GetInt32();

                    string verseText =
                        root.GetProperty("text").GetString()!;

                    // Verify that the API returned the requested verse.
                    if (returnedChapter != chapter ||
                        returnedVerse != verse ||
                        string.IsNullOrWhiteSpace(verseText))
                    {
                        Console.WriteLine(
                            "The API returned unexpected verse data."
                        );

                        validReference = false;
                        break;
                    }

                    actualBook = returnedBook;
                    verseTexts.Add(verseText);
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine(
                        "Could not connect to the scripture API. " +
                        "Please try again."
                    );

                    validReference = false;
                    break;
                }
                catch (JsonException)
                {
                    Console.WriteLine(
                        "The scripture response could not be read."
                    );

                    validReference = false;
                    break;
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine(
                        "The API response is missing required information."
                    );

                    validReference = false;
                    break;
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine(
                        "The API returned scripture data in an " +
                        "unexpected format."
                    );

                    validReference = false;
                    break;
                }
            }

            if (!validReference)
            {
                continue;
            }

            // Join all retrieved verses into one scripture text.
            string fullText = string.Join(" ", verseTexts);

            Reference reference;

            if (startVerse == endVerse)
            {
                reference = new Reference(
                    actualBook, chapter, startVerse
                );
            }
            else
            {
                reference = new Reference(
                    actualBook, chapter, startVerse, endVerse
                );
            }

            scripture = new Scripture(reference, fullText);
        }

        // Memorization loop.
        while (true)
        {
            Console.Clear();

            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            Console.WriteLine(
                "Press Enter to hide words or reveal the scripture, " +
                "or type 'quit' to exit:"
            );

            string input = Console.ReadLine() ?? "";

            if (input.Trim().Equals(
                "quit",
                StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            if (input == "")
            {
                if (scripture.IsCompletelyHidden())
                {
                    scripture.RevealAllWords();

                    Console.Clear();
                    Console.WriteLine(scripture.GetDisplayText());
                    break;
                }

                scripture.HideRandomWords(3);
            }
        }
    }
}
