using Microsoft.AspNetCore.Http;

namespace Qualification.Service.Interfaces;

public interface IAssetService
{
    ValueTask<(string fileName, string filePath)> SaveFileAsync(IFormFile file, string folder = "Images");
    bool DeleteFile(string fileName, string folder = "Images");
    ValueTask<IReadOnlyList<string>> RetrieveLinksByIdsAsync(IEnumerable<long> ids);
}
