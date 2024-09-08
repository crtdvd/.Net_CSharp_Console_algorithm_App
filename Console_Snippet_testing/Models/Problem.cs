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
        public Action Solution { get; set; }
        public List<string> Tags { get; set; }

        public Problem(string name, string description, Action solution, List<string> tags)
        {
            Name = name;
            Description = description;
            Solution = solution;
            Tags = tags;
        }
    }
}