namespace Qualification.Service.DTOs.Payment;

public class PaymentRequestForCreationDto
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public long ApplicationId { get; set; }
    public long UserId { get; set; }
    public int[] AssetIds { get; set; }
}