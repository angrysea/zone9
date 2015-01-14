using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorefrontModel;

namespace Storefront.Models
{
    public class DetailsViewData : SFViewData
    {
        public Product product { get; set; }
        public Details details { get; set; }
        public Availability availability { get; set; }
        public Manufacturer manufacturer { get; set; }
        public List<ProductSpecification> productSpecifications { get; set; }
        public List<ListProduct> similarProducts { get; set; }
        public List<ListProduct> recentlyViewed { get; set; }
    }
}