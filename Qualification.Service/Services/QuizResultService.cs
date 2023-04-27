using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Quizes;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.Exceptions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class QuizResultService : IQuizResultService
{
    private readonly IMapper mapper;
    private readonly IQuizRepository quizRepository;
    private readonly IStudentRepository studentRepository;
    private readonly IQuizResultRepository quizResultRepository;
    private readonly IStudentQuizRepository studentQuizRepository;
    private readonly ISubmissionResultRepository submissionResultRepository;
    private readonly IQuestionAnswerRepository questionAnswerRepository;

    public QuizResultService(
        IMapper mapper,
        IQuizRepository quizRepository,
        IStudentRepository studentRepository,
        IQuizResultRepository quizResultRepository,
        IStudentQuizRepository studentQuizRepository,
        ISubmissionResultRepository submissionResultRepository,
        IQuestionAnswerRepository questionAnswerRepository)
    {
        this.mapper = mapper;
        this.quizRepository = quizRepository;
        this.studentRepository = studentRepository;
        this.quizResultRepository = quizResultRepository;
        this.studentQuizRepository = studentQuizRepository;
        this.submissionResultRepository = submissionResultRepository;
        this.questionAnswerRepository = questionAnswerRepository;
    }

    public async ValueTask<QuizResultDto> RetrieveStudentQuizResultAsync(long quizId, long studentId)
    {
        var quizResult = await this.quizResultRepository
            .SelectAllQuizResults()
            .FirstOrDefaultAsync();

        if (quizResult is not null)
        {
            return this.mapper.Map<QuizResultDto>(quizResult);
        }

        var student = await this.studentRepository.SelectStudentByIdAsync(studentId);
        if (student is null)
            throw new NotFoundException("Couldn't find student for given id");

        quizResult = new QuizResult();

        var quiz = await this.studentQuizRepository
            .SelectAllStudentQuizzes()
            .Where(quiz => quiz.Id == quizId)
            .Include(quiz => quiz.Questions)
            .Include(quiz => quiz.Submissions)
            .ThenInclude(submission => submission.Option)
            .FirstOrDefaultAsync();

        if (quiz is null)
            throw new NotFoundException("Couldn't find quiz for given id");


        var correctSubmissions = this.submissionResultRepository
            .SelectAllSubmissionResults()
            .Where(t => t.QuizId == quizId)
            .Where(t => t.IsForStudent)
            .Where(t => t.IsCorrect).ToList();

        var correctAnswers = correctSubmissions.DistinctBy(t => t.QuizQuestionId).Count();

        quizResult.CorrectAnswers = (short)correctAnswers;
        quizResult.Score = correctAnswers * 100 / quiz.Questions.Count;
        quizResult.UserId = (long)quiz.StudentId;
        quizResult.QuizId = quiz.Id;
        quizResult.StudentId = studentId;

        quizResult = await this.quizResultRepository
            .UpdateQuizResultAsync(quizResult);

        return this.mapper.Map<QuizResultDto>(quizResult);
    }

    public async ValueTask<QuizResultDto> RetrieveTeacherQuizResultAsync(long quizId, long? studentId = null)
    {
        var quizResult = await this.quizResultRepository
            .SelectAllQuizResults()
            .FirstOrDefaultAsync();

        if (quizResult is not null)
        {
            return this.mapper.Map<QuizResultDto>(quizResult);
        }

        quizResult = new QuizResult();

        var quiz = await this.quizRepository
            .SelectAllQuizzes()
            .Where(quiz => quiz.Id == quizId)
            .Where(quiz => quiz.IsForTeacher)
            .Include(quiz => quiz.Questions)
            .Include(quiz => quiz.Submissions)
            .ThenInclude(submission => submission.Option)
            .FirstOrDefaultAsync();

        var studentQuiz = await this.quizRepository.SelectAllQuizzes()
            .Where(t => t.ApplicationId == quiz.ApplicationId)
            .Where(t => !t.IsForTeacher)
            .Where(t => t.IsCompleted)
            .FirstOrDefaultAsync();

        if (quiz is null)
        {
            throw new NotFoundException("Couldn't find quiz for given id");
        }

        //var options = quiz.Submissions
        //    .Select(submission =>
        //        submission.Option.QuizOptionId)
        //    .ToHashSet();

        //int correctAnswers = await this.questionAnswerRepository
        //    .SelectAllQuestions()
        //    .Where(answer => options.Any(id => id == answer.Id))
        //    .Where(answer => answer.IsCorrect)
        //    .CountAsync();

        var correctSubmissions = this.submissionResultRepository
            .SelectAllSubmissionResults()
            .Where(t => t.QuizId == quizId)
            .Where(t => t.IsCorrect).ToList();

        var correctAnswers = correctSubmissions.DistinctBy(t => t.QuizQuestionId).Count();

        quizResult.CorrectAnswers = (short)correctAnswers;
        quizResult.Score = correctAnswers * 100 / quiz.Questions.Count;
        quizResult.UserId = (long)quiz.UserId;
        quizResult.QuizId = quiz.Id;
        quizResult.StudentId = studentId;

        quizResult = await this.quizResultRepository
            .UpdateQuizResultAsync(quizResult);

        return this.mapper.Map<QuizResultDto>(quizResult);
    }
}
