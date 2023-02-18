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
    private readonly IQuizResultRepository quizResultRepository;
    private readonly IQuizRepository quizRepository;
    private readonly IQuestionAnswerRepository questionAnswerRepository;
    private readonly IMapper mapper;

    public QuizResultService(
        IQuizResultRepository quizResultRepository,
        IMapper mapper,
        IQuizRepository quizRepository,
        IQuestionAnswerRepository questionAnswerRepository)
    {
        this.quizResultRepository = quizResultRepository;
        this.mapper = mapper;
        this.quizRepository = quizRepository;
        this.questionAnswerRepository = questionAnswerRepository;
    }

    public async ValueTask<QuizResultDto> RetrieveQuizResultAsync(long quizId, long? studentId = null)
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
            .Include(quiz => quiz.Questions)
            .Include(quiz => quiz.Submissions)
            .ThenInclude(submission => submission.Option)
            .FirstOrDefaultAsync();

        if (quiz is null)
        {
            throw new NotFoundException("Couldn't find quiz for given id");
        }

        var options = quiz.Submissions
            .Select(submission =>
                submission.Option.QuizOptionId)
            .ToHashSet();

        int correctAnswers = await this.questionAnswerRepository
            .SelectAllQuestions()
            .Where(answer => options.Any(id => id == answer.Id))
            .Where(answer => answer.IsCorrect)
            .CountAsync();

        quizResult.CorrectAnswers = (short)correctAnswers;
        quizResult.Score = correctAnswers * 100 / quiz.Questions.Count;
        quizResult.UserId = quiz.UserId;
        quizResult.QuizId = quiz.Id;
        quizResult.StudentId = studentId;

        quizResult = await this.quizResultRepository
            .UpdateQuizResultAsync(quizResult);

        return this.mapper.Map<QuizResultDto>(quizResult);
    }
}
