using Qualification.Domain.Commons;
using Qualification.Domain.Entities.Users;

namespace Qualification.Domain.Entities.Payment;

public class PaymentRequest : Auditable
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    
    public long ApplicationId { get; set; }
    public Application Application { get; }
    
    public long UserId { get; set; }
    public User User { get; }

    public ICollection<PaymentAsset> Assets { get; set; }
}