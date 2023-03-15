namespace Qualification.Service.DTOs.Quizzes;

public class QuizForCreationDto
{
    public string Title { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public long UserId { get; set; }
    public long ApplicationId { get; set; }
}
