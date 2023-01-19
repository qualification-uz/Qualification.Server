using AutoMapper;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Quizes;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.Exceptions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class SubmissionService : ISubmissionService
{
    private readonly ISubmissionRepository submissionRepository;
    private readonly IMapper mapper;
    public SubmissionService(
        ISubmissionRepository submissionRepository,
        IMapper mapper)
    {
        this.submissionRepository = submissionRepository;
        this.mapper = mapper;
    }

    public async ValueTask<SubmissionDto> CreateSubmissionAsync(SubmissionForCreationDto submissionDto)
    {
        var submission = this.mapper.Map<Submission>(submissionDto);

        submission = await this.submissionRepository
            .InsertSubmissionAsync(submission);

        return this.mapper.Map<SubmissionDto>(submission);
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
