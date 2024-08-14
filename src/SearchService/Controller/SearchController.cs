using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace SearchService;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery]SearchParams searchParams)
    {
        var query = DB.PagedSearch<Item , Item>();


        if (!string.IsNullOrEmpty(searchParams.searchTerm))
        {
            query.Match(Search.Full , searchParams.searchTerm).SortByTextScore();
        }

        query = searchParams.OrderBy switch
        {
            "wins" => query.Sort(x => x.Ascending(a => a.Wins)),
            "loses" => query.Sort(x => x.Ascending(a => a.Loses)),
            _ => query.Sort(x => x.Ascending(a => a.UserName))
        };


        if (!string.IsNullOrEmpty(searchParams.FilterBy))
        {

            query = searchParams.FilterBy switch
            {
                "wins" => query.Match(x => x.Wins > searchParams.Wins),
                "loses" => query.Match(x => x.Loses > searchParams.Loses),
                _ => query.Match(x => x.Wins > 1)
            };

        }

        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);

        var result = await query.ExecuteAsync();

        return Ok(result.Results);
    }
}
