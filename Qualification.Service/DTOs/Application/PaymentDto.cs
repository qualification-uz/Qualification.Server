namespace Qualification.Service.DTOs.Application;

public class PaymentDto
{
    public long Id { get; set; }
    public decimal Price { get; set; }
    public long AssetId { get; set; }
    public ApplicationDto Application { get; set; }
}
