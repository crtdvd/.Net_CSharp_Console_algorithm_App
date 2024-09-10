using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_Snippet_testing.Problems
{
    public static class ValidParenthesis
    {
        public static string Description => @"Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.

An input string is valid if:
1. Open brackets must be closed by the same type of brackets.
2. Open brackets must be closed in the correct order.
3. Every close bracket has a corresponding open bracket of the same type.

Example 1:
Input: s = ""()""
Output: true

Example 2:
Input: s = ""()[]{}""
Output: true

Example 3:
Input: s = ""(]"" 
Output: false

Constraints:
1 <= s.length <= 104
s consists of parentheses only '()[]{}'.";

        public static string SolutionCode => @"
public static bool IsValid(string s) {
    Stack<char> stack = new Stack<char>();
    foreach (char c in s) {
        if (c == '(' || c == '{' || c == '[') {
            stack.Push(c);
        } else if (c == ')' && (stack.Count == 0 || stack.Pop() != '(')) {
            return false;
        } else if (c == '}' && (stack.Count == 0 || stack.Pop() != '{')) {
            return false;
        } else if (c == ']' && (stack.Count == 0 || stack.Pop() != '[')) {
            return false;
        }
    }
    return stack.Count == 0;
}";

        public static void Test()
        {
            Console.WriteLine("Running Valid Parenthesis test...");
            string[] testCases = { "()", "()[]{}", "(]", "([)]", "{[]}" };
            foreach (string testCase in testCases)
            {
                Console.WriteLine($"Input: {testCase}");
                Console.WriteLine($"Output: {IsValid(testCase)}");
                Console.WriteLine();
            }
        }

        private static bool IsValid(string s)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char c in s)
            {
                if (c == '(' || c == '{' || c == '[')
                {
                    stack.Push(c);
                }
                else if (c == ')' && (stack.Count == 0 || stack.Pop() != '('))
                {
                    return false;
                }
                else if (c == '}' && (stack.Count == 0 || stack.Pop() != '{'))
                {
                    return false;
                }
                else if (c == ']' && (stack.Count == 0 || stack.Pop() != '['))
                {
                    return false;
                }
            }
            return stack.Count == 0;
        }
    }
}