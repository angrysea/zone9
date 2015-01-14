using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorefrontModel
{
    public class SearchListItem
    {
        public string ProductNo { get; set; }
        public string GTIN { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Distributor { get; set; }
        public string ImageURLSmall { get; set; }
		public int Quantity { get; set; }
		public string Availability { get; set; }
        public double ListPrice { get; set; }
        public double OurPrice { get; set; }
        public double OurCost { get; set; }
    }
}
