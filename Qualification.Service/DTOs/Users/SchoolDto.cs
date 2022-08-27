using Newtonsoft.Json;

namespace Qualification.Service.DTOs.Users;

public class SchoolDto
{
    [JsonProperty("organizationid")]
    public long Id { get; set; }

    [JsonProperty("organizationname")]
    public string Name { get; set; }

    [JsonProperty("oblastid")]
    public int RegionId { get; set; }

    [JsonProperty("regionid")]
    public int DistrictId { get; set; }

    [JsonProperty("organizationtypeid")]
    public int TypeId { get; set; }

    [JsonProperty("usermobilenumber")]
    public string PhoneNumber { get; set; }
}
