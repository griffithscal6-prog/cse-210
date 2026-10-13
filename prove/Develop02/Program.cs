using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine( @"
        Please choose:
        1. Write
        2. Display
        3. Save
        4. Load
        5. Quit
        Your choice: "
        );
        string response = Console.ReadLine();
        int choice;
        while (! int.TryParse(response , out choice))
        {
            response = Console.ReadLine();
        }

       Console.WriteLine ($"Your choice: {choice}");

    }
}