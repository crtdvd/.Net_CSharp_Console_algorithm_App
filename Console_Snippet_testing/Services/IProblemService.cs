

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Console_Snippet_testing.Models;

namespace Console_Snippet_testing.Services
{
    public interface IProblemService
    {
        IEnumerable<Problem> GetAllProblems();
        IEnumerable<Problem> SearchProblems(string query);
    }
}