namespace Qualification.Service.DTOs.Application;

public class PaymentForCreationDto
{
    public decimal Price { get; set; }
    public long AssetId { get; set; }
    public long ApplicationId { get; set; }
}
