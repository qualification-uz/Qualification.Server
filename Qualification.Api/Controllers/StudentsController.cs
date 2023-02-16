using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    /// <summary>
    /// Student'lar uchun controller
    /// </summary>
    [ApiController]
    [Route("api/students")]
    [Authorize("SchoolPolicy")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;
        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        /// <summary>
        /// Student'lar ro'yhatini olish
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        [HttpGet("{schoolId}/{applicationId}")]
        public async ValueTask<IActionResult> GetStudentsAsync(long schoolId, long applicationId) =>
            Ok(await this.studentService.RetrieveAllAsync(schoolId, applicationId));

        /// <summary>
        /// Student'ni olish
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("login"), AllowAnonymous]
        public async ValueTask<IActionResult> LoginAsync([FromBody] string password) =>
            Ok(await this.studentService.RetrieveByPasswordAsync(password));
    }
}
