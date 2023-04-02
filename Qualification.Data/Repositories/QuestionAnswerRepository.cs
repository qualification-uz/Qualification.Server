using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Questions;

namespace Qualification.Data.Repositories;

public class QuestionAnswerRepository : IQuestionAnswerRepository
{
    private readonly AppDbContext appDbContext;

    public QuestionAnswerRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public IQueryable<QuestionAnswer> SelectAllQuestionAnswers() => this.appDbContext.QuestionAnswers;

    public async ValueTask<QuestionAnswer> SelectQuestionByIdAsync(long questionAnswerId) =>
        await this.appDbContext.QuestionAnswers.FindAsync(questionAnswerId);

    public async ValueTask<QuestionAnswer> SelectQuestionByIdAsync(
        long questionAnswerId,
        IReadOnlyList<string> includes)
    {
        IQueryable<QuestionAnswer> questionAnswers = this.appDbContext.QuestionAnswers;

        foreach (var include in includes)
            questionAnswers = questionAnswers.Include(include);

        return await questionAnswers
            .FirstOrDefaultAsync(questionAnswer => questionAnswer.Id == questionAnswerId);
    }

    public async ValueTask<QuestionAnswer> InsertQuestionAsync(QuestionAnswer questionAnswer)
    {
        EntityEntry<QuestionAnswer> questionAnswerEntityEntry =
            await this.appDbContext.QuestionAnswers.AddAsync(questionAnswer);

        await this.appDbContext.SaveChangesAsync();

        return questionAnswerEntityEntry.Entity;
    }

    public async ValueTask<QuestionAnswer> UpdateQuestionAsync(QuestionAnswer questionAnswer)
    {
        EntityEntry<QuestionAnswer> questionEntityEntry =
            this.appDbContext.QuestionAnswers.Update(questionAnswer);

        await this.appDbContext.SaveChangesAsync();

        return questionEntityEntry.Entity;
    }

    public async ValueTask<QuestionAnswer> DeleteQuestionAsync(QuestionAnswer questionAnswer)
    {
        EntityEntry<QuestionAnswer> questionEntityEntry =
            this.appDbContext.QuestionAnswers.Remove(questionAnswer);

        await this.appDbContext.SaveChangesAsync();

        return questionEntityEntry.Entity;
    }
}