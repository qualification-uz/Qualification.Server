namespace Qualification.Domain.Entities.Quizes;

public class SubmissionResult
{
    public long Id { get; set; }
    public long? QuizId { get; set; }
    public long? QuizForStudentId { get; set; }
    public long QuizQuestionId { get; set; }
    public long? UserId { get; set; }
    public long QuestionOptionId { get; set; }
    public bool IsCorrect { get; set; }
    public bool IsForStudent { get; set; }
    public long? StudentId { get; set; }
}
