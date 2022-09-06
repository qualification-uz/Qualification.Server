using Qualification.Domain.Configurations;
using Qualification.Service.DTOs.Question;

namespace Qualification.Service.Interfaces;

public interface IQuestionService
{
    IEnumerable<QuestionDto> RetrieveAllQuestions(PaginationParams @params);
    ValueTask<QuestionDto> RetrieveQuestionByIdAsync(long questionId);
    ValueTask<QuestionDto> AddQuestionAsync(QuestionForCreationDto questionForCreationDto);
    ValueTask<QuestionDto> ModifyQuestionAsync(long questionId, QuestionForUpdateDto questionForUpdateDto);
    ValueTask<QuestionDto> RemoveQuestionAsync(long questionId);
    
    ValueTask<QuestionDto> AddQuestionAnswersAsync(
        long questionId,
        IReadOnlyList<QuestionAnswerForCreationDto> questionAnswerForCreationDto);

    ValueTask<QuestionDto> AddQuestionAnswerAsync(
        long questionId,
        QuestionAnswerForCreationDto questionAnswerForCreationDto);

    ValueTask<QuestionDto> ModifyQuestionAnswerAsync(
        long questionId,
        long answerId,
        QuestionAnswerForUpdateDto questionAnswerForUpdateDto);
}
