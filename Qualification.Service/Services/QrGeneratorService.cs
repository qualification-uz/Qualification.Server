using System.Drawing;
using System.Drawing.Imaging;
using MessagingToolkit.QRCode.Codec;
using Microsoft.AspNetCore.Hosting;

namespace Qualification.Service.Services;

public class QRCodeGeneratorService
{
    private QRCodeEncoder encoder = new QRCodeEncoder();
    private readonly IWebHostEnvironment _environment;
    Bitmap bitmap;

    public QRCodeGeneratorService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public string GenerateQr(string data)
    {
        encoder.QRCodeScale = 8;
        bitmap = encoder.Encode(data);
        
        string outputFileName = Guid.NewGuid().ToString("N") + ".png";
        string outputFilePath = Path.Combine(_environment.WebRootPath, @$"Certificates\{outputFileName}");

        bitmap.Save(outputFilePath, ImageFormat.Png);
        return outputFilePath;
    }
}