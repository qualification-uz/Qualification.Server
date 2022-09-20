using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Questions;
using Qualification.Service.Extensions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class QuizService : IQuizService
{
    private readonly IQuestionRepository questionRepository;
    private readonly IConfiguration configuration;
    
    public QuizService(IQuestionRepository questionRepository, IConfiguration configuration)
    {
        this.questionRepository = questionRepository;
        this.configuration = configuration;
    }
    
    public IEnumerable<Question> GetAll(long subjectId, bool isForTeacher)
    {
        int questionCount = int.Parse(configuration.GetSection("Quiz:QuestionCount").Value);

        var questions = questionRepository.SelectAllQuestions()
            .Where(p => p.IsForTeacher.Equals(isForTeacher) && p.SubjectId == subjectId)
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
}