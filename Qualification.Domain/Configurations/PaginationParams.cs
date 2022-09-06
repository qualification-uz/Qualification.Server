namespace Qualification.Domain.Configurations;

public class PaginationParams
{
    private const int _maxSize = 20;
    private int _pageSize;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > _maxSize ? _maxSize : value;
    }

    public int PageNumber { get; set; }
}