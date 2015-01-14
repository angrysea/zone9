using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorefrontModel;

namespace Storefront.Models
{
    public class AddLinkViewData : SFViewData
    {
        public List<LinkCategory> categories { get; set; }
        public Link Link { get; set; }
    }

    public class LinkCategoriesViewData : SFViewData
    {
        public List<LinkCategory> categories { get; set; }
    }

    public class LinksViewData : SFViewData
    {
        public List<Link> links { get; set; }
        public string category { get; set; }
    }
}