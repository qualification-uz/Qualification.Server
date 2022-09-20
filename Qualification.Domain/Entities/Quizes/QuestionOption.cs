using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Quizes;

public class QuestionOption : Auditable
{
    public long QuizQuestionId { get; set; }
    public long QuizOptionId { get; set; }
    public short ShufflePosition { get; set; }
}
