using Qualification.Domain.Configurations;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using Qualification.Service.DTOs.Users;

namespace Qualification.Service.Interfaces;

public interface IApplicationService
{
    ValueTask<ApplicationDto> AddApplicationAsync(ApplicationForCreationDto applicationDto);
    IEnumerable<ApplicationDto> RetrieveAllApplications(PaginationParams @params, Filters filters);
    ValueTask<ApplicationDto> RetrieveApplicationByIdAsync(long applicationId);
    
    ValueTask<ApplicationDto> ModifyApplicationAsync(
        long applicationId,
        ApplicationForUpdateDto applicationDto);
    
    ValueTask<ApplicationDto> RemoveApplicationAsync(long applicationId);
    ValueTask<ApplicationDto> ModifyApplicationStatusAsync(long applicationId, ApplicationStatus status);
    IEnumerable<RoleDto> RetrieveAllApplicationStatus();
    IEnumerable<ApplicationDto> RetrieveApplicationsForSchool(
        long schoolId,
        PaginationParams @params,
        Filters filter);

    IEnumerable<ApplicationDto> RetrieveApplicationsForTeacher(
        long teacherId,
        PaginationParams @params,
        Filters filters);
}
