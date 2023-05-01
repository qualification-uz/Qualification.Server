using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.IRepositories;
using Qualification.Service.DTOs.Quizzes;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class AssetService : IAssetService
{
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IFileUploadRepository fileUploadRepository;

    public AssetService(
        IWebHostEnvironment webHostEnvironment, IFileUploadRepository fileUploadRepository)
    {
        this.webHostEnvironment = webHostEnvironment;
        this.fileUploadRepository = fileUploadRepository;
    }

    public async ValueTask<(string fileName, string filePath)> SaveFileAsync(IFormFile file, string folder = "Images")
    {
        string serverPath = Path.Combine(webHostEnvironment.WebRootPath, folder);

        if (!Directory.Exists(serverPath))
            Directory.CreateDirectory(serverPath);

        string fileName = new String(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ', '-');
        fileName = fileName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(serverPath, fileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return (fileName, $"/{folder}/{fileName}");
    }

    public bool DeleteFile(string fileName, string folder = "Images")
    {
        var filePath = Path.Combine(this.webHostEnvironment.WebRootPath, folder, fileName);

        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);

            return true;
        }

        return false;
    }
}
