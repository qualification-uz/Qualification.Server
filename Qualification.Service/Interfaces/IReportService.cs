using Qualification.Domain.Configurations;
using Qualification.Service.DTOs;

namespace Qualification.Service.Interfaces
{
    public interface IReportService
    {
        Task<MemoryStream> ExportTeachersToExcelAsync(int schoolId, PaginationParams @params, Filters filter);

        Task<MemoryStream> ExportApplicationsToExcelAsync(PaginationParams @params, Filters filter);
        Task<MemoryStream> ExportApplicationsBySchoolIdToExcelAsync(int schoolId, PaginationParams @params, Filters filter);
        Task<MemoryStream> ExportApplicationsByTeacherIdToExcelAsync(int teacherId, PaginationParams @params, Filters filter);

        Task<MemoryStream> ExportPaymentsToExcelAsync(PaginationParams @params, Filters filter);
        Task<MemoryStream> ExportStudentsToExcelAsync(int schoolId, int applicationId);
    }
}
