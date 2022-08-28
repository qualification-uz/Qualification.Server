using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;

namespace Qualification.Domain.Entities;

public class Application : Auditable
{
    public short AttandancePercent { get; set; }
    public int SubjectId { get; set; }
    public long SchoolId { get; set; }
    public string DocumentUrl { get; set; }

    public User  Teacher { get; }
    public long TeacherId { get; }

    public ApplicationStatus Status { get; set; } = ApplicationStatus.Yuborildi;

    public ICollection<Group> Groups { get; set; }
}