using Qualification.Service.DTOs.Sertificate;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class SertificateService : ISertificateService
{
    public ValueTask<SertificateDto> GenerateSertificateAsync(SertificateForCreationDto sertificateForCreationDto)
    {
        throw new NotImplementedException();
    }
}
