using System;

class Program
{
    static void Main(string[] args)
    {
        float sum = 0;
        float average = 0;
        int largest = 0;
        float total;
        List<int> numbers;
        numbers = new List<int>();
        Console.WriteLine("Enter Number: ");
        string number = Console.ReadLine();
        int value = int.Parse(number);
        
        while (value != 0)
        {
            numbers.Add(value);
            sum = sum + value;
            if (value > largest)
            {
                largest = value;
            }
            Console.WriteLine("Enter Number: ");
            number = Console.ReadLine();
            value = int.Parse(number);
        }
        
        total = sum;
        average = sum/numbers.Count;
        Console.WriteLine($"The Sum is: {total} ");
        Console.WriteLine($"The Average is: {average} ");
        Console.WriteLine($"The Largest Number is: {largest} ");
    }
}