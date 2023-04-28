using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Quizes;

public class QuizQuestion : Auditable
{
    public long QuestionId { get; set; }
    public long? QuizId { get; set; }
    public long? QuizForStudentId { get; set; }
    public short ShufflePosition { get; set; }

    public ICollection<QuestionOption> Options { get; set; }
}