using System.Collections.Generic;

namespace LocationSolution.Mvc.Web.Models.Search
{
    public class SearchIndexModel
    {
        public IList<SearchResult> Results { get; set; }

        public SearchIndexModel()
        {
            Results = new List<SearchResult>();
        }
    }
}