using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Payments;

public class Payment : Auditable
{
    public decimal Price { get; set; }
    public long AssetId { get; set; }

    public long ApplicationId { get; set; }
    public Application Application { get; set; }
}
