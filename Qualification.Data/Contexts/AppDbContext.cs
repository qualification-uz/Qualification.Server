using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Qualification.Domain.Entities;
using Qualification.Domain.Entities.Assets;
using Qualification.Domain.Entities.Payments;
using Qualification.Domain.Entities.Questions;
using Qualification.Domain.Entities.Quizes;
using Qualification.Domain.Entities.Users;

namespace Qualification.Data.Contexts;

public class AppDbContext : IdentityDbContext<User, Role, long>
{
    public AppDbContext()
    { }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Application request

        modelBuilder.Entity<Application>()
            .HasMany(application => application.Groups)
            .WithOne(group => group.Application)
            .HasForeignKey(group => group.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(teacher => teacher.Applications)
            .WithOne(application => application.Teacher)
            .HasForeignKey(application => application.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region Question

        modelBuilder.Entity<Question>()
            .HasMany(question => question.Answers)
            .WithOne(answer => answer.Question)
            .HasForeignKey(answer => answer.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Question>()
            .HasMany(question => question.Assets)
            .WithOne(asset => asset.Question)
            .HasForeignKey(asset => asset.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuestionAnswer>()
            .HasMany(answer => answer.Assets)
            .WithOne(asset => asset.QuestionAnswer)
            .HasForeignKey(asset => asset.QuestionAnswerId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region Quiz

        modelBuilder.Entity<Quiz>()
            .HasMany(quiz => quiz.Submissions)
            .WithOne(submission => submission.Quiz)
            .HasForeignKey(submission => submission.QuizId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Application>()
            .HasMany(application => application.Quizes)
            .WithOne(quiz => quiz.Application)
            .HasForeignKey(quiz => quiz.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Application> Applications { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionAsset> QuestionAssets { get; set; }
    public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
    public DbSet<QuestionAnswerAsset> QuestionAnswerAssets { get; set; }
    public DbSet<Asset> Assets { get; set; }

    public DbSet<Quiz> Quizes { get; set; }
    public DbSet<Submission> Submissions { get; set; }
}

