using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Quizes;

public class QuizQuestionAsset : Auditable
{
    public long AssetId { get; set; }

    public long QuizQuestionId { get; }
    public QuizQuestion Question { get; }
}
