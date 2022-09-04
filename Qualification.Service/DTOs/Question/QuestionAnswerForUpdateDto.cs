using System.ComponentModel.DataAnnotations;

namespace Qualification.Service.DTOs.Question;

public class QuestionAnswerForUpdateDto
{
    public string Content { get; set; }
    public bool? IsCorrect { get; set; }
}
