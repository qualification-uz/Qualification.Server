using Microsoft.AspNetCore.Mvc;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers;

[ApiController]
[Route("api/quizzes")]
public class QuizzesController : ControllerBase
{
    private readonly IQuizService quizService;

    public QuizzesController(IQuizService quizService)
    {
        this.quizService = quizService;
    }

    [HttpPost]
    public async ValueTask<IActionResult> PostQuizAsync(QuizForCreationDto quizForCreationDto) =>
        Ok(await this.quizService.CreateQuizAsync(quizForCreationDto));

    [HttpPost("bulk")]
    public async ValueTask<IActionResult> PostBulkQuizAsync(QuizForBulkCreationDto quizForBulkCreationDto)
    {
        await this.quizService.CreateBulkQuizAsync(quizForBulkCreationDto);

        return Ok(new { Result = "Success" });
    }

    [HttpGet]
    public IActionResult GetAllQuizzes() =>
        Ok(this.quizService.RetrieveAllQuizzes());

    [HttpGet("{id}")]
    public async ValueTask<IActionResult> GetQuizByIdAsync(long id) =>
        Ok(await this.quizService.RetrieveQuizByIdAsync(id));

    [HttpPut("{id}")]
    public async ValueTask<IActionResult> PutQuizAsync(long id, QuizForUpdateDto quizForUpdateDto) =>
        Ok(await this.quizService.ModifyQuizAsync(id, quizForUpdateDto));

    [HttpPatch("{id}/status")]
    public async ValueTask<IActionResult> PatchQuizStatusAsync(long id, QuizStatus quizStatus) =>
        Ok(await this.quizService.ModifyQuizStatusAsync(id, quizStatus));

    [HttpDelete("{id}")]
    public async ValueTask<IActionResult> DeleteQuizAsync(long id) =>
        Ok(await this.quizService.RemoveQuizAsync(id));

    [HttpGet("{id}/questions")]
    public async ValueTask<IActionResult> GetQuizRelatedQuestions(long id) =>
        Ok(await this.quizService.RetrieveQuizQuestions(id));

    [HttpGet("status")]
    public IActionResult GetQuizStatuses() =>
        Ok(this.quizService.RetrieveQuizStatuses());
}