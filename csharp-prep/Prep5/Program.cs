using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome();
        string userName = PromptUserName();
        int favoriteNumber = PromptUserNumber();
        int birthYear;
        PromptUserBirthYear(out birthYear);
        SquareNumber(favoriteNumber);
        DisplayResult(userName, favoriteNumber, birthYear);

    }
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program! ");
    }
    static string PromptUserName()
    {
        Console.WriteLine("What is your Name? ");
        string name = Console.ReadLine();
        return name;
    }
    static int PromptUserNumber()
    {
        Console.WriteLine("What is your Favorite Number? ");
        string var = Console.ReadLine();
        int number = int.Parse(var);
        return number;
    }
    static void PromptUserBirthYear(out int birthYear)
    {
        Console.WriteLine("What Year Were you Born? ");
        birthYear = int.Parse(Console.ReadLine());
    }
    static int SquareNumber(int number)
    {
        int square = number * number;
        return square;
    }
    static void DisplayResult(string name, int square, int birthYear)
    {
        Console.WriteLine($"{name}, the square of your number is {square}");
        Console.WriteLine($"{name}, You will turn {2026 - birthYear} this year! ");
    }

}