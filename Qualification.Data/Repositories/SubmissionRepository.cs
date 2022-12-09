using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Quizes;

namespace Qualification.Data.Repositories;

public class SubmissionRepository : ISubmissionRepository
{
    private readonly AppDbContext appDbContext;

    public SubmissionRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async ValueTask<Submission> InsertSubmissionAsync(Submission submission)
    {
        EntityEntry<Submission> submissionEntityEntry =
            await this.appDbContext.Submissions.AddAsync(submission);
        
        await this.appDbContext.SaveChangesAsync();

        return submissionEntityEntry.Entity;
    }

    public IQueryable<Submission> SelectAllSubmissions() =>
        this.appDbContext.Submissions;

    public async ValueTask<Submission> SelectSubmissionByIdAsync(long submissionId) =>
        await this.appDbContext.Submissions.FindAsync(submissionId);

    public async ValueTask<Submission> UpdateSubmissionAsync(Submission submission)
    {
        EntityEntry<Submission> submissionEntityEntry =
            this.appDbContext.Submissions.Update(submission);

        await this.appDbContext.SaveChangesAsync();

        return submissionEntityEntry.Entity;
    }

    public async ValueTask<Submission> DeleteSubmissionAsync(Submission submission)
    {
        EntityEntry<Submission> submissionEntityEntry =
            this.appDbContext.Remove(submission);

        await this.appDbContext.SaveChangesAsync();

        return submissionEntityEntry.Entity;
    }
}
