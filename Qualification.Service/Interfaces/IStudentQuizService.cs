using Qualification.Domain.Configurations;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.DTOs;

namespace Qualification.Service.Interfaces;

public interface IStudentQuizService
{
    ValueTask<QuizeForStudentDto> CreateQuizAsync(QuizForStudentCreationDto quizDto);
    IEnumerable<QuizeForStudentDto> RetrieveAllQuizzes(Filters filter, PaginationParams @params);
    ValueTask<QuizeForStudentDto> RetrieveQuizByIdAsync(long quizId);
    ValueTask<List<QuizeForStudentDto>> RetrieveQuizByApplicationIdAsync(long applicationId);
    ValueTask<QuizeForStudentDto> ModifyQuizAsync(long quizId, QuizForUpdateDto quizDto);
    ValueTask<QuizeForStudentDto> RemoveQuizAsync(long quizId);
    ValueTask<QuizeForStudentDto> ModifyQuizStatusAsync(long quizId, QuizStatus quizStatus);
    ValueTask<IEnumerable<QuizQuestionDto>> RetrieveQuizQuestions(long quizId);
}
