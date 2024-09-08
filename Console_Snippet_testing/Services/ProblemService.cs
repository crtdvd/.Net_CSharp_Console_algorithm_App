using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Console_Snippet_testing.Models;
using Console_Snippet_testing.Repositories;

namespace Console_Snippet_testing.Services
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _repository;

        public ProblemService(IProblemRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Problem> GetAllProblems() => _repository.GetAll();

        public IEnumerable<Problem> SearchProblems(string query) => _repository.Search(query);
        
    }
}