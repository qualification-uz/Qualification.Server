using Microsoft.AspNetCore.Mvc;
using Qualification.Service.DTOs.Sertificate;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [ApiController]
    [Route("api/certificate")]
    public class CertificateController : ControllerBase
    {
        private readonly IQuizService quizService;

        public CertificateController(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        /// <summary>
        /// Get certificate by quizId
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> PostCertificateAsync(long quizId) =>
            File(await this.quizService.GenerateSertificateAsync(quizId), "application/octet-stream", "sertificate.png");
    }
}
