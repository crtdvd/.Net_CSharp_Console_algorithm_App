using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_Snippet_testing.Problems
{
    public class ValidParenthesis : BaseProblem
    {
        public override string Description => @"Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.

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

        public override string SolutionMethodName => nameof(IsValid);
        public override string ClassName => nameof(ValidParenthesis);

        public override void Test()
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
            // Step 1: Initialize a stack to keep track of opening brackets
            Stack<char> stack = new Stack<char>();

            // Step 2: Iterate through each character in the string
            foreach (char c in s)
            {
                // Step 3: If it's an opening bracket, push it onto the stack
                if (c == '(' || c == '{' || c == '[')
                {
                    stack.Push(c);
                }
                // Step 4: If it's a closing bracket, check if it matches the last opening bracket
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

            // Step 5: Return true if all brackets are matched (stack is empty), false otherwise
            return stack.Count == 0;
        }
    }
}