using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Assets;

namespace Qualification.Domain.Entities.Questions;

public class QuestionAsset : Auditable
{
    public long AssetId { get; set; }
    public Asset Asset { get; set; }
    public long QuestionId { get; }
    public Question Question { get; }
}
