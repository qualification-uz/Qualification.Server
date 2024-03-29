﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Qualification.Data.IRepositories;
using Qualification.Data.Repositories;
using Qualification.Domain.Configurations;
using Qualification.Domain.Entities;
using Qualification.Domain.Entities.Questions;
using Qualification.Domain.Entities.Quizes;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Exceptions;
using Qualification.Service.Extensions;
using Qualification.Service.Interfaces;
using System;

namespace Qualification.Service.Services;

public class StudentQuizService : IStudentQuizService
{
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly IStudentRepository studentRepository;
    private readonly IApplicationService applicationService;
    private readonly IQuestionRepository questionRepository;
    private readonly IApplicationRepository applicationRepository;
    private readonly IStudentQuizRepository studentQuizRepository;
    public StudentQuizService(
        IMapper mapper,
        IConfiguration configuration,
        IStudentRepository studentRepository,
        IQuestionRepository questionRepository,
        IApplicationService applicationService,
        IApplicationRepository applicationRepository,
        IStudentQuizRepository studentQuizRepository)
    {
        this.mapper = mapper;
        this.configuration = configuration;
        this.studentRepository = studentRepository;
        this.questionRepository = questionRepository;
        this.applicationService = applicationService;
        this.applicationRepository = applicationRepository;
        this.studentQuizRepository = studentQuizRepository;
    }

    public async ValueTask<QuizeForStudentDto> CreateQuizAsync(QuizForStudentCreationDto quizDto)
    {
        var student = await this.studentRepository.SelectStudentByIdAsync(quizDto.StudentId);
        if (student is null)
            throw new NotFoundException("Student is not found");

        bool isApplication = student.ApplicationId.Equals(quizDto.ApplicationId);
        if (!isApplication)
            throw new NotFoundException("Application is not available");

        var application = await this.applicationRepository.SelectApplicationByIdAsync(quizDto.ApplicationId);

        var quiz = mapper.Map<QuizForStudent>(quizDto);
        quiz.StudentId = student.Id;
        quiz.ApplicationId = quizDto.ApplicationId;
        var createdQuiz = await this.studentQuizRepository.InsertStudentQuizAync(quiz);
        var result = mapper.Map<QuizeForStudentDto>(createdQuiz);
        result.User = mapper.Map<UserDto>(application.Teacher);
        result.UserId = application.TeacherId;
        result.Application = mapper.Map<ApplicationDto>(application);
        return result;
    }

    public async ValueTask<QuizeForStudentDto> ModifyQuizAsync(long quizId, QuizForUpdateDto quizDto)
    {
        var quiz = await this.studentQuizRepository
            .SelectStudentQuizByIdAsync(quizId);

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");

        quiz.StartsAt = quizDto.StartsAt;
        quiz.EndsAt = quizDto.EndsAt;

        quiz = await this.studentQuizRepository
            .UpdateStudentQuizAsync(quiz);

        return this.mapper.Map<QuizeForStudentDto>(quiz);
    }

    public async ValueTask<QuizeForStudentDto> ModifyQuizStatusAsync(long quizId, QuizStatus quizStatus)
    {
        var quiz = await this.studentQuizRepository
            .SelectStudentQuizByIdAsync(quizId);

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");

        quiz.Status = quizStatus;

        await this.studentQuizRepository.UpdateStudentQuizAsync(quiz);

        return this.mapper.Map<QuizeForStudentDto>(quiz);
    }

    public async ValueTask<QuizeForStudentDto> RemoveQuizAsync(long quizId)
    {
        var quiz = await this.studentQuizRepository
            .SelectStudentQuizByIdAsync(quizId);

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");

        quiz = await this.studentQuizRepository
             .DeleteStudentQuizAsync(quiz);

        return this.mapper.Map<QuizeForStudentDto>(quiz);
    }

    public IEnumerable<QuizeForStudentDto> RetrieveAllQuizzes(Filters filters, PaginationParams @params)
    {
        var quizzes = this.studentQuizRepository
            .SelectAllStudentQuizzes();

        quizzes = filters
                .Aggregate(quizzes, (current, filter) => current.Filter(filter));

        return this.mapper.Map<IEnumerable<QuizeForStudentDto>>(quizzes)
            .ToPagedList(@params);
    }

    public async ValueTask<List<QuizeForStudentDto>> RetrieveQuizByApplicationIdAsync(long applicationId)
    {
        var quiz = await this.studentQuizRepository
            .SelectAllStudentQuizzes()
            .Where(quiz => quiz.ApplicationId == applicationId)
            .ToListAsync();

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");

        return this.mapper.Map<List<QuizeForStudentDto>>(quiz);
    }

    public async ValueTask<QuizeForStudentDto> RetrieveQuizByIdAsync(long studentId)
    {
        var studentQuiz = await this.studentQuizRepository
            .SelectAllStudentQuizzes()
            .FirstOrDefaultAsync(quiz => quiz.StudentId == studentId);

        if (studentQuiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");
       
        var application = await this.applicationService.RetrieveApplicationByIdAsync(studentQuiz.ApplicationId);

        var result = mapper.Map<QuizeForStudentDto>(studentQuiz);
        result.User = mapper.Map<UserDto>(application.Teacher);
        result.UserId = application.Teacher.Id;
        result.Application = mapper.Map<ApplicationDto>(application);
        return result;
    }

    public async ValueTask<IEnumerable<QuizQuestionDto>> RetrieveQuizQuestions(long studentId)
    {
        var student = await this.studentRepository.SelectStudentByIdAsync(studentId);
        if (student == null)
            throw new NotFoundException("Student not found");

        var application = await this.applicationService.RetrieveApplicationByIdAsync((long)student.ApplicationId);
        var quiz = await this.studentQuizRepository
                .SelectAllStudentQuizzes()
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(t => t.StudentId == studentId);

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");


        // Select questions if haven't created yet
        if (quiz.Questions.Count == 0)
        {
            var shuffledQuestions = await RetrieveShuffledQuestions(
                subjectId: application.SubjectId,
                isForTeacher: false, 
                studentGradeId: student.GradeId ?? 0
            );

            foreach (var question in shuffledQuestions)
            {
                quiz.Questions.Add(new QuizQuestion
                {
                    QuizForStudentId = quiz.Id,
                    QuestionId = question.Id,
                    CreatedAt = DateTime.UtcNow,
                    Options = question.Answers.Select(option => new QuestionOption
                    {
                        QuizOptionId = option.Id,
                        QuizQuestionId = question.Id,
                        CreatedAt = DateTime.UtcNow,
                    }).ToList()
                });
            }

            await this.studentQuizRepository.UpdateStudentQuizAsync(quiz);

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
            .ThenInclude(asset => asset.Asset)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<QuizQuestionDto>>(questions);
    }

    private async ValueTask<IEnumerable<Question>> RetrieveShuffledQuestions(long subjectId, bool isForTeacher, long studentGradeId)
    {
        int questionCount = int.Parse(configuration.GetSection("Quiz:QuestionCount").Value);

        var questions = await questionRepository
            .SelectAllQuestions()
            .Where(p => p.IsForTeacher == isForTeacher && p.SubjectId == subjectId && p.StudentGradeId == studentGradeId)
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
}
