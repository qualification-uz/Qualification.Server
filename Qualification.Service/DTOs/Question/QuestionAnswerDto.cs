namespace Qualification.Service.DTOs.Question;

public class QuestionAnswerDto
{
    public long Id { get; set; }
    public string Content { get; set; }
    public bool IsCorrect { get; set; }

    public IReadOnlyList<long> AssetIds { get; set; }
    public IReadOnlyList<string> AssetUrls { get; set; }
}