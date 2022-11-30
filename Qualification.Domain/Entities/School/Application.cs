using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Assets;
using Qualification.Domain.Entities.Payment;
using Qualification.Domain.Entities.Quizes;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;

namespace Qualification.Domain.Entities;

public class Application : Auditable
{
    public int SubjectId { get; set; }
    public long SchoolId { get; set; }

    public long DocumentId { get; set; }

    public User Teacher { get; }
    public long TeacherId { get; }

    public ApplicationStatus Status { get; set; } = ApplicationStatus.Yuborildi;

    public ICollection<Group> Groups { get; set; }
    public ICollection<Quiz> Quizes { get; set; }

    public ICollection<PaymentRequest> PaymentRequests { get; set; }
}