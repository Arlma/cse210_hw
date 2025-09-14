
using System;

class Program
{
    static void Main(string[] args)
    {
        // Example scripture and reference - replace with any verse you'd like.
        var reference = new Reference("John", 3, 16);
        var text = "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.";
        var scripture = new Scripture(reference, text);

        Console.WriteLine("Scripture Memorizer");
        Console.WriteLine("-------------------");
        Console.WriteLine("Press Enter to hide more words. Type 'quit' and press Enter to exit.");
        Console.WriteLine();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Scripture:");
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("All words are hidden. Well done!");
                break;
            }

            Console.Write("Press Enter to hide more words or type 'quit' to stop: ");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && input.Trim().ToLower() == "quit")
            {
                Console.WriteLine("Quitting. Goodbye!");
                break;
            }

            // Each Enter press hides a few more words. You may adjust the number
            // to change difficulty (1 = slow, 3 = moderate, 5 = fast).
            scripture.HideRandomWords(3);
        }

        Console.WriteLine("Press any key to close.");
        Console.ReadKey();
    }
}
