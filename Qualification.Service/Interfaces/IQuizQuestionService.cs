using Qualification.Service.DTOs.Quizzes;

namespace Qualification.Service.Interfaces;

public interface IQuizQuestionService
{
    IQueryable<QuizQuestionDto> RetrieveAllQuizQuestions();
    ValueTask<QuizQuestionDto> RetrieveQuizQuestionByIdAsync(long quizQuestionId);
}