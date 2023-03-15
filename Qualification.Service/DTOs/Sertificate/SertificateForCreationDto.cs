namespace Qualification.Service.DTOs.Sertificate;

public class SertificateForCreationDto
{
    public string SertificateNumber { get; set; }
    public string FullName { get; set; }
    public string Subject { get; set; }
    public double SubjectScore { get; set; }
    public double PedagogicalScore { get; set; }
    public double TotalScore { get; set; }
}