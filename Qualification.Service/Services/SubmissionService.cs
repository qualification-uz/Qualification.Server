using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Quizes;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.Exceptions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class SubmissionService : ISubmissionService
{
    private readonly IMapper mapper;
    private readonly IStudentRepository studentRepository;
    private readonly IQuestionRepository questionRepository;
    private readonly ISubmissionRepository submissionRepository;
    private readonly IQuizQuestionRepository quizQuestionRepository;
    private readonly IQuestionAnswerRepository questionAnswerRepository;
    private readonly IQuizQuestionOptionRepository questionOptionRepository;
    public SubmissionService(
        IMapper mapper,
        IStudentRepository studentRepository,
        IQuestionRepository questionRepository,
        ISubmissionRepository submissionRepository,
        IQuizQuestionRepository quizQuestionRepository,
        IQuizQuestionOptionRepository questionOptionRepository,
        IQuestionAnswerRepository questionAnswerRepository)
    {
        this.mapper = mapper;
        this.studentRepository = studentRepository;
        this.questionRepository = questionRepository;
        this.submissionRepository = submissionRepository;
        this.quizQuestionRepository = quizQuestionRepository;
        this.questionOptionRepository = questionOptionRepository;
        this.questionAnswerRepository = questionAnswerRepository;
    }

    public async ValueTask<SubmissionDto> CreateSubmissionAsync(SubmissionForCreationDto submissionDto)
    {
        var quizId = submissionDto.QuizId;
        var questionId = submissionDto.QuizQuestionId;
        var answerId = submissionDto.QuestionOptionId;
        var quizQuestion = await this.quizQuestionRepository
            .SelectAllQuizQuestions()
            .FirstOrDefaultAsync(qq => qq.QuestionId == submissionDto.QuizQuestionId);
        if (quizQuestion is null)
            throw new NotFoundException("Couldn't find quiz question for given id");

        var quizQuestionOption = await this.questionOptionRepository
            .SelectAllQuizQuestions()
            .OrderBy(p => p.Id)
            .LastOrDefaultAsync(p => p.QuizOptionId == submissionDto.QuestionOptionId);
        if (quizQuestionOption is null)
            throw new NotFoundException("Couldn't find quiz question option for given id");

        var questionAnswers = await this.questionAnswerRepository.SelectAllQuestions().ToListAsync();
        var quizQuestionAnswer = questionAnswers.Find(t => t.Id == quizQuestionOption.QuizOptionId);
            
        submissionDto.QuestionOptionId = quizQuestionOption.Id;
        submissionDto.QuizQuestionId = quizQuestion.Id;

        var submission = this.mapper.Map<Submission>(submissionDto);
        
        if(quizQuestionAnswer.IsCorrect)
            submission.IsCorrect = true;

        submission.IsForStudent = false;

        // check for exist submission
        var existSubmission = await submissionRepository
            .SelectAllSubmissions()
            .Where(s => s.QuestionOptionId == submissionDto.QuestionOptionId)
            .FirstOrDefaultAsync();
        if(existSubmission != null)
            submission = await this.submissionRepository.UpdateSubmissionAsync(submission);
        else 
            submission = await this.submissionRepository.InsertSubmissionAsync(submission);

        var resultSubmission = new SubmissionDto
        {
            Id = submission.Id,
            QuizId = quizId,
            QuizQuestionId = questionId,
            QuestionOptionId = answerId,
            UserId = submissionDto.UserId,
        };
        return resultSubmission;
    }

    public async ValueTask<SubmissionForStudentDto> CreateSubmissionForStudentAsync(SubmissionForStudentForCreationDto submissionDto)
    {
        var quizQuestion = await this.quizQuestionRepository
            .SelectAllQuizQuestions()
            .FirstOrDefaultAsync(qq => qq.QuestionId == submissionDto.QuizQuestionId);
        if (quizQuestion is null)
            throw new NotFoundException("Couldn't find quiz question for given id");

        var quizQuestionOption = await this.questionOptionRepository
            .SelectAllQuizQuestions()
            .OrderBy(p => p.Id)
            .LastOrDefaultAsync(p => p.QuizOptionId == submissionDto.QuestionOptionId);
        if (quizQuestionOption is null)
            throw new NotFoundException("Couldn't find quiz question option for given id");

        var student = await this.studentRepository.SelectStudentByIdAsync(submissionDto.StudentId);
        if (student is null)
            throw new NotFoundException("Couldn't student for given id");

        var questionAnswers = await this.questionAnswerRepository.SelectAllQuestions().ToListAsync();
        var quizQuestionAnswer = questionAnswers.Find(t => t.Id == quizQuestionOption.QuizOptionId);

        submissionDto.QuestionOptionId = quizQuestionOption.Id;
        submissionDto.QuizQuestionId = quizQuestion.Id;

        var submission = this.mapper.Map<Submission>(submissionDto);

        if (quizQuestionAnswer.IsCorrect)
            submission.IsCorrect = true;

        submission.IsForStudent = true;

        // check for exist submission
        var existSubmission = await submissionRepository
            .SelectAllSubmissions()
            .Where(s => s.QuestionOptionId == submissionDto.QuestionOptionId)
            .FirstOrDefaultAsync();
        if (existSubmission != null)
            submission = await this.submissionRepository.DeleteSubmissionAsync(submission);
        else
            submission = await this.submissionRepository.InsertSubmissionAsync(submission);

        return this.mapper.Map<SubmissionForStudentDto>(submission);
    }

    public IEnumerable<SubmissionDto> RetrieveAllSubmissions()
    {
        var submissions = this.submissionRepository
            .SelectAllSubmissions()
            .OrderByDescending(submission => submission.CreatedAt);

        return this.mapper.Map<IEnumerable<SubmissionDto>>(submissions);
    }

    public IEnumerable<SubmissionDto> RetrieveAllSubmissionsByQuizId(long quizId)
    {
        var submissions = this.submissionRepository
            .SelectAllSubmissions()
            .Where(submission => submission.QuizId == quizId)
            .OrderByDescending(submission => submission.CreatedAt);

        return this.mapper.Map<IEnumerable<SubmissionDto>>(submissions);
    }

    public async ValueTask<SubmissionDto> RetrieveSubmissionByIdAsync(long submissionId)
    {
        var submission = await this.submissionRepository
            .SelectSubmissionByIdAsync(submissionId);

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
        var submission = await this.submissionRepository
            .SelectSubmissionByIdAsync(submissionId);

        if (submission is null)
        {
            throw new NotFoundException("Couldn't find submission for given id");
        }

        submission.QuestionOptionId = submissionDto.QuestionOptionId;

        submission = await this.submissionRepository
            .UpdateSubmissionAsync(submission);

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
