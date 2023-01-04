using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualification.Domain.Configurations;
using Qualification.Service.DTOs;
using Qualification.Service.DTOs.Question;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [ApiController]
    [Route("api/questions")]
    [Authorize(Policy = "TestPolicy")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService questionService;

        public QuestionsController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        /// <summary>
        /// Barcha testlar ro'yxatini olish
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllQuestions(
            [FromQuery(Name = "filter")] Filters filters,
            [FromQuery] PaginationParams @params) =>
            Ok(this.questionService.RetrieveAllQuestions(filters, @params));

        /// <summary>
        /// Id bo'yicha test olish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetQuestionByIdAsync(long id) =>
            Ok(await this.questionService.RetrieveQuestionByIdAsync(id));

        /// <summary>
        /// Yangi test yaratish
        /// </summary>
        /// <param name="questionForCreationDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> PostQuestionAsync([FromBody]QuestionForCreationDto questionForCreationDto) =>
            Ok(await this.questionService.AddQuestionAsync(questionForCreationDto));

        /// <summary>
        /// Test ma'lumotlarini o'zgartirish
        /// </summary>
        /// <param name="id"></param>
        /// <param name="questionForUpdateDto"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async ValueTask<IActionResult> PatchQuestionAsync(
            long id, [FromBody]QuestionForUpdateDto questionForUpdateDto) =>
            Ok(await this.questionService.ModifyQuestionAsync(id, questionForUpdateDto));

        /// <summary>
        /// Testni o'chirib yuborish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeleteQuestionAsync(long id) =>
            Ok(await this.questionService.RemoveQuestionAsync(id));
    }
}
