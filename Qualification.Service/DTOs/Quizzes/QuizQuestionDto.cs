using Qualification.Domain.Enums;

namespace Qualification.Service.DTOs.Quizzes;

public class QuizQuestionDto
{
    public long Id { get; set; }
    public string Content { get; set; }
    public QuestionType Type { get; set; }
    public int SubjectId { get; set; }
    public short Level { get; set; }
    public bool IsForTeacher { get; set; }
    public IReadOnlyList<string> AssetUrls { get; set; }
    public ICollection<QuizOptionDto> Answers { get; set; }
}