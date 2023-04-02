using Qualification.Domain.Entities.Quizes;

namespace Qualification.Data.IRepositories;

public interface ISubmissionResultRepository
{
    IQueryable<SubmissionResult> SelectAllSubmissionResults();
    ValueTask<SubmissionResult> SelectSubmissionResultByIdAsync(long submissionResultId);
    ValueTask<SubmissionResult> InsertSubmissionResultAsync(SubmissionResult submissionResult);
    ValueTask<SubmissionResult> UpdateSubmissionResultAsync(SubmissionResult submissionResult);
    ValueTask<SubmissionResult> DeleteSubmissionResultAsync(SubmissionResult submissionResult);
}

