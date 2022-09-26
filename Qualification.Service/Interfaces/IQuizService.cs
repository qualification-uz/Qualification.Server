using Qualification.Domain.Entities.Questions;
using Qualification.Service.DTOs.Quizzes;

namespace Qualification.Service.Interfaces;

public interface IQuizService
{
    IEnumerable<Question> GetAll(long subjectId, bool isForTeacher);
    Task<CheckedQuizResultDto> CheckQuizAsync(CheckedQuizInputDto[] answers);
}