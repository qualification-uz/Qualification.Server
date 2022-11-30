using Newtonsoft.Json;

namespace Qualification.Service.DTOs.Users
{
    public class TeacherFromErpDto
    {
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        
        [JsonProperty("familyname")]
        public string LastName { get; set; }
        
        [JsonProperty("lastname")]
        public string MiddleName { get; set; }

        [JsonProperty("contactinfo")]
        public string PhoneNumber { get; set; }
    }
}
