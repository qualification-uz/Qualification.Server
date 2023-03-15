using Qualification.Domain.Entities.Quizes;

namespace Qualification.Data.IRepositories;

public interface IQuizResultRepository
{
    ValueTask<QuizResult> InsertQuizResultAsync(QuizResult quizResult);
    IQueryable<QuizResult> SelectAllQuizResults();
    ValueTask<QuizResult> SelectQuizResultByIdAsync(long quizResultId);
    ValueTask<QuizResult> UpdateQuizResultAsync(QuizResult quizResult);
    ValueTask<QuizResult> DeleteQuizResultAsync(QuizResult quizResult);
}
