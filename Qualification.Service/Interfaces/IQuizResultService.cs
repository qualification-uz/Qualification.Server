using Qualification.Service.DTOs.Quizzes;

namespace Qualification.Service.Interfaces;

public interface IQuizResultService
{
    ValueTask<QuizResultDto> RetrieveTeacherQuizResultAsync(long quizId, long? studentId = null);
    ValueTask<QuizResultDto> RetrieveStudentQuizResultAsync(long quizId, long studentId);
}