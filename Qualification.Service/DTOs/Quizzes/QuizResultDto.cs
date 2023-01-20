namespace Qualification.Service.DTOs.Quizzes;

public class QuizResultDto
{
    public short CorrectAnswers { get; set; }
    public int Score { get; set; }

    public ICollection<SubmissionResultDto> Submissions { get; set; }
}
