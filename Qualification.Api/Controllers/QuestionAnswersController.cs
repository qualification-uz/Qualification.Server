using Microsoft.AspNetCore.Mvc;
using Qualification.Service.DTOs.Question;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [ApiController]
    [Route("api/questions/{id}/answers")]
    public class QuestionAnswersController : ControllerBase
    {
        private readonly IQuestionService questionService;

        public QuestionAnswersController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        /// <summary>
        /// Test javobini yaratish
        /// </summary>
        /// <param name="id"></param>
        /// <param name="questionAnswerForCreationDtos"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> PostQuestionAnswersAsync(
            long id,
            [FromForm] QuestionAnswerForCreationDto questionAnswerForCreationDtos) =>
                Ok(await this.questionService.AddQuestionAnswerAsync(id, questionAnswerForCreationDtos));

        /// <summary>
        /// Test javobini o'zgartirish
        /// </summary>
        /// <param name="id"></param>
        /// <param name="answerId"></param>
        /// <param name="questionAnswerForUpdateDto"></param>
        /// <returns></returns>
        [HttpPatch("{answerId}")]
        public  async ValueTask<IActionResult> PatchQuestionAnswerAsync(
            long id,
            long answerId,
            [FromForm] QuestionAnswerForUpdateDto questionAnswerForUpdateDto) =>
                Ok(await this.questionService.ModifyQuestionAnswerAsync(id, answerId, questionAnswerForUpdateDto));
    }
}
