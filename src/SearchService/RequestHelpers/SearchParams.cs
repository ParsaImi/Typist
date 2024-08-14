namespace SearchService;

public class SearchParams
{
    public string searchTerm { get; set;}

    public int PageNumber { get; set;} = 1;

    public int PageSize { get; set;} = 4;

    public int Wins  { get; set;}

    public int Loses { get; set;}

    public string OrderBy { get; set;}

    public string FilterBy  { get; set;}
}
