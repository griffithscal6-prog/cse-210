
namespace Journal;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Save");
            Console.WriteLine("4. Load");
            Console.WriteLine("5. Quit");
            Console.WriteLine();
            Console.Write("Your choice: ");

            string response = Console.ReadLine();

            if (!int.TryParse(response, out int choice))
            {
                Console.WriteLine("Please enter a valid number.\n");
                continue;
            }

            switch (choice)
            {
                case 1:
                    journal.WriteEntry();
                    break;

                case 2:
                    journal.Display();
                    break;

                case 3:
                    journal.Save();
                    break;

                case 4:
                    journal.Load();
                    break;

                case 5:
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Please choose a number from 1 to 5.\n");
                    break;
            }
        }
    }
}
