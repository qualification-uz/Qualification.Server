using Qualification.Service.DTOs.Quizzes;

namespace Qualification.Service.Interfaces;

public interface ISubmissionService
{
    ValueTask<SubmissionDto> CreateSubmissionAsync(SubmissionForCreationDto submissionDto);
    ValueTask<SubmissionForStudentDto> CreateSubmissionForStudentAsync(SubmissionForStudentForCreationDto submissionDto);
    IEnumerable<SubmissionDto> RetrieveAllSubmissions();
    IEnumerable<SubmissionDto> RetrieveAllSubmissionsByQuizId(long quizId);
    ValueTask<SubmissionDto> RetrieveSubmissionByIdAsync(long submissionId);
    ValueTask<SubmissionDto> ModifySubmissionAsync(long submissionId, SubmissionForUpdateDto submissionDto);
    ValueTask<SubmissionDto> RemoveSubmissionAsync(long submissionId);
}
