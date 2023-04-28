using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Users;

namespace Qualification.Domain.Entities.Quizes;

public class Submission : Auditable
{
    public long? QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public long? QuizForStudentId { get; set; }
    public QuizForStudent QuizForStudent { get; set; }

    public long QuizQuestionId { get; set; }
    public QuizQuestion Question { get; set; }

    public long? UserId { get; set; }
    public User User { get; set; }

    public long QuestionOptionId { get; set; }
    public QuestionOption Option { get; set; }

    public bool IsCorrect { get; set; } 
    public bool IsForStudent { get; set; }

    public long? StudentId { get; set; }
    public Student Student { get; set; }
}