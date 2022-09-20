using Qualification.Domain.Entities.Questions;

namespace Qualification.Service.Interfaces;

public interface IQuizService
{
    IEnumerable<Question> GetAll(long subjectId, bool isForTeacher);
}