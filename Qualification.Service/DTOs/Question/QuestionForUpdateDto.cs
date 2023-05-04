using Qualification.Domain.Enums;

namespace Qualification.Service.DTOs.Question;

public class QuestionForUpdateDto
{
    public string Content { get; set; }
    public int? SubjectId { get; set; }
    public short? CorrectAnswers { get; set; }
    public bool? IsForTeacher { get; set; }
    public IReadOnlyList<long> AssetIds { get; set; }
}
