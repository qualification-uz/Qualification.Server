using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Service.DTOs.Users
{
    public class TeacherFromErpDto
    {
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        
        [JsonProperty("familyname")]
        public string FamilyName { get; set; }

        [JsonProperty("contactinfo")]
        public string ContactInfo { get; set; }
    }
}
