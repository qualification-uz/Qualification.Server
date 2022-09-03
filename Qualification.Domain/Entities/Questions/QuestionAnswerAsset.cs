using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Questions;

public class QuestionAnswerAsset : Auditable
{
    public string AssetUrl { get; set; }

    public long QuestionAnswerId { get; }
    public QuestionAnswer QuestionAnswer { get; }
}
