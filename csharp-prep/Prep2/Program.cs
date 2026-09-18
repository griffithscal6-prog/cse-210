using System;

class Program
{
    static void Main(string[] args)
    {
        string letter;
        //Prompt the user for their grade
        Console.WriteLine("What is your Grade? ");
        string grade = Console.ReadLine();
        int x = int.Parse(grade);
        if (x >= 90)
        {
            letter = "A";
        }
        else if (x >= 80)
        {
            letter = "B";
        }
        else if (x >= 70)
        {
            letter = "C";
        }
        else if (x >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        if (x >= 70)
        {
            Console.WriteLine($"Congratulations! You passed the course with a {letter}!");
        }
        else
        {
            Console.WriteLine($"Unfortunately, You failed the course with a {letter}. :( Better luck Next time!");
        }
    }
}