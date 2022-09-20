using Microsoft.AspNetCore.Mvc;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers;

[ApiController]
[Route("api/quizzes")]
public class QuizzesController : ControllerBase
{
    private readonly IQuizService quizService;
    
    public QuizzesController(IQuizService quizService)
    {
        this.quizService = quizService;
    }

    [HttpGet("questions")]
    public IActionResult GetQuestions(long subjectId, bool isForTeacher)
        => Ok(quizService.GetAll(subjectId, isForTeacher));

    [HttpPost("check-quiz")]
    public async Task<IActionResult> CheckQuizAsync(CheckedQuizInputDto[] dto)
        => Ok(await quizService.CheckQuizAsync(dto));
}