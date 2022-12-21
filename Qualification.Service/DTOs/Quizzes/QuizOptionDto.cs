namespace Qualification.Service.DTOs.Quizzes;

public class QuizOptionDto
{
    public long Id { get; set; }
    public string Content { get; set; }

    public IReadOnlyList<long> AssetIds { get; set; }
}