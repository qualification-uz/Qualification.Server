using Qualification.Domain.Entities.Questions;

namespace Qualification.Data.IRepositories;

public interface IQuestionRepository
{
    IQueryable<Question> SelectAllQuestions();
    ValueTask<Question> SelectQuestionByIdAsync(long questionId);
    ValueTask<Question> InsertQuestionAsync(Question question);
    ValueTask<Question> UpdateQuestionAsync(Question question);
    ValueTask<Question> DeleteQuestionAsync(Question question);
}
