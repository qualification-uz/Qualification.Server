using Newtonsoft.Json;
using Qualification.Service.DTOs.Users;

namespace Qualification.Service.DTOs;

public class ERPResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("resultCount")]
    public int ResultCount { get; set; }

    [JsonProperty("result")]
    public SchoolDto Result { get; set; }

    [JsonProperty("errorMessages")]
    public string ErrorMessage { get; set; }
}
