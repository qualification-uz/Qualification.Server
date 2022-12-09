using Qualification.Domain.Entities.Quizes;

namespace Qualification.Data.IRepositories;

public interface ISubmissionRepository
{
    IQueryable<Submission> SelectAllSubmissions();
    ValueTask<Submission> SelectSubmissionByIdAsync(long submissionId);
    ValueTask<Submission> InsertSubmissionAsync(Submission submission);
    ValueTask<Submission> UpdateSubmissionAsync(Submission submission);
    ValueTask<Submission> DeleteSubmissionAsync(Submission submission);
}
