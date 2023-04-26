using Qualification.Domain.Configurations;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.DTOs.Sertificate;
using Qualification.Service.DTOs.Users;

namespace Qualification.Service.Interfaces;

public interface IQuizService
{
    ValueTask<QuizDto> CreateQuizAsync(QuizForCreationDto quizDto);
    ValueTask<QuizeForStudentDto> CreateStudentQuizAsync(QuizForStudentCreationDto quizDto);
    ValueTask CreateBulkQuizAsync(QuizForBulkCreationDto quizForBulkCreationDto);
    IEnumerable<QuizDto> RetrieveAllQuizzes(Filters filter, PaginationParams @params);
    ValueTask<QuizDto> RetrieveQuizByPropertyValue(Filter filter);
    ValueTask<QuizDto> RetrieveQuizByIdAsync(long quizId);
    ValueTask<QuizDto> RetrieveQuizByApplicationIdAsync(long applicationId);
    ValueTask<QuizDto> ModifyQuizAsync(long quizId, QuizForUpdateDto quizDto);
    ValueTask<QuizDto> RemoveQuizAsync(long quizId);
    ValueTask<QuizDto> ModifyQuizStatusAsync(long quizId, QuizStatus quizStatus);
    ValueTask<IEnumerable<QuizQuestionDto>> RetrieveQuizQuestions(long quizId);
    ValueTask<IEnumerable<QuizQuestionDto>> RetrieveQuizQuestionsByApplicationId(long applicationId, long studentGradeId);
    IEnumerable<RoleDto> RetrieveQuizStatuses();
    ValueTask<IEnumerable<QuizDto>> RetrieveQuizByTeacherId(long teacherId, PaginationParams paginationParams);
    ValueTask<byte[]> GenerateSertificateAsync(long quizId);
}