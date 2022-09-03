using Qualification.Service.DTOs.Application;

namespace Qualification.Service.Interfaces;

public interface IApplicationService
{
    ValueTask<ApplicationDto> AddApplicationAsync(ApplicationForCreationDto applicationDto);
    IEnumerable<ApplicationDto> RetrieveAllApplications();
    ValueTask<ApplicationDto> RetrieveApplicationByIdAsync(long applicationId);
    
    ValueTask<ApplicationDto> ModifyApplicationAsync(
        long applicationId,
        ApplicationForUpdateDto applicationDto);
    
    ValueTask<ApplicationDto> RemoveApplicationAsync(long applicationId);
}
