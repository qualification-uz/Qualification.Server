namespace Qualification.Service.DTOs.Quizzes;

public class SubmissionForStudentDto
{
    public long Id { get; set; }
    public long QuizId { get; set; }
    public long QuizQuestionId { get; set; }
    public long StudentId { get; set; }
    public long QuestionOptionId { get; set; }
}
