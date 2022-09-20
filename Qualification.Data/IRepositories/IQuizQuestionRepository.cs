using Qualification.Domain.Entities.Quizes;

namespace Qualification.Data.IRepositories;

public interface IQuizQuestionRepository
{
    IQueryable<QuizQuestion> SelectAllQuizes();
    ValueTask<QuizQuestion> SelectQuestionByIdAsync(long questionId);
    ValueTask<QuizQuestion> InsertQuizQuestionAsync(QuizQuestion quizQuestion);
    ValueTask<QuizQuestion> UpdateQuizQuestionAsync(QuizQuestion quizQuestion);
    ValueTask<QuizQuestion> DeleteQuizQuestionAsync(QuizQuestion quizQuestion);
}
