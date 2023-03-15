using Qualification.Domain.Entities.Quizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Data.IRepositories
{
    public interface IQuizQuestionOptionRepository
    {
        IQueryable<QuestionOption> SelectAllQuizQuestions();
    }
}
