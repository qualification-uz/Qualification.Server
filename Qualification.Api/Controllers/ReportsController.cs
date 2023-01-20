using Microsoft.AspNetCore.Mvc;
using Qualification.Domain.Configurations;
using Qualification.Service.DTOs;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
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
        
        
    }
}
