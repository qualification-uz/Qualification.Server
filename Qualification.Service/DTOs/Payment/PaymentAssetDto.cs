namespace Qualification.Service.DTOs.Payment;

public class PaymentAssetDto
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public bool IsFromAdmin { get; set; }
    public long PaymentRequestId { get; }
}