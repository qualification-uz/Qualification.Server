using System.ComponentModel.DataAnnotations;

namespace Qualification.Service.DTOs.Question;

public class QuestionAnswerForCreationDto
{
    [Required]
    public string Content { get; set; }

    [Required]
    public bool IsCorrect { get; set; }

    public IReadOnlyList<long> AssetIds { get; set; }
}
