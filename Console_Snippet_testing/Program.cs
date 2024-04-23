using System.Security.Cryptography.X509Certificates;

namespace Program
{
    class Program
    {
        /*VALID PARENTHESIS

            Given a string s containing just the characters 
            '(', ')', '{', '}', '[' and ']', determine if the input string is valid.

            An input string is valid if:

            Open brackets must be closed by the same type of brackets.
            Open brackets must be closed in the correct order.
            Every close bracket has a corresponding open bracket of the same type.
        */
        public static bool ValidParenthesis(string s)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char c in s)
            {
                if (c == '(')
                    stack.Push(')');
                else if (c == '{')
                    stack.Push('}');
                else if (c == '[')
                    stack.Push(']');
                else if (stack.Count == 0 || stack.Pop() != c)
                    return false;
            }
            return stack.Count == 0;
        }

        public static void ValidPar_Test()
        {
            // Write a program to check if the input string is a valid parenthesis string or not.
            bool valid = false;

            while (!valid)
            {
                Console.WriteLine("Please enter some characters :)");
                string? input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    Console.WriteLine(ValidParenthesis(input));
                    valid = true;
                }
            }

        }

        /* ARRAY TRIPLETS SUM

            Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]] 
            such that i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0.

            Notice that the solution set must not contain duplicate triplets.
        */
        public static IList<IList<int>> TripletSum(int[] nums)
        {
            /*
                THIS SOLUTION USES UP TO 3 LOOPS TO ITERATE EXPONENTIALLY
                INCREASING THE COMPUTACIONAL COST.
            */
            List<int> numList = nums.ToList();
            List<List<int>> outPutList = new List<List<int>>();
            List<int> innerList = new List<int>();
            HashSet<int> indexList = new HashSet<int>();
            HashSet<HashSet<int>> indexTriplets = new HashSet<HashSet<int>>();

            int numA, numB, numC;

            bool contains;

            if (numList.Count() >= 3)
            {
                for (int i = 0; i < numList.Count(); i++)
                {
                    for (int j = 0; j < numList.Count(); j++)
                    {
                        if (i != j)
                        {
                            //Better performance using the negative result of the first two 
                            //elements sum and using HashSet to compare indexes
                            numA = numList[i];
                            numB = numList[j];

                            numC = numA + numB == 0 ? 0 : -(numA + numB);
                            if (numList.Contains(numC))
                            {

                                int k = numList.IndexOf(numC);
                                if (k != i && k != j)
                                {

                                    indexList = new HashSet<int>() { i, j, k };

                                    if (!indexTriplets.Contains(indexList))
                                    {

                                        indexTriplets.Add(indexList);

                                        innerList = new List<int>();
                                        innerList.AddRange(new int[] { numA, numB, numC });

                                        contains = outPutList.Any(list => list.OrderBy(x => x).
                                                    SequenceEqual(innerList.OrderBy(x => x)));
                                        if (!contains)
                                        {
                                            outPutList.Add(innerList);
                                        }
                                    }
                                }
                            }

                            //Longest and not well performed loop

                            /*for (int k = 0; k < numList.Count(); k++)
                            {
                                valid = true;
                                if (k != i && k != j)
                                {
                                    
                                    numC = numList[k];

                                    if (numA + numB + numC == 0)
                                    {
                                        foreach (List<int> indexes in indexTriplets)
                                        {
                                            if (indexes.Contains(i) && indexes.Contains(j) &&
                                            indexes.Contains(k))
                                            {
                                                valid = false;
                                                break;
                                            }
                                        }
                                        if (valid)
                                        {
                                            indexList = new List<int>();
                                            indexList.AddRange(new int[] { i, j, k });
                                            indexTriplets.Add(indexList);

                                            innerList = new List<int>();
                                            innerList.AddRange(new int[] { numA, numB, numC });

                                            contains = outPutList.Any(list => list.OrderBy(x => x).
                                                        SequenceEqual(innerList.OrderBy(x => x)));
                                            if (!contains)
                                            {
                                                outPutList.Add(innerList);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }*/
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            IList<IList<int>> iList = outPutList.Select(list => (IList<int>)list).ToList();
            return iList;
        }

        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            /*
                THIS SOLUTION INCREASES THE PERFORMANCE OF THE PROGRAM A LOT BY :

                - Sort the given array in non-decreasing order.
                - Loop through the array from index 0 to n-1.
                - For each iteration, set the target as -nums[i].
                - Set two pointers, j=i+1 and k=n-1.
                - While j<k, check if nums[j]+nums[k]==target.
                - If yes, add the triplet {nums[i], nums[j], nums[k]} to the result and move 
                  j to the right and k to the left.
                - If no, move either j or k based on the comparison of nums[j]+nums[k] with target.
                - To avoid duplicate triplets, skip the iterations where nums[i]==nums[i-1] and also skip 
                  the iterations where nums[j]==nums[j-1] or nums[k]==nums[k+1].
            */

            List<IList<int>> result = new List<IList<int>>();

            // Sort the array in non-decreasing order
            Array.Sort(nums);

            for (int i = 0; i < nums.Length - 2; i++)
            {
                // Skip duplicates for i
                if (i > 0 && nums[i] == nums[i - 1])
                    continue;

                int target = -nums[i];
                int left = i + 1;
                int right = nums.Length - 1;

                while (left < right)
                {
                    int sum = nums[left] + nums[right];

                    if (sum == target)
                    {
                        result.Add(new List<int> { nums[i], nums[left], nums[right] });

                        // Move left pointer to the right, and right pointer to the left
                        left++;
                        right--;

                        // Skip duplicates for left and right
                        while (left < right && nums[left] == nums[left - 1])
                            left++;

                        while (left < right && nums[right] == nums[right + 1])
                            right--;
                    }
                    else if (sum < target)
                    {
                        left++;
                    }
                    else
                    {
                        right--;
                    }
                }
            }

            return result;
        }


        public static void TripletSum_Test()
        {
            int[] input = { -13, 11, 11, 0, -5, -14, 12, -11, -11, -14, -3, 0, -3, 12, -1, -9, -5, -13, 9, -7, -2, 9, -1, 4, -6, -13, -7, 10, 10, 9, 7, 13, 5, 4, -2, 7, 5, -13, 11, 10, -12, -14, -5, -8, 13, 2, -2, -14, 4, -8, -6, -13, 9, 8, 6, 10, 2, 6, 5, -10, 0, -11, -12, 12, 8, -7, -4, -9, -13, -7, 8, 12, -14, 10, -10, 14, -3, 3, -15, -14, 3, -14, 10, -11, 1, 1, 14, -11, 14, 4, -6, -1, 0, -11, -12, -14, -11, 0, 14, -9, 0, 7, -12, 1, -6 };
            
            //Valid function calls : TripletSum(), ThreeSum() -> better one
            
            IList<IList<int>> output = ThreeSum(input);
            //print the output int the console using the nested List formatting 
            //separing the different list inside the output with patenthesis each
            for (int i = 0; i < output.Count(); i++)
            {
                if (i == 0)
                {
                    Console.Write("[");
                }
                Console.Write("[");
                for (int j = 0; j < output[i].Count(); j++)
                {
                    Console.Write(output[i][j]);
                    if (j != output[i].Count() - 1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.Write("]");
                if (i != output.Count() - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.Write("]");
                }

            }
        }

        /* New problem 
        */

        static void Main(string[] args)
        {
            // Valid parenthesis problem test
            //ValidPar_Test();

            // Triplet sum problem test
            TripletSum_Test();

        }
    }
}
