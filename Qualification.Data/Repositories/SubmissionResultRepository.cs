using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Quizes;

namespace Qualification.Data.Repositories;

public class SubmissionResultRepository : ISubmissionResultRepository
{
    private readonly AppDbContext appDbContext;

    public SubmissionResultRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async ValueTask<SubmissionResult> InsertSubmissionResultAsync(SubmissionResult submissionResult)
    {
        EntityEntry<SubmissionResult> submissionResultEntityEntry =
            await this.appDbContext.SubmissionResults.AddAsync(submissionResult);

        await this.appDbContext.SaveChangesAsync();

        return submissionResultEntityEntry.Entity;
    }

    public IQueryable<SubmissionResult> SelectAllSubmissionResults() =>
        this.appDbContext.SubmissionResults;

    public async ValueTask<SubmissionResult> SelectSubmissionResultByIdAsync(long submissionResultId) =>
        await this.appDbContext.SubmissionResults.FindAsync(submissionResultId);

    public async ValueTask<SubmissionResult> UpdateSubmissionResultAsync(SubmissionResult submissionResult)
    {
        EntityEntry<SubmissionResult> submissionResultEntityEntry =
            this.appDbContext.SubmissionResults.Update(submissionResult);

        await this.appDbContext.SaveChangesAsync();

        return submissionResultEntityEntry.Entity;
    }

    public async ValueTask<SubmissionResult> DeleteSubmissionResultAsync(SubmissionResult submissionResult)
    {
        EntityEntry<SubmissionResult> submissionResultEntityEntry =
            this.appDbContext.Remove(submissionResult);

        await this.appDbContext.SaveChangesAsync();

        return submissionResultEntityEntry.Entity;
    }
}
