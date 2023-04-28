using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Users;

namespace Qualification.Domain.Entities.Quizes;

public class QuizResult : Auditable
{
    public long? UserId { get; set; }
    public User User { get; set; }

    public long? StudentId { get; set; }
    public Student Student { get; set; }

    public long? QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public long? QuizForStudentId { get; set; }
    public QuizForStudent QuizForStudent { get; set; }

    public short CorrectAnswers { get; set; }
    public int Score { get; set; }
}