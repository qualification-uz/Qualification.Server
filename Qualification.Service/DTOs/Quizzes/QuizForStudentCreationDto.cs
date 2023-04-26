namespace Qualification.Service.DTOs.Quizzes;

public class QuizForStudentCreationDto
{
    public string Title { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public long StudentId { get; set; }
    public long ApplicationId { get; set; }
}