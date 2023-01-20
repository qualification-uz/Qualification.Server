using Qualification.Domain.Configurations;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Service.Interfaces
{
    public interface IReportService
    {
        Task<MemoryStream> ExportSchoolsToExcelAsync(PaginationParams @params, Filters filter);
        Task<MemoryStream> ExportTeachersToExcelAsync(int schoolId, PaginationParams @params, Filters filter);
        
        Task<MemoryStream> ExportApplicationsToExcelAsync(PaginationParams @params, Filters filter);
        Task<MemoryStream> ExportApplicationsBySchoolIdToExcelAsync(int schoolId, PaginationParams @params, Filters filter);
        Task<MemoryStream> ExportApplicationsByTeacherIdToExcelAsync(int teacherId, PaginationParams @params, Filters filter);

        Task<MemoryStream> ExportPaymentsToExcelAsync(PaginationParams @params, Filters filter);
    }
}
