using Microsoft.AspNetCore.Mvc;
using Qualification.Service.DTOs.Sertificate;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [ApiController]
    [Route("api/certificate")]
    public class CertificateController : ControllerBase
    {
        private readonly ISertificateService sertificateService;

        public CertificateController(ISertificateService sertificateService)
        {
            this.sertificateService = sertificateService;
        }

        /// <summary>
        /// Generate certificate
        /// </summary>
        /// <param name="sertificateForCreationDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> PostCertificateAsync([FromBody] SertificateForCreationDto sertificateForCreationDto) =>
            File(await this.sertificateService.GenerateSertificateAsync(sertificateForCreationDto), "octet/stream", "sertificate.png");
    }
}
