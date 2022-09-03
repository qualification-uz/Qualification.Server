using Newtonsoft.Json;

namespace Qualification.Service.DTOs.Application;

public class SubjectDto
{
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }

    [JsonProperty(PropertyName = "nameuz")]
    public string Name { get; set; }
}