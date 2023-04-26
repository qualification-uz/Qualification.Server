using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qualification.Data.IRepositories;
using Qualification.Domain.Configurations;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.DTOs;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [Route("api/student-quiz")]
    [ApiController]
    public class StudentQuizesController : ControllerBase
    {
        private readonly IStudentQuizService studentQuizService;
        public StudentQuizesController(IStudentQuizService studentQuizService)
        {
            this.studentQuizService = studentQuizService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostQuizAsync(QuizForStudentCreationDto dto) =>
            Ok(await this.studentQuizService.CreateQuizAsync(dto));

        [HttpGet]
        public IActionResult GetAllQuizzes(
            [FromQuery(Name = "filter")] Filters filters,
            [FromQuery] PaginationParams @params) =>
            Ok(this.studentQuizService.RetrieveAllQuizzes(filters, @params));

        [HttpGet("{studentId}")]
        public async ValueTask<IActionResult> GetQuizByIdAsync(long studentId) =>
            Ok(await this.studentQuizService.RetrieveQuizByIdAsync(studentId));

        [HttpGet("application/{applicationId}")]
        public async ValueTask<IActionResult> GetQuizByApplicationIdAsync(long applicationId) =>
            Ok(await this.studentQuizService.RetrieveQuizByApplicationIdAsync(applicationId));

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> PutQuizAsync(long id, QuizForUpdateDto quizForUpdateDto) =>
            Ok(await this.studentQuizService.ModifyQuizAsync(id, quizForUpdateDto));

        [HttpPatch("{id}/status")]
        public async ValueTask<IActionResult> PatchQuizStatusAsync(long id, QuizStatus quizStatus) =>
            Ok(await this.studentQuizService.ModifyQuizStatusAsync(id, quizStatus));

        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeleteQuizAsync(long id) =>
            Ok(await this.studentQuizService.RemoveQuizAsync(id));

        [HttpGet("{studentId}/questions")]
        public async ValueTask<IActionResult> GetQuizRelatedQuestions(long studentId) =>
            Ok(await this.studentQuizService.RetrieveQuizQuestions(studentId));
    }
}
