using Qualification.Domain.Enums;
using Qualification.Service.DTOs.Question;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.DTOs.Users;

namespace Qualification.Service.Interfaces;

public interface IQuizService
{
    ValueTask<QuizDto> CreateQuizAsync(QuizForCreationDto quizDto);
    ValueTask CreateBulkQuizAsync(QuizForBulkCreationDto quizForBulkCreationDto);
    IEnumerable<QuizDto> RetrieveAllQuizzes();
    ValueTask<QuizDto> RetrieveQuizByIdAsync(long quizId);
    ValueTask<QuizDto> ModifyQuizAsync(long quizId, QuizForUpdateDto quizDto);
    ValueTask<QuizDto> RemoveQuizAsync(long quizId);
    ValueTask<QuizDto> ModifyQuizStatusAsync(long quizId, QuizStatus quizStatus);
    ValueTask<IEnumerable<QuizQuestionDto>> RetrieveQuizQuestions(long quizId);
    IEnumerable<RoleDto> RetrieveQuizStatuses();
}