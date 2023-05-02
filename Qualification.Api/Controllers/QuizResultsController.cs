using Microsoft.AspNetCore.Mvc;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [Route("api/results")]
    [ApiController]
    public class QuizResultsController : ControllerBase
    {
        private readonly IQuizResultService quizResultService;

        public QuizResultsController(IQuizResultService quizResultService)
        {
            this.quizResultService = quizResultService;
        }

        [HttpGet("{quizId}")]
        public async ValueTask<IActionResult> GetTeacherQuizResultAsync(long quizId, long? studentId = null) =>
            Ok(await this.quizResultService.RetrieveTeacherQuizResultAsync(quizId, studentId));


        [HttpGet("student-quiz/{quizId}")]
        public async ValueTask<IActionResult> GetStudentQuizResultAsync(long quizId, long studentId) =>
            Ok(await this.quizResultService.RetrieveStudentQuizResultAsync(quizId, studentId));


    }
}
