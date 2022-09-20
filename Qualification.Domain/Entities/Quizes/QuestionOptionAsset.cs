using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Quizes;

public class QuestionOptionAsset : Auditable
{
    public long AssetId { get; set; }

    public long QuestionOptionId { get; }
    public QuestionOption QuestionOption { get; }
}
