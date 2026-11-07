
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace mindfulness;

public class ReflectionActivity : Activity
{
    public ReflectionActivity()
        : base(
            "Reflection",
            "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    protected override void PerformActivity()
    {
        string[] prompts =
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        string[] questions =
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        Random random = new Random();

        // Create lists of available prompts and questions
        List<string> availablePrompts = new List<string>(prompts);
        List<string> availableQuestions = new List<string>(questions);

        // Store the user's responses
        List<string> responses = new List<string>();

        // Start timing the reflection activity
        Stopwatch timer = Stopwatch.StartNew();

        // Display a random prompt without repeating
        string selectedPrompt = SelectRandomWithoutRepeating(
            availablePrompts, random);

        Console.WriteLine("\nConsider the following prompt:");
        Console.WriteLine($"--- {selectedPrompt} ---");

        // Give the user an opportunity to respond
        Console.Write("\nYour response: ");
        string initialResponse = Console.ReadLine() ?? "";

        // Only store a response if the user entered something
        if (!string.IsNullOrWhiteSpace(initialResponse))
        {
            responses.Add(initialResponse);
        }

        // Continue asking questions until the duration is reached
        while (timer.Elapsed.TotalSeconds < Duration)
        {
            // Check whether all questions have been used
            if (availableQuestions.Count == 0)
            {
                Console.WriteLine(
                    "\nYou've considered all the questions. Continue reflecting on your experience.");

                int remainingSeconds =
                    Duration - (int)timer.Elapsed.TotalSeconds;

                if (remainingSeconds > 0)
                {
                    ShowSpinner(remainingSeconds);
                }

                break;
            }

            // Select a question that hasn't been used
            string selectedQuestion = SelectRandomWithoutRepeating(
                availableQuestions, random);

            Console.WriteLine("\n" + selectedQuestion);

            // Accept the user's response
            Console.Write("Your response: ");
            string response = Console.ReadLine() ?? "";

            // Store nonempty responses
            if (!string.IsNullOrWhiteSpace(response))
            {
                responses.Add(response);
            }

            // Calculate the remaining time
            int remainingTime =
                Duration - (int)timer.Elapsed.TotalSeconds;

            if (remainingTime <= 0)
            {
                break;
            }

            // Pause before the next question
            Console.WriteLine("\nTake a moment to reflect...");
            ShowSpinner(Math.Min(5, remainingTime));
        }

        // Display the number of responses
        Console.WriteLine(
            $"\nYou recorded {responses.Count} reflection(s)!");

        // Display the user's responses
        if (responses.Count > 0)
        {
            Console.WriteLine("\nYour reflections:");

            foreach (string response in responses)
            {
                Console.WriteLine($"- {response}");
            }
        }
    }

    // Selects a random item and removes it to prevent repetition
    private string SelectRandomWithoutRepeating(
        List<string> availableItems, Random random)
    {
        int index = random.Next(availableItems.Count);

        string selectedItem = availableItems[index];

        availableItems.RemoveAt(index);

        return selectedItem;
    }
}
