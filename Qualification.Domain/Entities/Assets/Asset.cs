using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Assets;

public class Asset : Auditable
{
    public string Url { get; set; }
}