using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.IRepositories;
using Qualification.Domain.Configurations;
using Qualification.Domain.Entities.Questions;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Question;
using Qualification.Service.Exceptions;
using Qualification.Service.Extensions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository questionRepository;
    private readonly IMapper mapper;
    private readonly IAssetService assetService;

    public QuestionService(
        IQuestionRepository questionRepository,
        IMapper mapper,
        IAssetService assetService)
    {
        this.questionRepository = questionRepository;
        this.mapper = mapper;
        this.assetService = assetService;
    }

    public IEnumerable<QuestionDto> RetrieveAllQuestions(
        Filters filters, PaginationParams @params)
    {
        var questions = this.questionRepository
            .SelectAllQuestions()
            .Include(p => p.Answers)
            .ThenInclude(p => p.Assets)
            .Include(p => p.Assets)
            .AsQueryable();

        questions = filters
                .Aggregate(questions, (current, filter) => current.Filter(filter));

        return this.mapper.Map<IEnumerable<QuestionDto>>(questions)
            .ToPagedList(@params);
    }

    public async ValueTask<QuestionDto> RetrieveQuestionByIdAsync(long questionId)
    {
        var question = await this.questionRepository
            .SelectQuestionByIdAsync(questionId, includes: new[] { "Answers", "Assets", "Answers.Assets" });

        if (question is null)
            throw new NotFoundException("Couldn't find any question for given id");

        return this.mapper.Map<QuestionDto>(question);
    }

    public async ValueTask<QuestionDto> AddQuestionAsync(QuestionForCreationDto questionForCreationDto)
    {
        var question = this.mapper.Map<Question>(questionForCreationDto);
        var assets = new List<QuestionAsset>();

        if(questionForCreationDto.AssetIds is not null)
        {
            foreach (var assetId in questionForCreationDto.AssetIds)
            {
                assets.Add(new QuestionAsset { AssetId = assetId });
            }
        }

        question.Assets = assets;
        question = await this.questionRepository.InsertQuestionAsync(question);

        return this.mapper.Map<QuestionDto>(question);
    }

    public async ValueTask<QuestionDto> ModifyQuestionAsync(
        long questionId,
        QuestionForUpdateDto questionForUpdateDto)
    {
        var question = await this.questionRepository
            .SelectQuestionByIdAsync(questionId);

        if (question is null)
            throw new NotFoundException("Couldn't find any question for given id");

        if (!string.IsNullOrEmpty(questionForUpdateDto.Content))
            question.Content = questionForUpdateDto.Content;

        if (questionForUpdateDto.Type.HasValue)
            question.Type = questionForUpdateDto.Type.Value;

        if(questionForUpdateDto.SubjectId.HasValue)
            question.SubjectId = questionForUpdateDto.SubjectId.Value;
        
        if(questionForUpdateDto.Level.HasValue)
            question.Level = questionForUpdateDto.Level.Value;

        if(questionForUpdateDto.CorrectAnswers.HasValue)
            question.CorrectAnswers = questionForUpdateDto.CorrectAnswers.Value;

        if(questionForUpdateDto.IsForTeacher.HasValue)
            question.IsForTeacher = questionForUpdateDto.IsForTeacher.Value;

        question.UpdatedAt = DateTime.UtcNow;

        question = await this.questionRepository
            .UpdateQuestionAsync(question);

        return this.mapper.Map<QuestionDto>(question);
    }

    public async ValueTask<QuestionDto> RemoveQuestionAsync(long questionId)
    {
        var question = await this.questionRepository
            .SelectQuestionByIdAsync(questionId);

        if (question is null)
            throw new NotFoundException("Couldn't find any question for given id");

        question = await this.questionRepository
            .DeleteQuestionAsync(question);

        return this.mapper.Map<QuestionDto>(question);
    }

    public async ValueTask<QuestionDto> AddQuestionAnswersAsync(
        long questionId,
        IReadOnlyList<QuestionAnswerForCreationDto> questionAnswerForCreationDtos)
    {
        var question = await this.questionRepository
            .SelectQuestionByIdAsync(questionId, new[] { "Answers" });

        if (question is null)
            throw new NotFoundException("Couldn't find any question for given id");

        if (question.Answers is null)
            question.Answers = new List<QuestionAnswer>();

        foreach(var answerDto in questionAnswerForCreationDtos)
        {
            var answer = this.mapper.Map<QuestionAnswer>(answerDto);
            
            if (answer.Assets is null)
                answer.Assets = new List<QuestionAnswerAsset>();

            if (answerDto.AssetIds is not null)
            {

                foreach (var assetId in answerDto.AssetIds)
                {
                    answer.Assets.Add(new QuestionAnswerAsset { AssetId = assetId });
                }
            }
            question.Answers.Add(answer);
        }
        
        question.UpdatedAt = DateTime.UtcNow;

        question = await this.questionRepository.UpdateQuestionAsync(question);

        return this.mapper.Map<QuestionDto>(question);
    }

    public async ValueTask<QuestionDto> AddQuestionAnswerAsync(
        long questionId,
        QuestionAnswerForCreationDto questionAnswerForCreationDto)
    {
        var question = await this.questionRepository
            .SelectQuestionByIdAsync(questionId, new[] { "Answers" });

        if (question is null)
            throw new NotFoundException("Couldn't find any question for given id");

        if (question.Answers is null)
            question.Answers = new List<QuestionAnswer>();

        var answer = this.mapper.Map<QuestionAnswer>(questionAnswerForCreationDto);

        if (answer.Assets is null)
            answer.Assets = new List<QuestionAnswerAsset>();

       if(questionAnswerForCreationDto.AssetIds is not null)
        {
            foreach (var assetId in questionAnswerForCreationDto.AssetIds)
            {
                answer.Assets.Add(new QuestionAnswerAsset { AssetId = assetId });
            }
        }

        question.Answers.Add(answer);
        question.UpdatedAt = DateTime.UtcNow;

        question = await this.questionRepository.UpdateQuestionAsync(question);

        return this.mapper.Map<QuestionDto>(question);
    }

    public async ValueTask<QuestionDto> ModifyQuestionAnswerAsync(
        long questionId,
        long answerId,
        QuestionAnswerForUpdateDto questionAnswerForUpdateDto)
    {
        var question = await this.questionRepository
            .SelectQuestionByIdAsync(questionId, new[] { "Answers" });

        if (question is null)
            throw new NotFoundException("Couldn't find any question for given id");

        var questionAnswer = question.Answers
            .FirstOrDefault(answer => answer.Id == answerId);

        if (questionAnswer is null)
            throw new NotFoundException("Couldn't find any answer for given id");

        if(!string.IsNullOrEmpty(questionAnswerForUpdateDto.Content))
            questionAnswer.Content = questionAnswerForUpdateDto.Content;

        if(questionAnswerForUpdateDto.IsCorrect.HasValue)
            questionAnswer.IsCorrect = questionAnswerForUpdateDto.IsCorrect.Value;

        question.UpdatedAt = DateTime.UtcNow;

        question = await this.questionRepository.UpdateQuestionAsync(question);

        return this.mapper.Map<QuestionDto>(question);
    }

    public async ValueTask<QuestionDto> RemoveQuestionAnswerAsync(long questionId, long answerId)
    {
        var question = await this.questionRepository
            .SelectQuestionByIdAsync(questionId, new[] { "Answers" });

        if (question is null)
            throw new NotFoundException("Couldn't find any question for given id");

        var questionAnswer = question.Answers
            .FirstOrDefault(answer => answer.Id == answerId);

        if (questionAnswer is null)
            throw new NotFoundException("Couldn't find any answer for given id");

        question.Answers.Remove(questionAnswer);

        question.UpdatedAt = DateTime.UtcNow;

        question = await this.questionRepository.UpdateQuestionAsync(question);

        return this.mapper.Map<QuestionDto>(question);
    }
}
