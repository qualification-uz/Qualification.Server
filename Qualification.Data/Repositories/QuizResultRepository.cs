using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Quizes;

namespace Qualification.Data.Repositories;

public class QuizResultRepository : IQuizResultRepository
{
    private readonly AppDbContext appDbContext;

    public QuizResultRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async ValueTask<QuizResult> InsertQuizResultAsync(QuizResult quizResult)
    {
        EntityEntry<QuizResult> quizResultEntityEntry =
            await this.appDbContext.Results.AddAsync(quizResult);

        await this.appDbContext.SaveChangesAsync();

        return quizResultEntityEntry.Entity;
    }

    public IQueryable<QuizResult> SelectAllQuizResults() =>
        this.appDbContext.Results;

    public async ValueTask<QuizResult> SelectQuizResultByIdAsync(long quizResultId) =>
        await this.appDbContext.Results.FindAsync(quizResultId);

    public async ValueTask<QuizResult> UpdateQuizResultAsync(QuizResult quizResult)
    {
        EntityEntry<QuizResult> quizResultEntityEntry =
            this.appDbContext.Results.Update(quizResult);

        await this.appDbContext.SaveChangesAsync();

        return quizResultEntityEntry.Entity;
    }

    public async ValueTask<QuizResult> DeleteQuizResultAsync(QuizResult quizResult)
    {
        EntityEntry<QuizResult> quizResultEntityEntry =
            this.appDbContext.Results.Remove(quizResult);

        await this.appDbContext.SaveChangesAsync();

        return quizResultEntityEntry.Entity;
    }
}