using Console_Snippet_testing.Repositories;
using Console_Snippet_testing.Services;
using Console_Snippet_testing.UI;


namespace ProblemSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            IProblemRepository repository = new ProblemRepository(); 
            IProblemService service = new ProblemService(repository);
            ConsoleUI ui = new ConsoleUI(service);

            ui.Run();
        }
    }
}
