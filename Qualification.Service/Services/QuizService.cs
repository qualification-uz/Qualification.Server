using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Qualification.Data.IRepositories;
using Qualification.Domain.Configurations;
using Qualification.Domain.Entities.Questions;
using Qualification.Domain.Entities.Quizes;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;
using Qualification.Service.AvloniyClient;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.DTOs.Sertificate;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Exceptions;
using Qualification.Service.Extensions;
using Qualification.Service.Helpers;
using Qualification.Service.Interfaces;
using System.Collections.Generic;

namespace Qualification.Service.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository quizRepository;
    private readonly ICertificateRepository certificateRepository;
    private readonly IStudentQuizRepository studentQuizRepository;
    private readonly IQuestionRepository questionRepository;
    private readonly IConfiguration configuration;
    private readonly UserManager<User> userManager;
    private readonly IApplicationRepository applicationRepository;
    private readonly IStudentQuizRepository quizForStudentRepository;
    private readonly IStudentRepository studentRepository;
    private readonly ISertificateService sertificateService;
    private readonly IStudentQuizService studentQuizService;
    private readonly IQuizResultRepository quizResultRepository;
    private readonly IAvloniyClientService avloniyService;
    private readonly IAssetService assetService;
    private IMapper mapper;

    public QuizService(
        IConfiguration configuration,
        IMapper mapper,
        IQuizRepository quizRepository,
        UserManager<User> userManager,
        IStudentRepository studentRepository,
        IQuestionRepository questionRepository,
        ISertificateService sertificateService,
        IQuizResultRepository quizResultRepository,
        IStudentQuizService studentQuizService,
        IAvloniyClientService avloniyService,
        IStudentQuizRepository quizForStudentRepository,
        IApplicationRepository applicationRepository,
        IAssetService assetService,
        IStudentQuizRepository studentQuizRepository,
        ICertificateRepository certificateRepository)
    {
        this.configuration = configuration;
        this.mapper = mapper;
        this.quizRepository = quizRepository;
        this.userManager = userManager;
        this.studentRepository = studentRepository;
        this.questionRepository = questionRepository;
        this.sertificateService = sertificateService;
        this.quizResultRepository = quizResultRepository;
        this.studentQuizService = studentQuizService;
        this.avloniyService = avloniyService;
        this.quizForStudentRepository = quizForStudentRepository;
        this.applicationRepository = applicationRepository;
        this.assetService=assetService;
        this.studentQuizRepository=studentQuizRepository;
        this.certificateRepository=certificateRepository;
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
        application.Status = ApplicationStatus.TestBelgilandi;
        quiz.UserId = user.Id;
        quiz.Application = application;
        quiz.IsForTeacher = true;
        quiz = await this.quizRepository.InsertQuizAync(quiz);

        var students = await this.studentRepository.SelectAllStudents()
            .Where(t => t.ApplicationId == quiz.ApplicationId).ToListAsync();

        foreach (var student in students)
            await this.studentQuizService.CreateQuizAsync(new QuizForStudentCreationDto
            {
                Title = quiz.Title,
                ApplicationId = application.Id,
                StudentId = student.Id,
                StartsAt = quiz.StartsAt,
                EndsAt = quiz.EndsAt,
            });

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

        foreach (var user in users)
        {
            if (!roleRelatedUsers.ContainsKey(user.Id))
                throw new InvalidOperationException("Some users have no access. Change the role");
        }

        var quizzes = new List<Quiz>();

        foreach (var data in quizForBulkCreationDto.Datas)
        {
            var quiz = new Quiz
            {
                Title = quizForBulkCreationDto.Title,
                StartsAt = quizForBulkCreationDto.StartsAt,
                EndsAt = quizForBulkCreationDto.EndsAt,
                UserId = data.UserId,
                ApplicationId = data.ApplicationId,
                Status = QuizStatus.Boshlanmadi,
                CreatedAt = DateTime.UtcNow,
                IsForTeacher = true
            };
        }

        await this.quizRepository.InsertBulkQuizAsync(quizzes);
    }

    public IEnumerable<QuizDto> RetrieveAllQuizzes(
        Filters filters,
        PaginationParams @params)
    {
        var quizzes = this.quizRepository
            .SelectAllQuizzes();

        quizzes = filters
                .Aggregate(quizzes, (current, filter) => current.Filter(filter));

        quizzes = quizzes
            .Include(quiz => quiz.Application)
            .Include(quiz => quiz.User)
            .OrderBy(quiz => quiz.CreatedAt);

        return this.mapper.Map<IEnumerable<QuizDto>>(quizzes)
            .ToPagedList(@params);
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

    public async ValueTask<QuizDto> RetrieveQuizByApplicationIdAsync(long applicationId)
    {
        var quiz = await this.quizRepository
            .SelectAllQuizzes()
            .Include(quiz => quiz.Application)
            .Include(quiz => quiz.User)
            .FirstOrDefaultAsync(quiz => quiz.ApplicationId == applicationId);

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
        if (quiz.Questions.Count == 0)
        {
            var shuffledQuestions = await RetrieveShuffledQuestions(
                subjectId: quiz.Application.SubjectId,
                isForTeacher: true);

            foreach (var question in shuffledQuestions)
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
            .ThenInclude(question => question.Asset)
            .Include(question => question.Answers)
            .ThenInclude(answer => answer.Assets)
            .ThenInclude(answer => answer.Asset)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<QuizQuestionDto>>(questions); 
    }

    public async ValueTask<IEnumerable<QuizQuestionDto>> RetrieveQuizQuestionsByApplicationId(long applicationId, long studentId)
    {
        var quiz = await this.quizForStudentRepository
                .SelectAllStudentQuizzes()
                .Include(quiz => quiz.Questions)
                .FirstOrDefaultAsync(quiz => quiz.ApplicationId == applicationId);

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");

        var application = await this.applicationRepository.SelectApplicationByIdAsync(applicationId);
        var student = await this.studentRepository.SelectStudentByIdAsync(studentId);

        // Select questions if haven't created yet
        if (quiz.Questions.Count == 0)
        {
            var shuffledQuestions = await RetrieveShuffledQuestions(
                subjectId: application.SubjectId,
                isForTeacher: false);

            foreach (var question in shuffledQuestions)
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

            await this.quizForStudentRepository.UpdateStudentQuizAsync(quiz);

            return this.mapper.Map<IEnumerable<QuizQuestionDto>>(shuffledQuestions);
        }

        // Questions have already exist
        var questions = await questionRepository
            .SelectAllQuestions()
            .Where(question => quiz.Questions
                .Select(question => question.QuestionId)
                .Any(id => id == question.Id) && question.StudentGradeId == student.GradeId)
            .Include(question => question.Assets)
            .ThenInclude(asset => asset.Asset)
            .Include(question => question.Answers)
            .ThenInclude(answer => answer.Assets)
            .ThenInclude(answer => answer.Asset)
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
            .ThenInclude(asset => asset.Asset)
            .Include(p => p.Answers)
            .ThenInclude(p => p.Assets)
            .ThenInclude(p => p.Asset)
            .ToDictionaryAsync(question => question.Id);

        HashSet<long> questionIds = new HashSet<long>();
        long minIndex = questions.OrderBy(x => x.Key).FirstOrDefault().Key;
        long maxIndex = questions.OrderByDescending(x => x.Key).FirstOrDefault().Key;
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

    public async ValueTask<QuizDto> RetrieveQuizByPropertyValue(Filter filter)
    {
        var quizes = this.quizRepository
            .SelectAllQuizzes()
            .Filter(filter);

        var quiz = await quizes
            .Include(quiz => quiz.Application)
            .FirstOrDefaultAsync();

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");

        return this.mapper.Map<QuizDto>(quiz);
    }

    public async ValueTask<IEnumerable<QuizDto>> RetrieveQuizByTeacherId(
        long teacherId,
        PaginationParams paginationParams)
    {
        var quizzes = await this.quizRepository
            .SelectAllQuizzes()
            .Include(quiz => quiz.Application)
            .Where(quiz => quiz.UserId == teacherId)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<QuizDto>>(quizzes
            .ToPagedList(paginationParams));
    }

    public async ValueTask<byte[]> GenerateSertificateAsync(long quizId)
    {
        var quiz = await this.quizRepository
            .SelectAllQuizzes()
            .Include(quiz => quiz.Application)
            .FirstOrDefaultAsync(quiz => quiz.Id == quizId);

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");

        // calculate subject score
        var quizResult = await this.quizResultRepository.SelectAllQuizResults()
            .Where(quizResult => quizResult.QuizId == quizId)
            .Include(quizResult => quizResult.Quiz)
            .Include(quizResult => quizResult.User)
            .FirstOrDefaultAsync();

        var subjectScore = 20 * (quizResult?.Score ?? 0) / 100;

        // calculate pedagocial score
        var studentQuizes = await this.studentQuizRepository.SelectAllStudentQuizzes()
            .Where(studentQuiz => studentQuiz.ApplicationId == quiz.ApplicationId)
            .ToListAsync();
        var studentIds = studentQuizes.Select(studentQuiz => studentQuiz.StudentId);
        var allQuizCompleted = studentQuizes.All(studentQuiz => studentQuiz.IsCompleted);
        if (!allQuizCompleted)
            throw new Exception("All student have to finish the quiz");

        if (!studentIds.Any())
            throw new NotFoundException("Students not found");

        var studentQuizResults = await this.quizResultRepository.SelectAllQuizResults()
            .Where(quizResult => studentIds.Contains(quizResult.StudentId ?? 0))
            .ToListAsync();

        var pedagogicalScore = 80 * studentQuizResults.Count(p => p.Score >= 50) / studentIds.Count();

        if(pedagogicalScore + subjectScore < 60)
            throw new Exception("Siz test sinovlaridan o�ta olmadingiz yoki test topshiriqlarini muvaffaqiyatli topshira olmadingiz");

        // create sertificate
        var subjectsResponse = await this.avloniyService.SelectAllSubjectsAsync();
        if (subjectsResponse.Success)
        {
            var subject = subjectsResponse.Result.FirstOrDefault(subject => subject.Id == quiz.Application?.SubjectId);
            
            // save certificate
            var certificate = await this.certificateRepository.InsertCertificateAsync(new Certificate
            {
                ApplicationId = quiz.ApplicationId,
                ExpireDate = DateTime.UtcNow.AddYears(1),
                PedagogicalScore = pedagogicalScore,
                SubjectScore = subjectScore,
                UserId = quiz.UserId
            });
            certificate.Code = quiz.UserId + "-" + quizId + "-" + certificate.Id;

            certificate = await this.certificateRepository.UpdateCertificateAsync(certificate);

            return await this.sertificateService.GenerateSertificateAsync(new SertificateForCreationDto
            {
                FullName = quizResult?.User?.FirstName + " " + quizResult?.User?.LastName,
                SertificateNumber = certificate.Code,
                PedagogicalScore = certificate.PedagogicalScore,
                Subject = subject.Name,
                SubjectScore= subjectScore,
                TotalScore = pedagogicalScore + subjectScore,
                CreatedDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddYears(1)
            });
        };

        throw new Exception("Couldn't get subject");
    }

    public async ValueTask<QuizeForStudentDto> CreateStudentQuizAsync(QuizForStudentCreationDto quizDto)
    {
        var student = await this.studentRepository.SelectStudentByIdAsync(quizDto.StudentId);
        if (student is null)
            throw new NotFoundException("Student is not found");

        bool isApplication = student.ApplicationId.Equals(quizDto.ApplicationId);
        if (!isApplication)
            throw new NotFoundException("Application is not available");

        var application = await this.applicationRepository.SelectApplicationByIdAsync(quizDto.ApplicationId);
        application.Status = ApplicationStatus.TestBelgilandi;

        var quiz = mapper.Map<QuizForStudent>(quizDto);
        quiz.StudentId = student.Id;
        quiz.ApplicationId = application.Id;
        var result = await this.quizForStudentRepository.InsertStudentQuizAync(quiz);
        return mapper.Map<QuizeForStudentDto>(result);
    }
}