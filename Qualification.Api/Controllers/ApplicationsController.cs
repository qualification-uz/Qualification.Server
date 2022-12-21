using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualification.Domain.Configurations;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Application;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [ApiController]
    [Route("api/applications")]
    [Authorize(Policy = "ApplicationPolicy")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        /// <summary>
        /// Yangi ariza yaratish
        /// </summary>
        /// <param name="applicationDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> PostApplicationAsync(
            [FromBody] ApplicationForCreationDto applicationDto) =>
                Ok(await this.applicationService.AddApplicationAsync(applicationDto));

        /// <summary>
        /// Barcha arizalarni olish
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllApplications(
            [FromQuery] PaginationParams @params,
            [FromQuery] Filter filter) =>
            Ok(this.applicationService.RetrieveAllApplications(@params, filter));

        /// <summary>
        /// Id bo'yicha arizani olish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async ValueTask<IActionResult> SelectApplicationByIdAsync(long id) =>
            Ok(await this.applicationService.RetrieveApplicationByIdAsync(id));

        /// <summary>
        /// Ariza ma'lumotlarini o'zgartirish berilgan id bo'yicha
        /// </summary>
        /// <param name="id"></param>
        /// <param name="applicationDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async ValueTask<IActionResult> PutApplicationAsync(
            long id, [FromBody] ApplicationForUpdateDto applicationDto) =>
                Ok(await this.applicationService.ModifyApplicationAsync(id, applicationDto));

        /// <summary>
        /// Arizani id'si bo'yicha o'chirib yuborish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeleteApplicationAsync(long id) =>
            Ok(await this.applicationService.RemoveApplicationAsync(id));

        /// <summary>
        /// Arizani statusini o'zgartirish
        /// </summary>
        /// <param name="id"></param>
        /// <param name="applicationStatus"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async ValueTask<IActionResult> PatchApplicationStatusAsync(
            long id, [FromQuery] ApplicationStatus applicationStatus) =>
                Ok(await this.applicationService.ModifyApplicationStatusAsync(id, applicationStatus));

        [HttpGet("status")]
        public IActionResult GetAllApplicationStatus() =>
            Ok(this.applicationService.RetrieveAllApplicationStatus());

        [HttpGet("schools/{id}")]
        public async ValueTask<IActionResult> GetApplicationsForSchool(
            long id,
            [FromQuery] PaginationParams @params,
            [FromQuery] Filter filter) =>
            Ok(this.applicationService.RetrieveApplicationsForSchool(id, @params, filter));

        [HttpGet("teachers/{id}")]
        public async ValueTask<IActionResult> GetApplicationsForTeacher(
            long id,
            [FromQuery] PaginationParams @params,
            [FromQuery] Filter filter) =>
            Ok(this.applicationService.RetrieveApplicationsForTeacher(id, @params, filter));
    }
}