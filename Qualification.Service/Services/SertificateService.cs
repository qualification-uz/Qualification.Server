using Microsoft.AspNetCore.Hosting;
using Qualification.Service.DTOs.Sertificate;
using Qualification.Service.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using MessagingToolkit.QRCode.Codec;

namespace Qualification.Service.Services;

#pragma warning disable
public class SertificateService : ISertificateService
{
    private readonly IWebHostEnvironment _env;
    private readonly QRCodeEncoder encoder;

    public SertificateService(IWebHostEnvironment env)
    {
        _env = env;
        encoder = new QRCodeEncoder();
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
        Font defaultFont = new Font("Arial", 50, FontStyle.Regular);
        Font fullNameFont = new Font("Arial", 50, FontStyle.Bold);

        // Set text size
        SizeF sizeOfCertNumber = graphics.MeasureString(sertificateForCreationDto.SertificateNumber, defaultFont);
        SizeF sizeOfFullName = graphics.MeasureString(sertificateForCreationDto.FullName, fullNameFont);
        SizeF sizeOfSubject = graphics.MeasureString(sertificateForCreationDto.Subject, defaultFont);
        SizeF sizeOfSubjectScore = graphics.MeasureString(sertificateForCreationDto.SubjectScore.ToString(), defaultFont);
        SizeF sizeOfPedagogicalScore = graphics.MeasureString(sertificateForCreationDto.PedagogicalScore.ToString(), defaultFont);
        SizeF sizeOfTotalScore = graphics.MeasureString(sertificateForCreationDto.TotalScore.ToString(), defaultFont);

        // Draw text to image
        graphics.DrawString(sertificateForCreationDto.SertificateNumber, defaultFont, brush, new PointF((bitmap.Width - sizeOfCertNumber.Width) / 2, 1715));
        graphics.DrawString(sertificateForCreationDto.FullName, fullNameFont, brush, new PointF((bitmap.Width - sizeOfFullName.Width) / 2, 1900));
        graphics.DrawString(sertificateForCreationDto.Subject, defaultFont, brush, new PointF((bitmap.Width - sizeOfSubject.Width) / 5, 2245));
        graphics.DrawString(sertificateForCreationDto.SubjectScore.ToString(), defaultFont, brush, new PointF((bitmap.Width - sizeOfSubjectScore.Width) / 5 * 4.2f, 2140));
        graphics.DrawString(sertificateForCreationDto.PedagogicalScore.ToString(), defaultFont, brush, new PointF((bitmap.Width - sizeOfPedagogicalScore.Width) / 5 * 4.2f, 2490));
        graphics.DrawString(sertificateForCreationDto.TotalScore.ToString(), defaultFont, brush, new PointF((bitmap.Width - sizeOfTotalScore.Width) / 4.2f, 2570));
        
        // Save output image
        string outputFileName = Guid.NewGuid().ToString("N") + ".png";
        string outputFilePath = Path.Combine(_env.WebRootPath, @$"Certificates\{outputFileName}");

        // generate QrCode
        encoder.QRCodeScale = 9;
        var qrBitmap = encoder.Encode(Path.Combine($"qualification.visualstudio.uz/Certificates/{outputFileName}"));
        graphics.DrawImage(qrBitmap, bitmap.Width - 375, 75);
        
        bitmap.Save(outputFilePath, ImageFormat.Png);

        if (File.Exists(outputFilePath))
            return await File.ReadAllBytesAsync(outputFilePath);

        return null;
    }
}
