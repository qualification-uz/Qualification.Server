using Qualification.Domain.Enums;

namespace Qualification.Service.DTOs.Users
{
    public class UserDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public UserRole RoleId { get; set; }
        public string Role { get; set; }
    }
}
