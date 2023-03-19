using Microsoft.AspNetCore.Mvc;
using Qualification.Service.DTOs.Sertificate;
using Qualification.Service.Interfaces;
using System.IO;

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

        /// <summary>
        /// Gets certificate by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetCertificateAsync([FromRoute] string id)
        {
            var result = new FileStreamResult(await sertificateService.GetSertificateAsync(id), "image/png");   

            return result;
        }
    }
}
