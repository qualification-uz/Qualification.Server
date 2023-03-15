using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Quizes;

namespace Qualification.Data.Repositories;

public class QuizQuestionRepository : IQuizQuestionRepository
{
    private readonly AppDbContext appDbContext;

    public QuizQuestionRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async ValueTask<QuizQuestion> InsertQuizQuestionAsync(QuizQuestion quizQuestion)
    {
        EntityEntry<QuizQuestion> quizQuestionEntityEntry =
            await this.appDbContext.QuizQuestions.AddAsync(quizQuestion);

        await this.appDbContext.SaveChangesAsync();

        return quizQuestionEntityEntry.Entity;
    }

    public IQueryable<QuizQuestion> SelectAllQuizQuestions() =>
        this.appDbContext.QuizQuestions;

    public async ValueTask<QuizQuestion> SelectQuizQuestionByIdAsync(long quizQuestionId) =>
        await this.appDbContext.QuizQuestions.FindAsync(quizQuestionId);

    public async ValueTask<QuizQuestion> UpdateQuizQuestionAsync(QuizQuestion quizQuestion)
    {

        EntityEntry<QuizQuestion> quizQuestionEntityEntry =
            this.appDbContext.QuizQuestions.Update(quizQuestion);

        await this.appDbContext.SaveChangesAsync();

        return quizQuestionEntityEntry.Entity;
    }

    public async ValueTask<QuizQuestion> DeleteQuizQuestionAsync(QuizQuestion quizQuestion)
    {

        EntityEntry<QuizQuestion> quizQuestionEntityEntry =
            this.appDbContext.QuizQuestions.Remove(quizQuestion);

        await this.appDbContext.SaveChangesAsync();

        return quizQuestionEntityEntry.Entity;
    }
}
