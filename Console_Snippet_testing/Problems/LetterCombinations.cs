using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_Snippet_testing.Problems
{
    public class LetterCombinations
    {
        public static string Description => @"Given a string containing digits from 2-9 inclusive, return all possible letter combinations that the number could represent. Return the answer in any order.

A mapping of digits to letters (just like on the telephone buttons) is given below. Note that 1 does not map to any letters.

2 = (a, b, c), 3 = (d, e, f), 4 = (g, h, i), 5 = (j, k, l), 6 = (m, n, o), 7 = (p, q, r, s), 8 = (t, u, v), 9 = (w, x, y, z)

Example 1:
Input: digits = ""23""
Output: [""ad"",""ae"",""af"",""bd"",""be"",""bf"",""cd"",""ce"",""cf""]

Example 2:
Input: digits = """"
Output: []

Example 3:
Input: digits = ""2""
Output: [""a"",""b"",""c""]

Constraints:
0 <= digits.length <= 4
digits[i] is a digit in the range ['2', '9'].";

        public static string SolutionCode => @"
public IList<string> LetterCombinationsSolution(string digits) {
    if (string.IsNullOrEmpty(digits)) return new List<string>();
    
    var phoneMap = new Dictionary<char, string> {
        {'2', ""abc""}, {'3', ""def""}, {'4', ""ghi""}, {'5', ""jkl""},
        {'6', ""mno""}, {'7', ""pqrs""}, {'8', ""tuv""}, {'9', ""wxyz""}
    };
    
    var result = new List<string>();
    Backtrack("""", digits, 0, phoneMap, result);
    return result;
}

private void Backtrack(string combination, string digits, int index, 
                       Dictionary<char, string> phoneMap, List<string> result) {
    if (index == digits.Length) {
        result.Add(combination);
        return;
    }
    
    foreach (char letter in phoneMap[digits[index]]) {
        Backtrack(combination + letter, digits, index + 1, phoneMap, result);
    }
}";

        public static void Test()
        {
            Console.WriteLine("Running Letter Combinations test...");
            string[] testCases = { "23", "", "2" };
            foreach (string testCase in testCases)
            {
                Console.WriteLine($"Input: {testCase}");
                Console.WriteLine($"Output: [{string.Join(", ", LetterCombinationsSolution(testCase))}]");
                Console.WriteLine();
            }
        }

        private static IList<string> LetterCombinationsSolution(string digits)
        {
            // Implement the method here, similar to the SolutionCode
            // This is needed for the Test method to work
            if (string.IsNullOrEmpty(digits)) return new List<string>();
            
            var phoneMap = new Dictionary<char, string> {
                {'2', "abc"}, {'3', "def"}, {'4', "ghi"}, {'5', "jkl"},
                {'6', "mno"}, {'7', "pqrs"}, {'8', "tuv"}, {'9', "wxyz"}
            };
            
            var result = new List<string>();
            Backtrack("", digits, 0, phoneMap, result);
            return result;
        }

        private static void Backtrack(string combination, string digits, int index, 
                                      Dictionary<char, string> phoneMap, List<string> result)
        {
            if (index == digits.Length)
            {
                result.Add(combination);
                return;
            }
            
            foreach (char letter in phoneMap[digits[index]])
            {
                Backtrack(combination + letter, digits, index + 1, phoneMap, result);
            }
        }
    }
}