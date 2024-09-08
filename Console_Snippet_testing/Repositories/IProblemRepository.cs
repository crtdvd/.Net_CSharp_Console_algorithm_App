using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Console_Snippet_testing.Models;

namespace Console_Snippet_testing.Repositories
{
    public interface IProblemRepository
    {
        IEnumerable<Problem> GetAll();
        IEnumerable<Problem> Search(string query);
        void Add(Problem problem);
    }
}