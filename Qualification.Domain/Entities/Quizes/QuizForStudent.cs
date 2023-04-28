using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;

namespace Qualification.Domain.Entities.Quizes;

public class QuizForStudent : Auditable
{
    public string Title { get; set; }
    public string Content { get; set; }
    public int QuestionCount { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public QuizStatus Status { get; set; }
    public long StudentId { get; set; }
    public long ApplicationId { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; }
    public virtual ICollection<QuizQuestion> Questions { get; set; }
}