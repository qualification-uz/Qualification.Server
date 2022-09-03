using Newtonsoft.Json;
using Qualification.Service.DTOs.Users;

namespace Qualification.Service.DTOs;

public class ERPResponse<T>
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("resultCount")]
    public int ResultCount { get; set; }

    [JsonProperty("result")]
    public T Result { get; set; }

    [JsonProperty("errorMessages")]
    public string ErrorMessage { get; set; }
}
