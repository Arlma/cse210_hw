// Program.cs
// Mindfulness Program (Week05)
// Author: (your name)
// Description: Console mindfulness app with Breathing, Reflection, and Listing activities.
// Creativity: Prompts/questions are shuffled per session so each is used once before repeats.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MindfulnessApp
{
    // Base Activity class (encapsulation + shared behavior)
    public abstract class Activity
    {
        private string _name;
        private string _description;
        private int _durationSeconds; // duration in seconds for the activity
        private static readonly char[] SpinnerChars = new char[] { '|', '/', '-', '\\' };
        private static readonly Random _random = new Random();

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
            _durationSeconds = 0;
        }

        // Public setter/getter for duration (validated)
        public void SetDuration(int seconds)
        {
            if (seconds < 5)
            {
                _durationSeconds = 5; // enforce minimum to allow meaningful activity
            }
            else
            {
                _durationSeconds = seconds;
            }
        }

        public int GetDuration()
        {
            return _durationSeconds;
        }

        // Standard start message (shared)
        public void DisplayStart()
        {
            Console.Clear();
            Console.WriteLine($"*** {_name} ***");
            Console.WriteLine();
            Console.WriteLine(_description);
            Console.WriteLine();
            Console.Write("How long, in seconds, would you like for your session? ");
            // Caller should set duration
        }

        // Standard prepare message and short pause
        protected void PrepareToBegin()
        {
            Console.WriteLine();
            Console.Write("Prepare to begin ");
            ShowDots(3, 700);
            Console.WriteLine();
        }

        // Standard ending message (shared)
        public void DisplayEnd()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            ShowDots(2, 800);
            Console.WriteLine($"You have completed the {_name} for {GetDuration()} seconds.");
            ShowDots(2, 800);
            Console.WriteLine();
            Console.WriteLine("Press Enter to return to the menu...");
            Console.ReadLine();
        }

        // Spinner animation for given milliseconds
        protected void ShowSpinner(int millisecondsPerSpin = 150, int totalMillis = 2000)
        {
            Stopwatch sw = Stopwatch.StartNew();
            int idx = 0;
            while (sw.ElapsedMilliseconds < totalMillis)
            {
                Console.Write(SpinnerChars[idx % SpinnerChars.Length]);
                Thread.Sleep(millisecondsPerSpin);
                Console.Write('\b');
                idx++;
            }
            sw.Stop();
        }

        // Show a countdown from given seconds (prints the number then wait 1s)
        protected void Countdown(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b"); // erase the number
            }
        }

        // Show a simple "..." dots animation
        protected void ShowDots(int count, int millisEach)
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write('.');
                Thread.Sleep(millisEach);
            }
            Console.WriteLine();
        }

        // Helper to get a random integer
        protected int Rand(int maxExclusive) => _random.Next(maxExclusive);

        // Derived classes implement RunActivity which performs the specific activity
        public abstract void RunActivity();
    }

    // Breathing Activity
    public class BreathingActivity : Activity
    {
        public BreathingActivity()
            : base("Breathing Activity",
                  "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        { }

        public override void RunActivity()
        {
            DisplayStart();
            Console.WriteLine();
            Console.WriteLine("Enter duration (seconds) and press Enter:");
            if (int.TryParse(Console.ReadLine(), out int seconds))
            {
                SetDuration(seconds);
            }
            else
            {
                SetDuration(30); // default
            }

            PrepareToBegin();

            int totalSeconds = GetDuration();
            Stopwatch sw = Stopwatch.StartNew();

            // We'll alternate: "Breathe in..." then "Breathe out..." and show a 4-second countdown by default.
            int phaseSeconds = 4; // seconds per in/out phase (tweakable)
            while (sw.Elapsed.TotalSeconds < totalSeconds)
            {
                Console.Write("Breathe in... ");
                Countdown(phaseSeconds);
                Console.WriteLine(); // newline after countdown
                if (sw.Elapsed.TotalSeconds >= totalSeconds) break;

                Console.Write("Breathe out... ");
                Countdown(phaseSeconds);
                Console.WriteLine();
            }

            sw.Stop();
            DisplayEnd();
        }
    }

    // Reflection Activity
    public class ReflectionActivity : Activity
    {
        private List<string> _prompts;
        private List<string> _questions;
        private Queue<string> _shuffledQuestions;

        public ReflectionActivity()
            : base("Reflection Activity",
                  "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        {
            _prompts = new List<string> {
                "Think of a time when you stood up for someone else.",
                "Think of a time when you did something really difficult.",
                "Think of a time when you helped someone in need.",
                "Think of a time when you did something truly selfless."
            };

            _questions = new List<string> {
                "Why was this experience meaningful to you?",
                "Have you ever done anything like this before?",
                "How did you get started?",
                "How did you feel when it was complete?",
                "What made this time different than other times when you were not as successful?",
                "What is your favorite thing about this experience?",
                "What could you learn from this experience that applies to other situations?",
                "What did you learn about yourself through this experience?",
                "How can you keep this experience in mind in the future?"
            };

            // prepare shuffled queue of questions (creativity: avoid immediate repeats)
            _shuffledQuestions = new Queue<string>(ShuffleList(_questions));
        }

        public override void RunActivity()
        {
            DisplayStart();
            Console.WriteLine();
            Console.WriteLine("Enter duration (seconds) and press Enter:");
            if (int.TryParse(Console.ReadLine(), out int seconds))
            {
                SetDuration(seconds);
            }
            else
            {
                SetDuration(60);
            }

            PrepareToBegin();

            // Show a random prompt from the prompts list
            string prompt = _prompts[Rand(_prompts.Count)];
            Console.WriteLine();
            Console.WriteLine($"--- Prompt ---\n{prompt}\n");
            Console.WriteLine("When you have something in mind, press Enter to continue...");
            Console.ReadLine();

            Stopwatch sw = Stopwatch.StartNew();
            int pausePerQuestionMs = 4000; // show spinner for 4s between questions

            while (sw.Elapsed.TotalSeconds < GetDuration())
            {
                string question = GetNextQuestion();
                Console.WriteLine($"> {question}");
                // spinner for reflection time
                ShowSpinner(150, pausePerQuestionMs);
                Console.WriteLine();
            }

            sw.Stop();
            DisplayEnd();
        }

        // Shuffles and ensures not repeating until exhausted
        private string GetNextQuestion()
        {
            if (_shuffledQuestions.Count == 0)
            {
                _shuffledQuestions = new Queue<string>(ShuffleList(_questions));
            }
            return _shuffledQuestions.Dequeue();
        }

        private List<T> ShuffleList<T>(List<T> list)
        {
            var copied = new List<T>(list);
            for (int i = copied.Count - 1; i > 0; i--)
            {
                int j = new Random().Next(i + 1);
                T tmp = copied[i];
                copied[i] = copied[j];
                copied[j] = tmp;
            }
            return copied;
        }
    }

    // Listing Activity
    public class ListingActivity : Activity
    {
        private List<string> _prompts;

        public ListingActivity()
            : base("Listing Activity",
                  "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        {
            _prompts = new List<string> {
                "Who are people that you appreciate?",
                "What are personal strengths of yours?",
                "Who are people that you have helped this week?",
                "When have you felt peaceful or thankful this month?",
                "Who are some of your personal heroes?"
            };
        }

        public override void RunActivity()
        {
            DisplayStart();
            Console.WriteLine();
            Console.WriteLine("Enter duration (seconds) and press Enter:");
            if (int.TryParse(Console.ReadLine(), out int seconds))
            {
                SetDuration(seconds);
            }
            else
            {
                SetDuration(60);
            }

            PrepareToBegin();

            // Choose a prompt randomly
            string prompt = _prompts[Rand(_prompts.Count)];
            Console.WriteLine();
            Console.WriteLine($"--- Prompt ---\n{prompt}\n");
            Console.WriteLine("You will have a few seconds to think. Starting in:");
            // short countdown to begin
            for (int i = 3; i >= 1; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
            Console.WriteLine();
            Console.WriteLine("Start listing items. Press Enter after each item.");

            List<string> entries = new List<string>();
            Stopwatch sw = Stopwatch.StartNew();
            int duration = GetDuration();

            // While time remains, read user input but don't block forever: use ReadLine with a small timeout pattern.
            // Console.ReadLine is blocking, so we implement a loop that checks time and uses Console.KeyAvailable to avoid blocking past duration.
            while (sw.Elapsed.TotalSeconds < duration)
            {
                Console.Write("> ");
                string item = ReadLineWithTimeout(duration - (int)sw.Elapsed.TotalSeconds);
                if (item == null)
                {
                    // time expired while waiting for entry
                    break;
                }
                item = item.Trim();
                if (!string.IsNullOrEmpty(item))
                {
                    entries.Add(item);
                }
            }

            sw.Stop();

            Console.WriteLine();
            Console.WriteLine($"You listed {entries.Count} items. Great job!");
            Console.WriteLine();
            DisplayEnd();
        }

        // ReadLine with timeout in seconds (returns null on timeout)
        private string ReadLineWithTimeout(int secondsTimeout)
        {
            if (secondsTimeout <= 0) return null;

            string input = "";
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds < secondsTimeout)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(intercept: true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        return input;
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (input.Length > 0)
                        {
                            input = input.Substring(0, input.Length - 1);
                            Console.Write("\b \b");
                        }
                    }
                    else
                    {
                        Console.Write(key.KeyChar);
                        input += key.KeyChar;
                    }
                }
                Thread.Sleep(50);
            }
            // timeout
            Console.WriteLine();
            return null;
        }
    }

    // Program entry, menu and loop
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Mindfulness Program ===");
                Console.WriteLine("Choose an activity:");
                Console.WriteLine("1) Breathing Activity");
                Console.WriteLine("2) Reflection Activity");
                Console.WriteLine("3) Listing Activity");
                Console.WriteLine("4) Quit");
                Console.Write("Enter choice (1-4): ");

                string choice = Console.ReadLine()?.Trim();
                Activity activity = null;

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
                        exit = true;
                        continue;
                    default:
                        Console.WriteLine("Invalid selection. Press Enter to try again.");
                        Console.ReadLine();
                        continue;
                }

                // Run selected activity
                activity.RunActivity();
            }

            Console.WriteLine("Thanks for using the Mindfulness Program. Goodbye!");
        }
    }
}
