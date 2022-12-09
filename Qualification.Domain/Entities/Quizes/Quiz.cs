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

    public long UserId { get; }
    public User User { get; }

    public long ApplicationId { get; }
    public Application Application { get; }

    public ICollection<Submission> Submissions { get; set; }
}
