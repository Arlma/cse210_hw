using System;

class Program
{
    static void Main(string[] args)
    {
        //Ask for first name
        Console.WriteLine("Hello World! This is the Exercise1 Project.");
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();

        // Ask for last name
        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");
    }
}
 