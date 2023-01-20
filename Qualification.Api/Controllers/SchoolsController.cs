using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qualification.Domain.Configurations;
using Qualification.Service.AvloniyClient;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [Route("api/schools")]
    [ApiController]
    [Authorize(Policy = "SchoolPolicy")]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolService schoolService;

        public SchoolsController(ISchoolService schoolService)
        {
            this.schoolService = schoolService;
        }

        /// <summary>
        /// Fanlar ro'yxatini olish
        /// </summary>
        /// <returns></returns>
        [HttpGet("subjects")]
        [AllowAnonymous]
        public async ValueTask<IActionResult> GetAllSubjectsAsync() =>
            Ok(await this.schoolService.RetrieveAllSubjectsAsync());

        /// <summary>
        /// Sinflar raqamlari ro'yxatini olish
        /// </summary>
        /// <returns></returns>
        [HttpGet("grades")]
        public async ValueTask<IActionResult> GetAllGradesAsync() =>
            Ok(await this.schoolService.RetrieveAllGradesAsync());

        /// <summary>
        /// Sinf harflari ro'yxatini olish
        /// </summary>
        /// <returns></returns>
        [HttpGet("grade-letters")]
        public async ValueTask<IActionResult> GetAllGradeLettersAsync() =>
            Ok(await this.schoolService.RetrieveAllGradeLettersAsync());

        /// <summary>
        /// O'quv yili ro'yxatini olish
        /// </summary>
        /// <returns></returns>
        [HttpGet("years")]
        public async ValueTask<IActionResult> GetAllSchoolYearsAsync() =>
            Ok(await this.schoolService.RetrieveAllSchoolYearsAsync());

        /// <summary>
        /// O'qituvchilar ro'yxatini olish
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/teachers")]
        public IActionResult GetAllTeachersAsync(
            int id,
            [FromQuery]PaginationParams paginationParams) =>
            Ok(this.schoolService.RetrieveAllTeachers(id, paginationParams));

        /// <summary>
        /// Maktab o'qituvchisini o'chirish
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [HttpDelete("{id}/teachers/{teacherId}")]
        public async ValueTask<IActionResult> DeleteTeacherAsync(
            int id,
            int teacherId) =>
            Ok(await this.schoolService.RemoveTeacherAsync(id, teacherId));

        /// <summary>
        /// O'qituvchini PINFL orqali ro'yxatga olish 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teacherPinflDto"></param>
        /// <returns></returns>
        [HttpPost("{id}/teachers/pinfl")]
        public async ValueTask<IActionResult> PostTeacherByPinflAsync(
            int id, [FromQuery] TeacherPinflDto teacherPinflDto) =>
                Ok(await this.schoolService.RetrieveTeacherByPinflAsync(id, teacherPinflDto));
    }
}
