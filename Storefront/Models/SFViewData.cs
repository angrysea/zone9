using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorefrontModel;

namespace Storefront.Models
{
    public class ProductGroup
    {
        public ProductGroup()
        {
            catagories = new List<Category>();
        }

        public GroupType grouptype { get; set; }
        public List<Category> catagories { get; set; }
    }

    public class SFViewData
    {
        public Company company { get; set; }
        public SiteTheme theme { get; set; }
        public List<Manufacturer> manufacturers { get; set; }
        public List<ProductGroup> groups { get; set; }
        public Users user { get; set; }
        public Customer customer { get; set; }
        public string login { get; set; }
        public string gotourl { get; set; }
        public string cookie { get; set; }
        public List<string> errors { get; set; }
        public int productsInCart { get; set; }
        public bool update { get; set; }
        public string searchId { get; set; }
        public bool bInSearch { get; set; }
        public bool bBrandMenu { get; set; }
        public bool bBreadcrumbs { get; set; }
    }
}
