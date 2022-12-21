namespace Qualification.Service.DTOs.Quizzes;

public class QuizForBulkCreationDto
{
    public string Title { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }

    public ICollection<QuizDataDto> Datas { get; set; }
}

public class QuizDataDto
{
    public long UserId { get; set; }
    public long ApplicationId { get; set; }
}