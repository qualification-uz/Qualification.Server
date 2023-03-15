namespace Qualification.Service.DTOs.Quizzes;

public class SubmissionForCreationDto
{
    public long QuizId { get; set; }
    public long QuizQuestionId { get; set; }
    public long UserId { get; set; }
    public long QuestionOptionId { get; set; }
}
