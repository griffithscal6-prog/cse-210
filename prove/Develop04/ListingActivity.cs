
using System;
using System.Collections.Generic;

namespace mindfulness;

public class ListingActivity : Activity
{
    public ListingActivity()
        : base(
            "Listing",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void PerformActivity()
    {
        string[] prompts =
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        Random random = new Random();

        // Create a list of available prompts
        List<string> availablePrompts = new List<string>(prompts);

        // Display a random prompt without repeating
        string selectedPrompt = SelectRandomWithoutRepeating(
            availablePrompts, random);

        Console.WriteLine(
            "\nList as many responses as you can to the following prompt:");

        Console.WriteLine($"--- {selectedPrompt} ---");

        // Give the user time to think
        Console.WriteLine("\nYou may begin thinking about the prompt in...");
        ShowCountDown(5);

        // Start collecting entries
        Console.WriteLine(
            "\nStart listing your responses. Press ENTER after each item.");

        System.Diagnostics.Stopwatch timer =
            System.Diagnostics.Stopwatch.StartNew();

        int count = 0;

        while (timer.Elapsed.TotalSeconds < Duration)
        {
            Console.Write("> ");

            string entry = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(entry))
            {
                count++;
            }
        }

        // Display the number of items entered
        Console.WriteLine($"\nYou listed {count} items!");
    }
}
