namespace Qualification.Service.DTOs.Application;

public class ApplicationForUpdateDto
{
    public int? SubjectId { get; set; }
    public long? DocumentId { get; set; }
    public long? TeacherId { get; set; }
    public long? AssetId { get; set; }
    public ICollection<GroupForCreationDto> Groups { get; set; }
}
