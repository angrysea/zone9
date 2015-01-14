using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorefrontModel
{
    public class ListProduct
    {
        public string ProductNo { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string ImageURLSmall { get; set; }
        public double ListPrice { get; set; }
        public double OurPrice { get; set; }
        public string Description { get; set; }
    }
}
