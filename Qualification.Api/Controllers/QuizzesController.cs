using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public IActionResult GetQuestions(long subjectId, bool isForTeacher)
        => Ok(quizService.GetAll(subjectId, isForTeacher));
}