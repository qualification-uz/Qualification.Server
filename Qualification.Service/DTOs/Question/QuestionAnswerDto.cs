using System.ComponentModel.DataAnnotations.Schema;

namespace Qualification.Service.DTOs.Question;

public class QuestionAnswerDto
{
    public long Id { get; set; }
    public string Content { get; set; }
    public bool IsCorrect { get; set; }

    public ICollection<AssetDto> Assets { get; set; }
}