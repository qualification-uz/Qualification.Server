using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities;

public class Group : Auditable
{
    public string Name { get; set; }

    public long ApplicationId { get; }
    public Application Application { get; }
}
