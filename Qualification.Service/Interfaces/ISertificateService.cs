using Qualification.Service.DTOs.Sertificate;

namespace Qualification.Service.Interfaces;

public interface ISertificateService
{
    ValueTask<byte[]> GenerateSertificateAsync(SertificateForCreationDto sertificateForCreationDto);
    ValueTask<FileStream> GetSertificateAsync(string id);
}