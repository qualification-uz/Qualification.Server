using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Quizes;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.Exceptions;
using Qualification.Service.Helpers;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class SubmissionService : ISubmissionService
{
    private readonly IMapper mapper;
    private readonly AppDbContext appDbContext;
    private readonly IStudentRepository studentRepository;
    private readonly IQuestionRepository questionRepository;
    private readonly ISubmissionRepository submissionRepository;
    private readonly IQuizQuestionRepository quizQuestionRepository;
    private readonly IQuestionAnswerRepository questionAnswerRepository;
    private readonly ISubmissionResultRepository submissionResultRepository;
    private readonly IQuizQuestionOptionRepository questionOptionRepository;
    public SubmissionService(
        IMapper mapper,
        AppDbContext appDbContext,
        IStudentRepository studentRepository,
        IQuestionRepository questionRepository,
        ISubmissionRepository submissionRepository,
        IQuizQuestionRepository quizQuestionRepository,
        ISubmissionResultRepository submissionResultRepository,
        IQuizQuestionOptionRepository questionOptionRepository,
        IQuestionAnswerRepository questionAnswerRepository)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
        this.studentRepository = studentRepository;
        this.questionRepository = questionRepository;
        this.submissionRepository = submissionRepository;
        this.quizQuestionRepository = quizQuestionRepository;
        this.submissionResultRepository = submissionResultRepository;
        this.questionOptionRepository = questionOptionRepository;
        this.questionAnswerRepository = questionAnswerRepository;
    }

    public async ValueTask<SubmissionDto> CreateSubmissionAsync(SubmissionForCreationDto submissionDto)
    {
        var quizId = submissionDto.QuizId;
        var questionId = submissionDto.QuizQuestionId;
        var answerId = submissionDto.QuestionOptionId;

        var quizQuestion = await this.questionRepository
            .SelectAllQuestions()
            .FirstOrDefaultAsync(qq => qq.Id == submissionDto.QuizQuestionId);
        if (quizQuestion is null)
            throw new NotFoundException("Couldn't find quiz question for given id");

        var quizQuestionOption = await this.questionAnswerRepository
            .SelectAllQuestionAnswers()
            .Where(t => t.QuestionId == quizQuestion.Id).
            FirstOrDefaultAsync(t => t.Id == submissionDto.QuestionOptionId);
        if (quizQuestionOption is null)
            throw new NotFoundException("Couldn't find quiz question option for given id");
            
        submissionDto.QuestionOptionId = quizQuestionOption.Id;
        submissionDto.QuizQuestionId = quizQuestion.Id;

        var submission = new SubmissionResult()
        {
            QuestionOptionId = submissionDto.QuestionOptionId,
            QuizQuestionId = submissionDto.QuizQuestionId
        };

        // if it is student role
        if (HttpContextHelper.Role == UserRole.Student.ToString())
        {
            submission.IsForStudent = true;
            submission.StudentId = submissionDto.UserId;
            submission.QuizForStudentId = submissionDto.QuizId;
        }
        else
        {
            submission.IsForStudent = false;
            submission.UserId = submissionDto.UserId;
            submission.QuizId = submissionDto.QuizId;
        }

        if (quizQuestionOption.IsCorrect)
            submission.IsCorrect = true;

        // check for exist submission
        var existSubmission = await submissionResultRepository
            .SelectAllSubmissionResults()
            .FirstOrDefaultAsync(t => t.QuestionOptionId == submission.QuestionOptionId);
        if(existSubmission != null)
            submission = await this.submissionResultRepository.DeleteSubmissionResultAsync(existSubmission);
        else 
            submission = await this.submissionResultRepository.InsertSubmissionResultAsync(submission);

        // submission result should change for returning temporary
        submission.UserId = submissionDto.UserId;
        submission.QuizId = submissionDto.QuizId;

        return this.mapper.Map<SubmissionDto>(submission);
    }

    public async ValueTask<SubmissionForStudentDto> CreateSubmissionForStudentAsync(SubmissionForStudentForCreationDto submissionDto)
    {

        var quizId = submissionDto.QuizId;
        var questionId = submissionDto.QuizQuestionId;
        var answerId = submissionDto.QuestionOptionId;

        var quizQuestion = await this.questionRepository
            .SelectAllQuestions()
            .FirstOrDefaultAsync(qq => qq.Id == submissionDto.QuizQuestionId);
        if (quizQuestion is null)
            throw new NotFoundException("Couldn't find quiz question for given id");

        var quizQuestionOption = await this.questionAnswerRepository
            .SelectAllQuestionAnswers().
            Where(t => t.QuestionId == quizQuestion.Id).
            FirstOrDefaultAsync(t => t.Id == submissionDto.QuestionOptionId);
        if (quizQuestionOption is null)
            throw new NotFoundException("Couldn't find quiz question option for given id");

        submissionDto.QuestionOptionId = quizQuestionOption.Id;
        submissionDto.QuizQuestionId = quizQuestion.Id;

        var submission = new SubmissionResult()
        {
            StudentId = submissionDto.StudentId,
            QuestionOptionId = submissionDto.QuestionOptionId,
            QuizQuestionId = submissionDto.QuizQuestionId,
            QuizId = submissionDto.QuizId,
            IsForStudent = true
        };

        if (quizQuestionOption.IsCorrect)
            submission.IsCorrect = true;

        // check for exist submission
        var existSubmission = await submissionResultRepository
            .SelectAllSubmissionResults()
            .FirstOrDefaultAsync(t => t.QuestionOptionId == submission.QuestionOptionId);
        if (existSubmission != null)
            submission = await this.submissionResultRepository.DeleteSubmissionResultAsync(existSubmission);
        else
            submission = await this.submissionResultRepository.InsertSubmissionResultAsync(submission);

        return this.mapper.Map<SubmissionForStudentDto>(submission);
    }

    public IEnumerable<SubmissionDto> RetrieveAllSubmissions()
    {
        var submissions = this.submissionResultRepository
            .SelectAllSubmissionResults()
            .OrderByDescending(submission => submission.Id);

        return this.mapper.Map<IEnumerable<SubmissionDto>>(submissions);
    }

    public IEnumerable<SubmissionDto> RetrieveAllSubmissionsByQuizId(long quizId)
    {
        var submissions = this.submissionResultRepository
            .SelectAllSubmissionResults()
            .Where(submission => submission.QuizForStudentId == quizId)
            .OrderByDescending(submission => submission.Id);

        // if it is student 
        if (HttpContextHelper.Role == UserRole.Student.ToString())
        {
            foreach (var submission in submissions)
            {
                submission.QuizId = submission.QuizForStudentId;
                submission.UserId = submission.StudentId;
            }
        }

        return this.mapper.Map<IEnumerable<SubmissionDto>>(submissions);
    }

    public async ValueTask<SubmissionDto> RetrieveSubmissionByIdAsync(long submissionId)
    {
        var submission = await this.submissionResultRepository
            .SelectSubmissionResultByIdAsync(submissionId);

        if (submission is null)
        {
            throw new NotFoundException("Couldn't find submission for given id");
        }

        return this.mapper.Map<SubmissionDto>(submission);
    }

    public async ValueTask<SubmissionDto> ModifySubmissionAsync(
        long submissionId,
        SubmissionForUpdateDto submissionDto)
    {
        var submission = await this.submissionResultRepository
            .SelectSubmissionResultByIdAsync(submissionId);

        if (submission is null)
        {
            throw new NotFoundException("Couldn't find submission for given id");
        }

        submission.QuestionOptionId = submissionDto.QuestionOptionId;

        submission = await this.submissionResultRepository
            .UpdateSubmissionResultAsync(submission);

        return this.mapper.Map<SubmissionDto>(submission);
    }

    public async ValueTask<SubmissionDto> RemoveSubmissionAsync(long submissionId)
    {
        var submission = await this.submissionRepository
            .SelectSubmissionByIdAsync(submissionId);

        if (submission is null)
        {
            throw new NotFoundException("Couldn't find submission for given id");
        }

        submission = await this.submissionRepository
            .DeleteSubmissionAsync(submission);

        return this.mapper.Map<SubmissionDto>(submission);
    }

}
