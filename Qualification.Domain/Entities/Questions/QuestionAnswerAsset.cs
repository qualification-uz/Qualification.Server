using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Questions;

public class QuestionAnswerAsset : Auditable
{
    public long AssetId { get; set; }
    public long QuestionAnswerId { get; }
    public QuestionAnswer QuestionAnswer { get; }
}
