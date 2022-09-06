using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [ApiController]
    [Route("api/assets")]
    [Authorize(Policy = "All")]
    public class AssetsController : ControllerBase
    {
        private readonly IFileUploadService fileUploadService;

        public AssetsController(IFileUploadService fileUploadService)
        {
            this.fileUploadService = fileUploadService;
        }

        /// <summary>
        /// File id'si bo'yicha olish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async ValueTask<IActionResult> GetAssetByIdAsync(long id) =>
            Ok(await this.fileUploadService.RetrieveFileByIdAsync(id));

        /// <summary>
        /// Yangi file'lar yaratish
        /// </summary>
        /// <param name="formFiles"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> PostAssetsAsync(IReadOnlyList<IFormFile> formFiles) =>
            Ok(await this.fileUploadService.SaveFilesAsync(formFiles));

        /// <summary>
        /// File'ni yangilash
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async ValueTask<IActionResult> PatchAssetAsync(long id, IFormFile formFile) =>
            Ok(await this.fileUploadService.ModifyFileAsync(id, formFile));

        /// <summary>
        /// File'ni o'chirib yuborish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeleteAssetAsync(long id) =>
            Ok(await this.fileUploadService.RemoveFileAsync(id));
    }
}
