namespace Qualification.Service.DTOs.Payment;

public class PaymentRequestDto
{
    public long Id { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public long ApplicationId { get; set; }
    public long UserId { get; set; }
    public ICollection<PaymentAssetDto> Assets { get; set; }
}