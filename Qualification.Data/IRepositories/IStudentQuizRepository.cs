using Qualification.Domain.Entities.Quizes;

namespace Qualification.Data.IRepositories;

public interface IStudentQuizRepository
{
    IQueryable<QuizForStudent> SelectAllStudentQuizzes();
    ValueTask<QuizForStudent> SelectStudentQuizByIdAsync(long quizId);
    ValueTask<QuizForStudent> InsertStudentQuizAync(QuizForStudent quiz);
    ValueTask<QuizForStudent> UpdateStudentQuizAsync(QuizForStudent quiz);
    ValueTask<QuizForStudent> DeleteStudentQuizAsync(QuizForStudent quiz);
    ValueTask InsertBulkStudentQuizAsync(IEnumerable<QuizForStudent> quizes);
}