using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorefrontModel;

namespace Storefront.Models
{
    public class SearchViewData : SFViewData
    {
        public List<SearchListItem> items { get; set; }
        public List<SortFields> sortFields { get; set; }
        public List<SearchBreadCrumb> searchBreadCrumbs { get; set; }
        public string sortBy { get; set; }
        public int page { get; set; }
        public string keywordsearch { get; set; }
    }
}