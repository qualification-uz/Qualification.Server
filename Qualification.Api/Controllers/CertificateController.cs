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
        /// <param name="quizId"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> PostSertificateAsync(long quizId) =>
            File(await this.quizService.GenerateSertificateAsync(quizId), "application/octet-stream", "sertificate.png");

        /// <summary>
        /// Gets certificate by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetCertificateAsync([FromRoute] string id)
        {
            var result = new FileStreamResult(await sertificateService.GetFileAsync(id), "image/png");   

            return result;
        }
    }
}
