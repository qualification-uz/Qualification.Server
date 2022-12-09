using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Quizes;

namespace Qualification.Data.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext appDbContext;

    public QuizRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async ValueTask<Quiz> InsertQuizAync(Quiz quiz)
    {
        EntityEntry<Quiz> quizEntityEntry =
            await this.appDbContext.Quizes.AddAsync(quiz);

        await this.appDbContext.SaveChangesAsync();

        return quizEntityEntry.Entity;
    }

    public IQueryable<Quiz> SelectAllQuizes() =>
        this.appDbContext.Quizes;

    public async ValueTask<Quiz> SelectQuizByIdAsync(long quizId) =>
        await this.appDbContext.Quizes.FindAsync(quizId);

    public async ValueTask<Quiz> UpdateQuizAsync(Quiz quiz)
    {
        EntityEntry<Quiz> quizEntityEntry =
            this.appDbContext.Quizes.Update(quiz);

        await this.appDbContext.SaveChangesAsync();

        return quizEntityEntry.Entity;
    }

    public async ValueTask<Quiz> DeleteQuizAsync(Quiz quiz)
    {
        EntityEntry<Quiz> quizEntityEntry =
            this.appDbContext.Quizes.Remove(quiz);

        await this.appDbContext.SaveChangesAsync();

        return quizEntityEntry.Entity;
    }
}
