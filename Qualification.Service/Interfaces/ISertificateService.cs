using Qualification.Service.DTOs.Sertificate;

namespace Qualification.Service.Interfaces;

public interface ISertificateService
{
    ValueTask<SertificateDto> GenerateSertificateAsync(SertificateForCreationDto sertificateForCreationDto);
}