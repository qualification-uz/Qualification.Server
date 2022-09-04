using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Qualification.Service.DTOs.Question;

public class QuestionAnswerForCreationDto
{
    [Required]
    public string Content { get; set; }

    [Required]
    public bool IsCorrect { get; set; }

    public IReadOnlyList<IFormFile> Assets { get; set; }
}
