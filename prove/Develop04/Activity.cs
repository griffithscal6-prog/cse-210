
using System;
using System.Collections.Generic;
using System.Threading;

namespace mindfulness;

public abstract class Activity
{
    // Private member variables
    private string _name;
    private string _description;
    private int _duration;

    // Constructor
    protected Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    // Allows child classes to access the duration
    protected int Duration
    {
        get { return _duration; }
    }

    // Runs the standard sequence for every activity
    public void Run()
    {
        DisplayStartingMessage();
        PerformActivity();
        DisplayEndingMessage();
    }

    // Each child class provides its own activity behavior
    protected abstract void PerformActivity();

    // Starting message shared by all activities
    private void DisplayStartingMessage()
    {
        Console.WriteLine($"\nWelcome to the {_name} Activity.");
        Console.WriteLine(_description);

        Console.Write("\nHow long, in seconds, would you like for your session? ");

        while (true)
        {
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int seconds) && seconds > 0)
            {
                _duration = seconds;
                break;
            }

            Console.Write("Please enter a positive whole number: ");
        }

        Console.WriteLine("\nGet ready...");
        ShowSpinner(3);
    }

    // Ending message shared by all activities
    private void DisplayEndingMessage()
    {
        Console.WriteLine("\nWell done!!");
        ShowSpinner(2);

        Console.WriteLine(
            $"You have completed the {_name} Activity for {_duration} seconds.");

        ShowSpinner(3);
        Console.WriteLine();
    }

    // Spinner animation
    protected void ShowSpinner(int seconds)
    {
        char[] animation = { '|', '/', '-', '\\' };

        for (int i = 0; i < seconds * 4; i++)
        {
            Console.Write(animation[i % animation.Length]);
            Thread.Sleep(250);
            Console.Write("\b");
        }

        Console.WriteLine();
    }

    // Countdown animation
    protected void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
        }

        Console.WriteLine();
    }

    // Selects a random item and removes it to prevent repetition
    protected string SelectRandomWithoutRepeating(
        List<string> availableItems, Random random)
    {
        if (availableItems.Count == 0)
        {
            throw new InvalidOperationException(
                "There are no available items to select.");
        }

        // Select a random index
        int index = random.Next(availableItems.Count);

        // Get the selected item
        string selectedItem = availableItems[index];

        // Remove the selected item
        availableItems.RemoveAt(index);

        // Return the selected item
        return selectedItem;
    }
}
