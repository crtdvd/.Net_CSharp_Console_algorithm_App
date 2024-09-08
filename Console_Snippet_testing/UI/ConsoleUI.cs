using System;
using System.Collections.Generic;
using System.Linq;
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
                PrintHeader("Welcome to Problem Solver!");
                PrintMenuOption("1", "List all problems");
                PrintMenuOption("2", "Search problems");
                PrintMenuOption("3", "Add new problem");
                PrintMenuOption("4", "Exit");
                Console.Write("\nChoose an option: ");

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
                        AddNewProblem();
                        break;
                    case "4":
                        return;
                    default:
                        PrintError("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ListProblems(IEnumerable<Problem> problems)
        {
            Console.Clear();
            PrintHeader("Problem List");
            int index = 1;
            foreach (var problem in problems)
            {
                PrintProblemListItem(index, problem.Name);
                index++;
            }

            Console.Write("\nSelect a problem (or 0 to go back): ");
            if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex > 0 && selectedIndex <= problems.Count())
            {
                ShowProblemDetails(problems.ElementAt(selectedIndex - 1));
            }
        }

        private void SearchProblems()
        {
            Console.Clear();
            PrintHeader("Search Problems");
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
            PrintHeader(problem.Name);
            PrintSubHeader("Tags");
            Console.WriteLine(string.Join(", ", problem.Tags));
            PrintSubHeader("Description");
            Console.WriteLine(problem.Description);
            Console.WriteLine("\nPress any key to run the solution...");
            Console.ReadKey();

            PrintSubHeader("Solution");
            problem.Solution();

            Console.WriteLine("\nPress any key to go back...");
            Console.ReadKey();
        }

        private void AddNewProblem()
        {
            Console.Clear();
            PrintHeader("Add New Problem");

            Console.Write("Enter problem name: ");
            string? name = Console.ReadLine();

            Console.Write("Enter problem description: ");
            string? description = Console.ReadLine();

            Console.Write("Enter tags (comma-separated): ");
            string? tagsInput = Console.ReadLine();
            List<string> tags = tagsInput?.Split(',').Select(t => t.Trim()).ToList() ?? new List<string>();

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(description))
            {
                Problem newProblem = new Problem(name, description, () => Console.WriteLine("Solution not implemented yet."), tags);
                _problemService.AddProblem(newProblem);
                Console.WriteLine("\nProblem added successfully. Press any key to continue...");
            }
            else
            {
                PrintError("Invalid input. Problem not added. Press any key to continue...");
            }

            Console.ReadKey();
        }

        private void PrintHeader(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n{text}");
            Console.WriteLine(new string('=', text.Length));
            Console.ResetColor();
        }

        private void PrintSubHeader(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{text}");
            Console.WriteLine(new string('-', text.Length));
            Console.ResetColor();
        }

        private void PrintMenuOption(string key, string description)
        {
            Console.Write($"  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(key);
            Console.ResetColor();
            Console.WriteLine($". {description}");
        }

        private void PrintProblemListItem(int index, string name)
        {
            Console.Write($"  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{index}");
            Console.ResetColor();
            Console.WriteLine($". {name}");
        }

        private void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}