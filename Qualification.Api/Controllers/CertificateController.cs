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
        private readonly ISertificateService sertificateService;

        public CertificateController(IQuizService quizService, ISertificateService sertificateService)
        {
            this.quizService = quizService;
            this.sertificateService = sertificateService;
        }

        /// <summary>
        /// Get certificate by quizId
        /// </summary>
        /// <param name="sertDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> PostCertificateAsync(SertificateForCreationDto sertDto) =>
            File(await this.sertificateService.GenerateSertificateAsync(sertDto), "application/octet-stream", "sertificate.png");
    }
}
