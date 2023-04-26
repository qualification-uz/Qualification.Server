using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Quizes;

namespace Qualification.Data.Repositories;

public class StudentQuizRepository : IStudentQuizRepository
{
    private readonly AppDbContext appDbContext;

    public StudentQuizRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async ValueTask<QuizForStudent> InsertStudentQuizAync(QuizForStudent quiz)
    {
        EntityEntry<QuizForStudent> quizEntityEntry =
            await this.appDbContext.QuizForStudents.AddAsync(quiz);

        await this.appDbContext.SaveChangesAsync();

        return quizEntityEntry.Entity;
    }

    public IQueryable<QuizForStudent> SelectAllStudentQuizzes() =>
        this.appDbContext.QuizForStudents;

    public async ValueTask<QuizForStudent> SelectStudentQuizByIdAsync(long quizId) =>
        await this.appDbContext.QuizForStudents.FindAsync(quizId);

    public async ValueTask<QuizForStudent> UpdateStudentQuizAsync(QuizForStudent quiz)
    {
        EntityEntry<QuizForStudent> quizEntityEntry =
            this.appDbContext.QuizForStudents.Update(quiz);

        await this.appDbContext.SaveChangesAsync();

        return quizEntityEntry.Entity;
    }

    public async ValueTask<QuizForStudent> DeleteStudentQuizAsync(QuizForStudent quiz)
    {
        EntityEntry<QuizForStudent> quizEntityEntry =
            this.appDbContext.QuizForStudents.Remove(quiz);

        await this.appDbContext.SaveChangesAsync();

        return quizEntityEntry.Entity;
    }

    public async ValueTask InsertBulkStudentQuizAsync(IEnumerable<QuizForStudent> quizes)
    {
        await this.appDbContext.QuizForStudents.AddRangeAsync(quizes);

        await this.appDbContext.SaveChangesAsync();
    }
}
