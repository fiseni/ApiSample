namespace ApiSample.Domain;

public class PaginationSettings
{
    public int DefaultPage { get; } = 1;
    public int DefaultPageSize { get; }
    public int DefaultPageSizeLimit { get; }

    public PaginationSettings(int defaultPageSize, int defaultPageSizeLimit)
    {
        DefaultPageSize = defaultPageSize;
        DefaultPageSizeLimit = defaultPageSizeLimit;
    }
}
