using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Assets;

namespace Qualification.Domain.Entities.Questions;

public class QuestionAnswer : Auditable
{
    public string Content { get; set; }
    public bool IsCorrect { get; set; }

    public long QuestionId { get; }
    public Question Question { get; }

    public ICollection<QuestionAnswerAsset> Assets { get; set; }
}
