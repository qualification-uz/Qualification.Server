using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [Route("api/results")]
    [ApiController]
    public class QuizResults : ControllerBase
    {
        private readonly IQuizResultService quizResultService;

        public QuizResults(IQuizResultService quizResultService)
        {
            this.quizResultService = quizResultService;
        }

        [HttpGet("{quizId}")]
        public async ValueTask<IActionResult> GetQuizResultAsync(long quizId) =>
            Ok(this.quizResultService.RetrieveQuizResultAsync(quizId));
    }
}
