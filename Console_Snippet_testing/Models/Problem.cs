using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_Snippet_testing.Models
{
    public class Problem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SolutionCode { get; set; }
        public Action TestSolution { get; set; }
        public List<string> Tags { get; set; }

        public Problem(string name, string description, string solutionCode, Action testSolution, List<string> tags)
        {
            Name = name;
            Description = description;
            SolutionCode = solutionCode;
            TestSolution = testSolution;
            Tags = tags;
        }
    }
}