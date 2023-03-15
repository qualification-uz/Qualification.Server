namespace Qualification.Service.DTOs.Quizzes;

public class SubmissionForStudentForCreationDto
{
    public long QuizId { get; set; }
    public long QuizQuestionId { get; set; }
    public long QuestionOptionId { get; set; }
    public long StudentId { get; set; }
}
