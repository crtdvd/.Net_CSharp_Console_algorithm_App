using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Reflection.Emit;
using System.IO;


namespace Console_Snippet_testing.Problems
{
    public class ArrayTripletsSum
    {
        public static string Description => @"Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]] such that i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0.

Notice that the solution set must not contain duplicate triplets.

Example 1:
Input: nums = [-1,0,1,2,-1,-4]
Output: [[-1,-1,2],[-1,0,1]]
Explanation: 
nums[0] + nums[1] + nums[2] = (-1) + 0 + 1 = 0.
nums[1] + nums[2] + nums[4] = 0 + 1 + (-1) = 0.
nums[0] + nums[3] + nums[4] = (-1) + 2 + (-1) = 0.
The distinct triplets are [-1,0,1] and [-1,-1,2].
Notice that the order of the output and the order of the triplets does not matter.

Example 2:
Input: nums = [0,1,1]
Output: []
Explanation: The only possible triplet does not sum up to 0.

Example 3:
Input: nums = [0,0,0]
Output: [[0,0,0]]
Explanation: The only possible triplet sums up to 0.

Constraints:
3 <= nums.length <= 3000
-105 <= nums[i] <= 105";

        public static string SolutionCode => GetMethodBody(nameof(ThreeSumSolution));

        public static void Test()
        {
            Console.WriteLine("Running Array Triplets Sum test...");
            int[][] testCases = {
                new int[] {-1, 0, 1, 2, -1, -4},
                new int[] {0, 1, 1},
                new int[] {0, 0, 0}
            };

            foreach (var testCase in testCases)
            {
                Console.WriteLine($"Input: [{string.Join(", ", testCase)}]");
                var result = ThreeSumSolution(testCase);
                Console.WriteLine("Output:");
                foreach (var triplet in result)
                {
                    Console.WriteLine($"[{string.Join(", ", triplet)}]");
                }
                Console.WriteLine();
            }
        }

        private static string GetMethodBody(string methodName)
        {
            // Get the type of the current class
            var type = typeof(ArrayTripletsSum);
            // Get the assembly containing this type
            var assembly = type.Assembly;
            // Get the location of the assembly file
            var assemblyLocation = assembly.Location;
            // Navigate up three directory levels from the bin folder to reach the project root
            var projectRoot = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(assemblyLocation) ?? "", "..", "..", ".."));
            // Construct the full path to the source file
            var filePath = Path.Combine(projectRoot, "Problems", "ArrayTripletsSum.cs");

            // Check if the file exists at the constructed path
            if (!File.Exists(filePath))
            {
                return $"// Error: Could not find source file at {filePath}";
            }

            // Read the entire content of the source file
            var sourceCode = File.ReadAllText(filePath);
            // Find the starting index of the method name in the source code
            var methodStart = sourceCode.IndexOf(methodName);
            if (methodStart == -1) return $"// Error: Method '{methodName}' not found in source code";
            
            // Find the start of the method body (first opening brace after method name)
            var bodyStart = sourceCode.IndexOf('{', methodStart);
            if (bodyStart == -1) return $"// Error: Could not find start of method body for '{methodName}'";
            
            // Initialize variables to keep track of nested braces
            var bracketCount = 1;
            var methodEnd = bodyStart + 1;
            
            // Iterate through the source code to find the end of the method body
            for (int i = bodyStart + 1; i < sourceCode.Length; i++)
            {
                if (sourceCode[i] == '{') bracketCount++;
                if (sourceCode[i] == '}') bracketCount--;
                if (bracketCount == 0)
                {
                    methodEnd = i + 1;
                    break;
                }
            }

            // Extract the method body from the source code
            var methodBody = sourceCode.Substring(bodyStart, methodEnd - bodyStart);
            if (string.IsNullOrWhiteSpace(methodBody))
            {
                return $"// Error: Empty method body for '{methodName}'";
            }

            // Return the extracted method body
            return methodBody;
        }

        private static string GetOpCodeName(byte code)
        {
            // Get all public static fields of OpCodes class
            // These fields represent all possible OpCodes
            var opCodes = typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static)
                                         .Where(f => f.FieldType == typeof(OpCode));

            // Iterate through all OpCode fields
            foreach (var field in opCodes)
            {
                // Get the value of the current field
                var value = field.GetValue(null);
                if (value != null)
                {
                    // Cast the value to OpCode
                    var opCode = (OpCode)value;
                    // Check if the OpCode's value matches the input byte code
                    if (opCode.Value == code)
                    {
                        // If a match is found, return the name of the OpCode
                        return field.Name;
                    }
                }
            }

            // If no matching OpCode is found, return "Unknown"
            return "Unknown";
        }

        public static IList<IList<int>> ThreeSumSolution(int[] nums)
        {
            // Step 1: Sort the array to handle duplicates and optimize the search
            // Sorting allows us to easily skip duplicates and use two-pointer technique
            Array.Sort(nums);
            var result = new List<IList<int>>();
            
            // Step 2: Iterate through the array, using the current number as the first number of the triplet
            // We stop at length - 2 because we need at least two more numbers after i
            for (int i = 0; i < nums.Length - 2; i++)
            {
                // Step 3: Skip duplicates for the first number to avoid duplicate triplets
                // If the current number is the same as the previous, we've already processed it
                if (i > 0 && nums[i] == nums[i - 1]) continue;
                
                // Initialize two pointers: left starts after i, right starts at the end
                int left = i + 1;
                int right = nums.Length - 1;
                
                // Step 4: Use two pointers to find the other two numbers of the triplet
                // We move these pointers towards each other to find pairs that sum to -nums[i]
                while (left < right)
                {
                    // Calculate the sum of the current triplet
                    int sum = nums[i] + nums[left] + nums[right];
                    if (sum == 0)
                    {
                        // Step 5: Found a triplet that sums to zero, add it to the result
                        result.Add(new List<int> { nums[i], nums[left], nums[right] });
                        
                        // Step 6: Skip duplicates for the second and third numbers
                        // Move left pointer right while skipping duplicates
                        while (left < right && nums[left] == nums[left + 1]) left++;
                        // Move right pointer left while skipping duplicates
                        while (left < right && nums[right] == nums[right - 1]) right--;
                        
                        // Move both pointers inward
                        left++;
                        right--;
                    }
                    else if (sum < 0)
                    {
                        // Step 7: Sum is too small, move left pointer to increase the sum
                        // Since the array is sorted, moving left increases the sum
                        left++;
                    }
                    else
                    {
                        // Step 8: Sum is too large, move right pointer to decrease the sum
                        // Since the array is sorted, moving right decreases the sum
                        right--;
                    }
                }
            }
            
            // Step 9: Return the list of triplets
            // At this point, we have found all unique triplets that sum to zero
            return result;
        }
    }
}