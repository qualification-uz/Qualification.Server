using Microsoft.AspNetCore.Mvc;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
#pragma warning disable
    [ApiController]
    [Route("api/insights")]
    public class InsightsController : ControllerBase
    {
        private readonly IInsightService insightService;
        public InsightsController(IInsightService insightService)
        {
            this.insightService=insightService;
        }

        /// <summary>
        /// Ushbu quiz bo'yicha 5, 4 va 3 baxo olganlar sonini qaytaradi
        /// </summary>
        /// <param name="quizId"></param>
        /// <returns></returns>
        [HttpGet("quizzes/top-score-count/{quizId}")]
        public async ValueTask<IActionResult> GetQuizResultTops(int quizId)
            => Ok(await this.insightService.GetCountOfTopInQuizResultAsync(quizId));

        /// <summary>
        /// Platforma bo'yicha status holatlari bo'yicha a'zolar sonini qaytaradi
        /// </summary>
        /// <returns></returns>
        [HttpGet("applications/status")]
        public async ValueTask<IActionResult> GetCountOfApplicationStatus()
            => Ok(await this.insightService.GetCountOfApplicationStatusAsync());

        /// <summary>
        /// Bir maktabning application status holatlari bo'yicha a'zolar sonini qaytaradi
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        [HttpGet("applications/status/schools/{schoolId}")]
        public async ValueTask<IActionResult> GetCountOfApplicationStatusBySchoolId(int schoolId)
            => Ok(await this.insightService.GetCountOfApplicationStatusBySchoolIdAsync(schoolId));

        /// <summary>
        /// Bir o'qituvchining application status holatlari bo'yicha a'zolar sonini qaytaradi
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [HttpGet("applications/status/teachers/{teacherId}")]
        public async ValueTask<IActionResult> GetCountOfApplicationStatusByTeacherId(int teacherId)
            => Ok(await this.insightService.GetCountOfApplicationStatusByTeacherIdAsync(teacherId));
    }
}
