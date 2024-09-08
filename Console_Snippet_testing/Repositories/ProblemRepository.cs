using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Console_Snippet_testing.Models;
using Console_Snippet_testing.Problems;

namespace Console_Snippet_testing.Repositories
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly List<Problem> _problems;

        public ProblemRepository()
        {
            _problems = new List<Problem>
            {
                new Problem(
                    "Valid Parenthesis",
                    ValidParenthesis.Description,
                    ValidParenthesis.Test,
                    new List<string> { "string", "stack", "validation" }
                ),
                new Problem(
                    "Array Triplets Sum",
                    ArrayTripletsSum.Description,
                    ArrayTripletsSum.Test,
                    new List<string> { "array", "sum", "triplets" }
                ),
                new Problem(
                    "Letter Combinations of a Phone Number",
                    LetterCombinations.Description,
                    LetterCombinations.Test,
                    new List<string> { "string", "combination", "recursion" }
                )
            };
        }

        public IEnumerable<Problem> GetAll() => _problems;

        public IEnumerable<Problem> Search(string query)
        {
            return _problems.Where(p =>
                p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                p.Tags.Any(t => t.Contains(query, StringComparison.OrdinalIgnoreCase))
            );
        }

        public void Add(Problem problem)
        {
            _problems.Add(problem);
        }
    }
}
