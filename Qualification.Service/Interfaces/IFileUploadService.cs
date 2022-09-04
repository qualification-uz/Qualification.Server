using Microsoft.AspNetCore.Http;
using Qualification.Service.DTOs;

namespace Qualification.Service.Interfaces;

public interface IFileUploadService
{
    ValueTask<AssetDto> RetrieveFileByIdAsync(long fileId);
    ValueTask<IEnumerable<AssetDto>> SaveFilesAsync(IReadOnlyList<IFormFile> formFiles);
    ValueTask<AssetDto> ModifyFileAsync(long fileId, IFormFile formFile);
    ValueTask<AssetDto> RemoveFileAsync(long fileId);
}
