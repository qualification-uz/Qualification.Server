﻿using Qualification.Domain.Enums;

namespace Qualification.Service.DTOs.Question;

public class QuestionDto
{
    public long Id { get; set; }
    public string Content { get; set; }
    public QuestionType Type { get; set; }
    public int SubjectId { get; set; }
    public short CorrectAnswers { get; set; }
    public short Level { get; set; }
    public bool IsForTeacher { get; set; }

    public IReadOnlyList<long> AssetIds { get; set; }
    public IReadOnlyList<string> AssetUrls { get; set; }
    public ICollection<QuestionAnswerDto> Answers { get; set; }
}
