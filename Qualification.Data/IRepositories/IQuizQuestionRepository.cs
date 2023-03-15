using Qualification.Domain.Entities.Quizes;

namespace Qualification.Data.IRepositories;

public interface IQuizQuestionRepository
{
    ValueTask<QuizQuestion> InsertQuizQuestionAsync(QuizQuestion quizQuestion);
    IQueryable<QuizQuestion> SelectAllQuizQuestions();
    ValueTask<QuizQuestion> SelectQuizQuestionByIdAsync(long quizQuestionId);
    ValueTask<QuizQuestion> UpdateQuizQuestionAsync(QuizQuestion quizQuestion);
    ValueTask<QuizQuestion> DeleteQuizQuestionAsync(QuizQuestion quizQuestion);
}