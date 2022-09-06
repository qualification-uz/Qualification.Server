using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Assets;
using Qualification.Domain.Enums;

namespace Qualification.Domain.Entities.Questions;

public class Question : Auditable
{
    public string Content { get; set; }
    public QuestionType Type { get; set; }
    public int SubjectId { get; set; }
    public bool IsForTeacher { get; set; }
    public short CorrectAnswers { get; set; }
    public short Level { get; set; }
    public bool IsActive { get; set; }

    public ICollection<QuestionAnswer> Answers { get; set; }
    public ICollection<QuestionAsset> Assets { get; set; }
}
