﻿using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Users;

namespace Qualification.Domain.Entities.Quizes;

public class QuizResult : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public short CorrectAnswers { get; set; }
    public int Score { get; set; }
}