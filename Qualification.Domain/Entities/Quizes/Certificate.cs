using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Quizes;

public class Certificate : Auditable
{
    public long? UserId { get; set; }
    public long? ApplicationId { get; set; }
    public double SubjectScore { get; set; }
    public double PedagogicalScore { get; set; }
    public string Code { get; set; }
    public DateTime DateIssued { get; set; }
}