using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Reflection.Emit;
using System.IO;


namespace Console_Snippet_testing.Problems
{
    public class ArrayTripletsSum : BaseProblem
    {
        public override string Description => @"Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]] such that i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0.

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

        public override string SolutionMethodName => nameof(ThreeSumSolution);
        public override string ClassName => nameof(ArrayTripletsSum);

        public override void Test()
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