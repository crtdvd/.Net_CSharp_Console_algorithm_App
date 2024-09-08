using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Console_Snippet_testing.Models;
using Console_Snippet_testing.Services;

namespace Console_Snippet_testing.UI
{
    public class ConsoleUI
    {
        private readonly IProblemService _problemService;

        public ConsoleUI(IProblemService problemService)
        {
            _problemService = problemService;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Problem Solver!");
                Console.WriteLine("1. List all problems");
                Console.WriteLine("2. Search problems");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListProblems(_problemService.GetAllProblems());
                        break;
                    case "2":
                        SearchProblems();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ListProblems(IEnumerable<Problem> problems)
        {
            Console.Clear();
            int index = 1;
            foreach (var problem in problems)
            {
                Console.WriteLine($"{index}. {problem.Name}");
                index++;
            }

            Console.Write("Select a problem (or 0 to go back): ");
            if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex > 0 && selectedIndex <= problems.Count())
            {
                ShowProblemDetails(problems.ElementAt(selectedIndex - 1));
            }
        }

        private void SearchProblems()
        {
            Console.Clear();
            Console.Write("Enter search query: ");
            string? query = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(query))
            {
                var results = _problemService.SearchProblems(query);
                ListProblems(results);
            }
        }

        private void ShowProblemDetails(Problem problem)
        {
            Console.Clear();
            Console.WriteLine($"Problem: {problem.Name}");
            Console.WriteLine($"Tags: {string.Join(", ", problem.Tags)}");
            Console.WriteLine("\nDescription:");
            Console.WriteLine(problem.Description);
            Console.WriteLine("\nPress any key to run the solution...");
            Console.ReadKey();

            Console.WriteLine("\nSolution:");
            problem.Solution();

            Console.WriteLine("\nPress any key to go back...");
            Console.ReadKey();
        }
    }
}