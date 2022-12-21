using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;

namespace Qualification.Domain.Entities.Quizes;

public class Quiz : Auditable
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int QuestionCount { get; set; }
    public bool IsForTeacher { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public QuizStatus Status { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public long ApplicationId { get; set; }
    public Application Application { get; set; }

    public ICollection<Submission> Submissions { get; set; }
    public ICollection<QuizQuestion> Questions { get; set; }
}
