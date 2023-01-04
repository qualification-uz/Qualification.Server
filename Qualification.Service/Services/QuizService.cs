using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Questions;
using Qualification.Domain.Entities.Quizes;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Exceptions;
using Qualification.Service.Extensions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository quizRepository;
    private readonly IQuestionRepository questionRepository;
    private readonly IConfiguration configuration;
    private readonly UserManager<User> userManager;
    private IMapper mapper;

    public QuizService(
        IConfiguration configuration,
        IMapper mapper,
        IQuizRepository quizRepository,
        UserManager<User> userManager,
        IQuestionRepository questionRepository)
    {
        this.configuration = configuration;
        this.mapper = mapper;
        this.quizRepository = quizRepository;
        this.userManager = userManager;
        this.questionRepository = questionRepository;
    }

    public async ValueTask<QuizDto> CreateQuizAsync(QuizForCreationDto quizDto)
    {
        var quiz = this.mapper.Map<Quiz>(quizDto);

        var user = await this.userManager.Users
            .Include(teacher => teacher.Applications)
            .FirstOrDefaultAsync(teacher =>
                teacher.Id == quizDto.UserId);

        if (user is null)
            throw new NotFoundException("Couldn't find user for given id");

        bool isExpectedTeacher = await this.userManager
            .IsInRoleAsync(user, Enum.GetName<UserRole>(UserRole.Teacher));

        if (!isExpectedTeacher)
            throw new InvalidOperationException("Not allowed user");

        var application = user.Applications.FirstOrDefault(application =>
            application.Id == quizDto.ApplicationId);

        if (application is null)
            throw new NotFoundException("Couldn't find application for given id");

        quiz.User = user;
        quiz.Application = application;
        quiz = await this.quizRepository.InsertQuizAync(quiz);

        return this.mapper.Map<QuizDto>(quiz);
    }

    public async ValueTask CreateBulkQuizAsync(QuizForBulkCreationDto quizForBulkCreationDto)
    {
        var userIds = quizForBulkCreationDto.Datas.Select(data => data.UserId);

        var users = await this.userManager.Users
            .Where(user => userIds.Contains(user.Id))
            .Include(user => user.Applications)
            .ToListAsync();

        if (users.Count == 0 && users.Count != quizForBulkCreationDto.Datas.Count)
            throw new InvalidOperationException("Some users have missed");

        // get all users related to specific role
        var roleRelatedUsers = (await this.userManager
            .GetUsersInRoleAsync(Enum.GetName<UserRole>(UserRole.Teacher))
            ).ToDictionary(user => user.Id);

        foreach(var user in users)
        {
            if(!roleRelatedUsers.ContainsKey(user.Id))
                throw new InvalidOperationException("Some users have no access. Change the role");
        }

        var quizzes = new List<Quiz>();

        foreach(var data in quizForBulkCreationDto.Datas)
        {
            var quiz = new Quiz
            {
                Title = quizForBulkCreationDto.Title,
                StartsAt = quizForBulkCreationDto.StartsAt,
                EndsAt = quizForBulkCreationDto.EndsAt,
                UserId = data.UserId,
                ApplicationId = data.ApplicationId,
                Status = QuizStatus.NotStarted,
                CreatedAt = DateTime.UtcNow,
                IsForTeacher = true
            };
        }

        await this.quizRepository.InsertBulkQuizAsync(quizzes);
    }

    public IEnumerable<QuizDto> RetrieveAllQuizzes()
    {
        var quizzes = this.quizRepository
            .SelectAllQuizzes()
            .Include(quiz => quiz.Application)
            .Include(quiz => quiz.User)
            .OrderBy(quiz => quiz.CreatedAt);

        return this.mapper.Map<IEnumerable<QuizDto>>(quizzes);
    }

    public async ValueTask<QuizDto> RetrieveQuizByIdAsync(long quizId)
    {
        var quiz = await this.quizRepository
            .SelectAllQuizzes()
            .Include(quiz => quiz.Application)
            .Include(quiz => quiz.User)
            .FirstOrDefaultAsync(quiz => quiz.Id == quizId);

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");

        return this.mapper.Map<QuizDto>(quiz);
    }

    public async ValueTask<QuizDto> ModifyQuizAsync(long quizId, QuizForUpdateDto quizDto)
    {
        var quiz = await this.quizRepository
            .SelectQuizByIdAsync(quizId);

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");

        quiz.StartsAt = quizDto.StartsAt;
        quiz.EndsAt = quizDto.EndsAt;

        quiz = await this.quizRepository
            .UpdateQuizAsync(quiz);

        return this.mapper.Map<QuizDto>(quiz);
    }

    public async ValueTask<QuizDto> RemoveQuizAsync(long quizId)
    {
        var quiz = await this.quizRepository
            .SelectQuizByIdAsync(quizId);

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");

        quiz = await this.quizRepository
             .DeleteQuizAsync(quiz);

        return this.mapper.Map<QuizDto>(quiz);
    }

    public async ValueTask<QuizDto> ModifyQuizStatusAsync(long quizId, QuizStatus quizStatus)
    {
        var quiz = await this.quizRepository
            .SelectQuizByIdAsync(quizId);

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");

        quiz.Status = quizStatus;

        await this.quizRepository.UpdateQuizAsync(quiz);

        return this.mapper.Map<QuizDto>(quiz);
    }

    public async ValueTask<IEnumerable<QuizQuestionDto>> RetrieveQuizQuestions(long quizId)
    {
        var quiz = await this.quizRepository
            .SelectAllQuizzes()
            .Include(quiz => quiz.Questions)
            .Include(quiz => quiz.Application)
            .FirstOrDefaultAsync(quiz => quiz.Id == quizId);

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");

        // Select questions if haven't created yet
        if(quiz.Questions.Count == 0)
        {
            var shuffledQuestions = await RetrieveShuffledQuestions(
                subjectId: quiz.Application.SubjectId,
                isForTeacher: true);

            foreach(var question in shuffledQuestions)
            {
                quiz.Questions.Add(new QuizQuestion
                {
                    QuestionId = question.Id,
                    QuizId = quiz.Id,
                    CreatedAt = DateTime.UtcNow,
                    Options = question.Answers.Select(option => new QuestionOption
                    {
                        QuizOptionId = option.Id,
                        QuizQuestionId = question.Id,
                        CreatedAt = DateTime.UtcNow,
                    }).ToList()
                });
            }

            await this.quizRepository.UpdateQuizAsync(quiz);

            return this.mapper.Map<IEnumerable<QuizQuestionDto>>(shuffledQuestions);
        }

        // Questions have already exist
        var questions = await questionRepository
            .SelectAllQuestions()
            .Where(question => quiz.Questions
                .Select(question => question.QuestionId)
                .Any(id => id == question.Id))
            .Include(question => question.Assets)
            .Include(question => question.Answers)
            .ThenInclude(answer => answer.Assets)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<QuizQuestionDto>>(questions);
    }

    private async ValueTask<IEnumerable<Question>> RetrieveShuffledQuestions(long subjectId, bool isForTeacher)
    {
        int questionCount = int.Parse(configuration.GetSection("Quiz:QuestionCount").Value);

        var questions = await questionRepository
            .SelectAllQuestions()
            .Where(p => p.IsForTeacher == isForTeacher && p.SubjectId == subjectId)
            .Include(p => p.Assets)
            .Include(p => p.Answers)
            .ThenInclude(p => p.Assets)
            .ToDictionaryAsync(question => question.Id);

        HashSet<long> questionIds = new HashSet<long>();
        long minIndex = questions.Keys.Min();
        long maxIndex = questions.Keys.Max();
        var random = new Random();

        if (questions.Count < questionCount)
            questionCount = questions.Count;

        while (questionIds.Count != questionCount)
        {
            var randomId = random.NextInt64(minIndex, maxIndex + 1);

            if (questions.ContainsKey(randomId))
                questionIds.Add(randomId);
        }

        var shuffledQuestions = questionIds.Join(
                questions,
                questionId => questionId,
                question => question.Key,
                (questionId, question) => question.Value).ToList();

        return shuffledQuestions;
    }

    public IEnumerable<RoleDto> RetrieveQuizStatuses()
    {
        var quizStatusIds = Enum.GetValues<QuizStatus>();

        foreach (var quizStatusId in quizStatusIds)
            yield return new RoleDto
            {
                Id = (int)quizStatusId,
                Name = Enum.GetName<QuizStatus>(quizStatusId)
            };
    }

    //private async Task<CheckedQuizResultDto> CheckQuizAsync(CheckedQuizInputDto[] answers)
    //{
    //    return null;
    //}
}