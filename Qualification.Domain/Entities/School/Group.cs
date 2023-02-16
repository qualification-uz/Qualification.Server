using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Users;

namespace Qualification.Domain.Entities;

public class Group : Auditable
{
    public string Grade { get; set; }
    public int GradeId { get; set; }

    public string GradeLetter { get; set; }
    public int GradeLetterId { get; set; }

    public long ApplicationId { get; }
    public Application Application { get; }

    public string SchoolYear { get; set; }
    public int SchoolYearId { get; set; }
}