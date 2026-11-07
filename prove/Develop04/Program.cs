
using System;

namespace mindfulness;

public class Program
{
    public static void Main(string[] args)
    {
        // Creativity enhancements:
        // The Reflection Activity accepts and stores written responses.
        // Reflection questions are randomly selected without repetition.
        // Activity prompts are also selected from available lists,
        // preventing repeated selections during a session.

        while (true)
        {
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflection activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Quit");

            Console.Write("\nSelect a choice from the menu: ");

            string choice = Console.ReadLine() ?? "";

            Activity activity;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;

                case "2":
                    activity = new ReflectionActivity();
                    break;

                case "3":
                    activity = new ListingActivity();
                    break;

                case "4":
                    Console.WriteLine(
                        "Thank you for using the Mindfulness Program!");
                    return;

                default:
                    Console.WriteLine(
                        "Invalid selection. Please try again.");
                    continue;
            }

            // Run the selected activity
            activity.Run();
        }
    }
}
