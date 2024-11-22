using Microsoft.EntityFrameworkCore;

namespace OT.Assesment.Shared.Helpers;

public class PagedListDto<T>
{
    private readonly int _totalPages;

    public PagedListDto(IEnumerable<T> data,int page,int pageSize, int totalPages,int total)
    {
        Data = data;
        Page = page;
        PageSize = pageSize;
        Total = total;
        TotalPages = totalPages;
    }
    public IEnumerable<T> Data { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
}

public class PagedList<T> : List<T>
{
    
    public PagedList(IEnumerable<T> currentPage, int count, int pageNumber, int pageSize)
    {
        Pape = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        Total = count;
        AddRange(currentPage);
    }

    public int Pape { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }

    public static async Task<PagedListDto<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();
        int truePageCount = count / pageSize;
        truePageCount += (count % pageSize == 0 ? 0 : 1);
        return new PagedListDto<T>(items, pageNumber, pageSize,truePageCount,count );
    }
}