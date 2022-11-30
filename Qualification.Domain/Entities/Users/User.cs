using Microsoft.AspNetCore.Identity;
using Qualification.Domain.Entities.Assets;
using Qualification.Domain.Entities.Payment;

namespace Qualification.Domain.Entities.Users;

public class User : IdentityUser<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }

    public int SchoolId { get; set; }
    public long? AssetId { get; set; }

    public ICollection<Application> Applications { get; set; }
    public ICollection<PaymentRequest> PaymentRequests { get; set; }
}
