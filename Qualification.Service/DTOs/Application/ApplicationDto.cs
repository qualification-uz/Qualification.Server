using Qualification.Domain.Enums;
using Qualification.Service.DTOs.Payment;
using Qualification.Service.DTOs.Users;

namespace Qualification.Service.DTOs.Application;

public class ApplicationDto
{
    public long Id { get; set; }
    public int SubjectId { get; set; }
    public long SchoolId { get; set; }
    public long DocumentId { get; set; }
    public UserDto Teacher { get; set; }

    public ApplicationStatus Status { get; set; }
    public ICollection<GroupDto> Groups { get; set; }
    public ICollection<PaymentRequestDto> PaymentRequests { get; set; }
}
