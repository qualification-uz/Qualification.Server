using Qualification.Domain.Entities.Questions;

namespace Qualification.Data.IRepositories;

public interface IQuestionAnswerRepository
{
    IQueryable<QuestionAnswer> SelectAllQuestions();
    ValueTask<QuestionAnswer> SelectQuestionByIdAsync(long questionAnswerId);

    ValueTask<QuestionAnswer> SelectQuestionByIdAsync(
        long questionAnswerId,
        IReadOnlyList<string> includes);

    ValueTask<QuestionAnswer> InsertQuestionAsync(QuestionAnswer questionAnswer);
    ValueTask<QuestionAnswer> UpdateQuestionAsync(QuestionAnswer questionAnswer);
    ValueTask<QuestionAnswer> DeleteQuestionAsync(QuestionAnswer questionAnswer);
}