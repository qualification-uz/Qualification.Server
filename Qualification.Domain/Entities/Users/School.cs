using Microsoft.AspNetCore.Identity;

namespace Qualification.Domain.Entities.Users;

public class School : User
{
    public string Name { get; set; }
    public int RegionId { get; set; }
    public int DistrictId { get; set; }
    public int TypeId { get; set; }
}
