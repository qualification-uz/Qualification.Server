namespace Qualification.Service.DTOs.Quizzes;

public class CheckedQuizResultDto
{
    public int CorrectCount { get; set; }
    public int InCorrectCount { get; set; }
    public int Percent { get; set; }
    public ICollection<Domain.Entities.Questions.Question> InCorrectQuestions { get; set; }
}