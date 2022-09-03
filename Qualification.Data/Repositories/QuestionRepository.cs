using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Questions;

namespace Qualification.Data.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly AppDbContext appDbContext;

    public QuestionRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public IQueryable<Question> SelectAllQuestions() => this.appDbContext.Questions;

    public async ValueTask<Question> SelectQuestionByIdAsync(long questionId) =>
        await this.appDbContext.Questions.FindAsync(questionId);

    public async ValueTask<Question> InsertQuestionAsync(Question question)
    {
        EntityEntry<Question> questionEntityEntry =
            await this.appDbContext.Questions.AddAsync(question);

        await this.appDbContext.SaveChangesAsync();

        return questionEntityEntry.Entity;
    }

    public async ValueTask<Question> UpdateQuestionAsync(Question question)
    {
        EntityEntry<Question> questionEntityEntry =
            this.appDbContext.Questions.Update(question);

        await this.appDbContext.SaveChangesAsync();

        return questionEntityEntry.Entity;
    }

    public async ValueTask<Question> DeleteQuestionAsync(Question question)
    {
        EntityEntry<Question> questionEntityEntry =
            this.appDbContext.Questions.Remove(question);

        await this.appDbContext.SaveChangesAsync();

        return questionEntityEntry.Entity;
    }
}
