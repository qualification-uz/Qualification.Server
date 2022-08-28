using Microsoft.AspNetCore.Mvc;
using Qualification.Service.DTOs.Application;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [ApiController]
    [Route("api/applications")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostApplicationAsync(
            [FromForm] ApplicationForCreationDto applicationDto) =>
                Ok(await this.applicationService.AddApplicationAsync(applicationDto));

        [HttpGet]
        public IActionResult GetAllApplications() => Ok(this.applicationService.RetrieveAllApplications());

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> SelectApplicationByIdAsync(long id) =>
            Ok(await this.applicationService.RetrieveApplicationByIdAsync(id));

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> PutApplicationAsync(
            long id, [FromForm] ApplicationForUpdateDto applicationDto) =>
                Ok(await this.applicationService.ModifyApplicationAsync(id, applicationDto));

        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeleteApplicationAsync(long id) =>
            Ok(await this.applicationService.RemoveApplicationAsync(id));
    }
}
