using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_Snippet_testing.Models
{
    public class Solution
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string TestCases { get; set; }

        public Solution(string name, string description, string code, string testCases)
        {
            Name = name;
            Description = description;
            Code = code;
            TestCases = testCases;
        }
    }
}
