using Microsoft.AspNetCore.Hosting;
using Qualification.Domain.Entities.Users;
using Qualification.Service.Interfaces;
using System.Text;

namespace Qualification.Service.Services;

public class ExcelService : IExcelService
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public ExcelService(IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
    }

    public async Task<byte[]> ExportStudentsAsync(List<Student> data)
    {
        string serverPath = Path.Combine(webHostEnvironment.WebRootPath, "exports");

        if (!Directory.Exists(serverPath))
            Directory.CreateDirectory(serverPath);

        string fileName = DateTime.Now.ToString("yymmssfff") + ".csv";
        
        var filePath = Path.Combine(serverPath, fileName);

        StringBuilder csvContent = new StringBuilder();

        // Add the headers to the .csv file
        csvContent.AppendLine("Login,Password");

        // Loop through the login and password list and add each row to the .csv file
        for (int i = 0; i < data.Count; i++)
        {
            csvContent.AppendLine(data[i].Id + "," + data[i].Id);
        }

        // Write the .csv content to the file
        await File.WriteAllTextAsync(filePath, csvContent.ToString());

        if (File.Exists(filePath))
            return await File.ReadAllBytesAsync(filePath);

        return null;
    }
}