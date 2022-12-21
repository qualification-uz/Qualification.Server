using Qualification.Service.DTOs.Quizzes;

namespace Qualification.Service.Interfaces;

public interface ISubmissionService
{
    ValueTask<SubmissionDto> CreateSubmissionAsync(SubmissionForCreationDto submissionDto);
    IEnumerable<SubmissionDto> RetrieveAllSubmissions();
    ValueTask<SubmissionDto> RetrieveSubmissionByIdAsync(long submissionId);
    ValueTask<SubmissionDto> ModifySubmissionAsync(long submissionId, SubmissionForUpdateDto submissionDto);
    ValueTask<SubmissionDto> RemoveSubmissionAsync(long submissionId);
}
