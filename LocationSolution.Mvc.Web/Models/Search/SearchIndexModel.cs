using System.Collections.Generic;
using PagedList;

namespace LocationSolution.Mvc.Web.Models.Search
{
    public class SearchIndexModel
    {
        public IList<SearchResult> Results { get; set; }
        public IPagedList<SearchResult> PagedResults { get; set; }

        public SearchIndexModel()
        {
            Results = new List<SearchResult>();
        }
    }
}