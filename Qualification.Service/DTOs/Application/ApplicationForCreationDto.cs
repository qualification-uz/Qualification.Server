namespace Qualification.Service.DTOs.Application;
public class ApplicationForCreationDto
{
    public int SubjectId { get; set; }
    public long SchoolId { get; set; }
    public long DocumentId { get; set; }
    public long TeacherId { get; set; }
    
    public ICollection<GroupForCreationDto> Groups { get; set; }
}
