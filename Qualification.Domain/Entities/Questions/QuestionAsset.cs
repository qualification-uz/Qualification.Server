using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Questions;

public class QuestionAsset : Auditable
{
    public string AssetUrl { get; set; }

    public long QuestionId { get; }
    public Question Question { get; }
}
