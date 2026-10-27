using System;

class Program
{
    static void Main(string[] args)
    {
        Assignment assignment =
            new Assignment("Samuel Bennett", "Multiplication");
            Console.WriteLine(assignment.getSummary());

        MathAssignment math =
            new MathAssignment("Samuel Bennett", 
            "Multiplication", 
            "7.3", 
            "8-19");

        Console.WriteLine(math.getSummary());
        Console.WriteLine(math.GetHomeworkList());

        WritingAssignment writing =
            new WritingAssignment(
                "Mary Waters",
                "European History",
                "The Causes of World War II");
        Console.WriteLine(writing.getSummary());
        Console.WriteLine(writing.GetWritingInformation());
    }
}