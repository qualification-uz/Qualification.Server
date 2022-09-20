using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Quizes;

public class QuizQuestion : Auditable
{
    public string Content { get; set; }

    public long QuizId { get; }
    public Quiz Quiz { get; }

    public ICollection<QuestionOption> Options { get; set; }
    public ICollection<QuizQuestionAsset> Assets { get; set; }
}
