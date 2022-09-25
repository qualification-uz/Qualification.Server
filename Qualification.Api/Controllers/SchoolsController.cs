using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAllTeachersAsync(int id) =>
            Ok(this.schoolService.RetrieveAllTeachers(id));

        /// <summary>
        /// O'qituvchini PINFL orqali ro'yxatga olish 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teacherPinflDto"></param>
        /// <returns></returns>
        [HttpPost("{id}/teachers/pinfl")]
        public async ValueTask<IActionResult> PostTeacherByPinflAsyn(
            int id, [FromQuery] TeacherPinflDto teacherPinflDto) =>
                Ok(await this.schoolService.RetrieveTeacherByPinfl(id, teacherPinflDto));
    }
}
