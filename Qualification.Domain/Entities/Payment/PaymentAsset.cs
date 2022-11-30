using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Payment;

public class PaymentAsset : Auditable
{
    public long AssetId { get; set; }
    public bool IsFromAdmin { get; set; }
    public long PaymentRequestId { get; }
    public PaymentRequest PaymentRequest { get; }
}