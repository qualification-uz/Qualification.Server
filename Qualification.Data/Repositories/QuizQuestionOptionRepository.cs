using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Questions;
using Qualification.Domain.Entities.Quizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Data.Repositories
{
    public class QuizQuestionOptionRepository : IQuizQuestionOptionRepository
    {
        private readonly AppDbContext appDbContext;

        public QuizQuestionOptionRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public IQueryable<QuestionOption> SelectAllQuizQuestions()
            => this.appDbContext.QuizQuestionOptions;
    }
}
