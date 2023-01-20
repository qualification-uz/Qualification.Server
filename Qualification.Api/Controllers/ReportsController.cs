using Microsoft.AspNetCore.Mvc;
using Qualification.Domain.Configurations;
using Qualification.Domain.Entities.Users;
using Qualification.Service.DTOs;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
#pragma warning disable
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService reportService;
        public ReportsController(IReportService reportService)
        {
            this.reportService=reportService;
        }
        
        /// <summary>
        /// Export filtered teachers to excel
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="params"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("teachers/{schoolId}")]
        public async ValueTask<IActionResult> GetTeachersReportAsync(
            int schoolId, 
            [FromQuery]PaginationParams @params,
            [FromQuery] Filters filter) =>
            File(await this.reportService.ExportTeachersToExcelAsync(schoolId, @params, filter),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "teachers.xlsx");

        /// <summary>
        /// Export filtered applications to excel
        /// </summary>
        /// <param name="params"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("applications")]
        public async ValueTask<IActionResult> GetApplicationsReportAsync(
            [FromQuery] PaginationParams @params,
            [FromQuery] Filters filter) =>
            File(await this.reportService.ExportApplicationsToExcelAsync(@params, filter),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "applications.xlsx");

        /// <summary>
        /// Export filtered applications by schoolId to excel
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="params"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("applications/schools/{schoolId}")]
        public async ValueTask<IActionResult> GetApplicationsBySchoolIdReportAsync(
            int schoolId,
            [FromQuery] PaginationParams @params,
            [FromQuery] Filters filter) =>
            File(await this.reportService.ExportApplicationsBySchoolIdToExcelAsync(schoolId, @params, filter),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "report.xlsx");

        /// <summary>
        /// Export filtered applications by teacherId to excel
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="params"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("applications/teachers/{teacherId}")]
        public async ValueTask<IActionResult> GetApplicationsByTeacherIdReportAsync(
            int teacherId,
            [FromQuery] PaginationParams @params,
            [FromQuery] Filters filter) =>
            File(await this.reportService.ExportApplicationsBySchoolIdToExcelAsync(teacherId, @params, filter),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "applications.xlsx");

        /// <summary>
        /// Export filtered paymentRequests to excel
        /// </summary>
        /// <param name="params"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("payment-requests")]
        public async ValueTask<IActionResult> GetPaymentRequestReportAsync(
            [FromQuery] PaginationParams @params,
            [FromQuery] Filters filter) =>
            File(await this.reportService.ExportPaymentsToExcelAsync(@params, filter),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "paymentRequests.xlsx");

    }
}
