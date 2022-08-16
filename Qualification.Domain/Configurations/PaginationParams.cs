namespace Qualification.Domain.Configurations;

public class PaginationParams
{
    private const int MaxSize = 20;
    private int _pageSize;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxSize ? MaxSize : value;
    }

    public int PageIndex { get; set; }
}