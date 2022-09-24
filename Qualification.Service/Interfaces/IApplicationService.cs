using Qualification.Domain.Configurations;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;

namespace Qualification.Service.Interfaces;

public interface IApplicationService
{
    ValueTask<ApplicationDto> AddApplicationAsync(ApplicationForCreationDto applicationDto);
    IEnumerable<ApplicationDto> RetrieveAllApplications(PaginationParams @params, Filter filter);
    ValueTask<ApplicationDto> RetrieveApplicationByIdAsync(long applicationId);
    
    ValueTask<ApplicationDto> ModifyApplicationAsync(
        long applicationId,
        ApplicationForUpdateDto applicationDto);
    
    ValueTask<ApplicationDto> RemoveApplicationAsync(long applicationId);
    ValueTask<ApplicationDto> ModifyApplicationStatusAsync(long applicationId, ApplicationStatus status);
}
