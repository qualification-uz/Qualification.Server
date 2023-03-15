using Newtonsoft.Json;

namespace Qualification.Service.DTOs.Users;

public class StudentDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("firstname")]
    public string FirstName { get; set; }

    [JsonProperty("familyname")]
    public string LastName { get; set; }

    [JsonProperty("lastname")]
    public string MiddleName { get; set; }

    [JsonProperty("schoolgradename")]
    public string Grade { get; set; }

    [JsonProperty("schoolgradeid")]
    public int GradeId { get; set; }

    [JsonProperty("schoolgradelettername")]
    public string GradeLetter { get; set; }

    [JsonProperty("schoolgradeletterid")]
    public int GradeLetterId { get; set; }
}