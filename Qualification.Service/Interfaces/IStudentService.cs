using Qualification.Domain.Entities.Users;
using Qualification.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Service.Interfaces
{
    public interface IStudentService
    {
        ValueTask<IEnumerable<Student>> RetrieveAllAsync(long schoolId, long applicationId);
        ValueTask<object> RetrieveByPasswordAsync(string password);
    }
}
