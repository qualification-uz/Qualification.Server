using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Assets;

namespace Qualification.Domain.Entities.Questions;

public class QuestionAnswerAsset : Auditable
{
    public long AssetId { get; set; }
    public Asset Asset { get; set; }
    public long QuestionAnswerId { get; }
    public QuestionAnswer QuestionAnswer { get; }
}
