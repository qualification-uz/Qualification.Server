using Microsoft.AspNetCore.Http;
using Qualification.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    [Required]
    public short CorrectAnswers { get; set; }
    
    [Required]
    public short Level { get; set; }

    public ICollection<IFormFile> Assets { get; set; }
}
