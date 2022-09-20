using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Users;

namespace Qualification.Domain.Entities.Quizes;

public class Submission : Auditable
{
    public long QuizId { get; }
    public Quiz Quiz { get; }

    public long QuizQuestionId { get; }
    public QuizQuestion Question { get; }

    public long UserId { get; }
    public User User { get; }

    public long QuestionOptionId { get; }
    public QuestionOption Option { get; }
}