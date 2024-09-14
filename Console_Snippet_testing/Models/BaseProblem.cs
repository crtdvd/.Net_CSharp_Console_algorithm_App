using System.Reflection;
using System.Reflection.Emit;

namespace Console_Snippet_testing.Problems
{
    public abstract class BaseProblem
    {
        public abstract string Description { get; }
        public abstract string SolutionMethodName { get; }
        public abstract string ClassName { get; }
        public abstract void Test();

        public string SolutionCode => GetMethodBody(SolutionMethodName);

        protected string GetMethodBody(string methodName)
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
            var filePath = Path.Combine(projectRoot, "Problems", $"{ClassName}.cs");

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
    }
}