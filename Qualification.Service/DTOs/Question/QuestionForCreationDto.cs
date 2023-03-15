using Qualification.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Qualification.Service.DTOs.Question;

public class QuestionForCreationDto
{
    [Required]
    public string Content { get; set; }

    [Required]
    public QuestionType Type { get; set; }

    [Required]
    public int SubjectId { get; set; }

    [Required]
    public bool IsForTeacher { get; set; }

    public long? StudentGradeId { get; set; }

    [Required]
    public short CorrectAnswers { get; set; }

    [Required]
    public short Level { get; set; }

    public IReadOnlyList<long> AssetIds { get; set; }
}
