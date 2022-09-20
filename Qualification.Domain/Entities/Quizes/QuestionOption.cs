using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Quizes;

public class QuestionOption : Auditable
{
    public string Content { get; set; }
    public bool IsCorrect { get; set; }

    public long QuizQuestionId { get; }
    public QuizQuestion Question { get; }

    public ICollection<QuestionOptionAsset> Assets { get; set; }
}
