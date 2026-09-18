using System;

class Program
{
    static void Main(string[] args)
    {
        //Prompt the user for First Name
        Console.WriteLine("What is your First Name? ");

        //Read the input and store it as a variable
        string firstName = Console.ReadLine();

        //Prompt the user for Last Name
        Console.WriteLine("What is your Last Name? ");

        //Read the input and store it as a variable
        string lastName = Console.ReadLine();

        //Use the input as instructed
        Console.WriteLine($"Your Name is {lastName}, {firstName} {lastName}.");

    }
}