using Microsoft.AspNetCore.Mvc;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [ApiController]
    [Route("api/submissions")]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionService submissionService;

        public SubmissionsController(ISubmissionService submissionService)
        {
            this.submissionService = submissionService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostTeacherSubmissionAsync(
            SubmissionForCreationDto submissionForCreationDto) =>
            Ok(await this.submissionService.CreateSubmissionAsync(submissionForCreationDto));

        [HttpPost("student")]
        public async ValueTask<IActionResult> PostStudentSubmissionAsync(
            SubmissionForStudentForCreationDto submissionForCreationDto) =>
            Ok(await this.submissionService.CreateSubmissionForStudentAsync(submissionForCreationDto));

        [HttpGet("all")]
        public IActionResult GetAllSubmissions() =>
            Ok(this.submissionService.RetrieveAllSubmissions());

        [HttpGet("byQuiz/{quizId}")]
        public IActionResult GetAllSubmissionsByQuizId(long quizId) =>
            Ok(this.submissionService.RetrieveAllSubmissionsByQuizId(quizId));

        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetSubmissionByIdAsync(long id) =>
            Ok(await this.submissionService.RetrieveSubmissionByIdAsync(id));

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> PutSubmissionAsync(
            long id,
            SubmissionForUpdateDto submissionForUpdateDto) =>
            Ok(await this.submissionService.ModifySubmissionAsync(id, submissionForUpdateDto));

        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeleteSubmissionAsync(long id) =>
            Ok(await this.submissionService.RemoveSubmissionAsync(id));
    }
}
