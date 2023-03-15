using Qualification.Domain.Entities.Quizes;

namespace Qualification.Data.IRepositories;

public interface IQuizRepository
{
    IQueryable<Quiz> SelectAllQuizzes();
    ValueTask<Quiz> SelectQuizByIdAsync(long quizId);
    ValueTask<Quiz> InsertQuizAync(Quiz quiz);
    ValueTask<Quiz> UpdateQuizAsync(Quiz quiz);
    ValueTask<Quiz> DeleteQuizAsync(Quiz quiz);
    ValueTask InsertBulkQuizAsync(IEnumerable<Quiz> quizes);
}