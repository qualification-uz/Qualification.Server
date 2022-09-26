using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Questions;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.Extensions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class QuizService : IQuizService
{
    private readonly IQuestionRepository questionRepository;
    private readonly IQuestionAnswerRepository questionAnswerRepository;
    private readonly IConfiguration configuration;
    
    public QuizService(IQuestionRepository questionRepository, IConfiguration configuration, 
        IQuestionAnswerRepository questionAnswerRepository)
    {
        this.questionRepository = questionRepository;
        this.configuration = configuration;
        this.questionAnswerRepository = questionAnswerRepository;
    }
    
    public IEnumerable<Question> GetAll(long subjectId, bool isForTeacher)
    {
        int questionCount = int.Parse(configuration.GetSection("Quiz:QuestionCount").Value);

        var questions = questionRepository.SelectAllQuestions()
            .Where(p => p.IsForTeacher.Equals(isForTeacher) && p.SubjectId == subjectId)
            .Include(p => p.Answers)
            .ThenInclude(p => p.Assets)
            .Include(p => p.Assets)
            .ToDictionary(question => question.Id);

        HashSet<long> ids = new HashSet<long>();
        long minIndex = questions.Keys.Min();
        long maxIndex = questions.Keys.Max();
        var random = new Random();

        if (questions.Count < questionCount)
            questionCount = questions.Count;
        
        while (ids.Count != questionCount)
        {
            var randomId = random.NextInt64(minIndex, maxIndex + 1);
            
            if (questions.ContainsKey(randomId))
                ids.Add(randomId);
        }

        // shuffle
        var idsArray = ids.ToArray();
        random.Shuffle(idsArray);

        var query = ids.Join(questions, p => p, p => p.Key, (id, question) => question.Value);
        
        return query;
    }

    public async Task<CheckedQuizResultDto> CheckQuizAsync(CheckedQuizInputDto[] answers)
    {
        var quizResult = new CheckedQuizResultDto()
        {
            InCorrectQuestions = new List<Question>()
        };

        var questionIds = answers.Select(p => p.QuestionId);
        var questionAnswers = questionAnswerRepository.SelectAllQuestions()
            .Include(p => p.Question)
            .Where(p => questionIds.Contains(p.QuestionId)).ToDictionary(p => p.Id);


        foreach (var answer in answers)
        {
            var questionAnswer = questionAnswers
                .FirstOrDefault(p => p.Value.QuestionId == answer.QuestionId);
            
            if(!questionAnswer.Value.IsCorrect)
                quizResult.InCorrectQuestions.Add(questionAnswer.Value.Question);
        }

        quizResult.InCorrectCount = quizResult.InCorrectQuestions.Count;
        quizResult.CorrectCount = questionAnswers.Count - quizResult.InCorrectCount;
        quizResult.Percent = (quizResult.CorrectCount * 100) / (questionAnswers.Count == 0 ? 100 : questionAnswers.Count);
        
        return quizResult;
    }
}