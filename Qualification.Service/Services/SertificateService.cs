using Microsoft.AspNetCore.Hosting;
using Qualification.Service.DTOs.Sertificate;
using Qualification.Service.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.ConstrainedExecution;

namespace Qualification.Service.Services;

#pragma warning disable
public class SertificateService : ISertificateService
{
    private readonly IWebHostEnvironment _env;

    public SertificateService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async ValueTask<byte[]> GenerateSertificateAsync(SertificateForCreationDto sertificateForCreationDto)
    {
        string filePath = Path.Combine(_env.WebRootPath, "Templates/certificate.jpg");
        
        Bitmap bitmap = new Bitmap(filePath); // inputFile must be absolute path
        
        // Initialize Graphics class object
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        Brush brush = new SolidBrush(Color.FromKnownColor(KnownColor.Black));

        // Set text font
        Font arial = new Font("Arial", 70, FontStyle.Bold);
        Font arial2 = new Font("Arial", 35, FontStyle.Italic);
        
        // Set text size
        SizeF sizeOfCertNumber = graphics.MeasureString(sertificateForCreationDto.SertificateNumber, arial);
        SizeF sizeOfFullName = graphics.MeasureString(sertificateForCreationDto.FullName, arial);
        SizeF sizeOfSubject = graphics.MeasureString(sertificateForCreationDto.Subject, arial);
        SizeF sizeOfSubjectScore = graphics.MeasureString(sertificateForCreationDto.SubjectScore.ToString(), arial);
        SizeF sizeOfPedagogicalScore = graphics.MeasureString(sertificateForCreationDto.PedagogicalScore.ToString(), arial);
        SizeF sizeOfTotalScore = graphics.MeasureString(sertificateForCreationDto.TotalScore.ToString(), arial);

        // Draw text to image
        graphics.DrawString(sertificateForCreationDto.SertificateNumber, arial, brush, new PointF((bitmap.Width - sizeOfCertNumber.Width) / 2, 730));
        graphics.DrawString(sertificateForCreationDto.FullName, arial, brush, new PointF((bitmap.Width - sizeOfFullName.Width) / 2, 870));
        graphics.DrawString(sertificateForCreationDto.Subject, arial, brush, new PointF((bitmap.Width - sizeOfSubject.Width) / 2, 1000));
        graphics.DrawString(sertificateForCreationDto.SubjectScore.ToString(), arial, brush, new PointF((bitmap.Width - sizeOfSubjectScore.Width) / 2, 1130));
        graphics.DrawString(sertificateForCreationDto.PedagogicalScore.ToString(), arial, brush, new PointF((bitmap.Width - sizeOfPedagogicalScore.Width) / 2, 1260));
        graphics.DrawString(sertificateForCreationDto.TotalScore.ToString(), arial, brush, new PointF((bitmap.Width - sizeOfTotalScore.Width) / 2, 1390));

        // Save output image
        string outputFileName = Guid.NewGuid().ToString("N") + ".png";
        string outputFilePath = Path.Combine(_env.WebRootPath, @$"Certificates\{outputFileName}");
        
        bitmap.Save(outputFilePath, ImageFormat.Png);

        if(File.Exists(outputFilePath))
            return await File.ReadAllBytesAsync(outputFilePath);

        return null;
    }
}
