using Qualification.Domain.Enums;
using Qualification.Service.DTOs.Application;
using Qualification.Service.DTOs.Users;

namespace Qualification.Service.DTOs.Quizzes;

public class QuizDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public int QuestionCount { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public QuizStatus Status { get; set; }
    public long UserId { get; set; }
    public UserDto User { get; set; }
    public ApplicationDto Application { get; set; }
}
