using Qualification.Service.DTOs.Quizzes;

namespace Qualification.Service.Interfaces;

public interface IQuizResultService
{
    ValueTask<QuizResultDto> RetrieveQuizResultAsync(long quizId);
}