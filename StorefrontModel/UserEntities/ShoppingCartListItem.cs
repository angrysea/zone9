using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorefrontModel
{
    public class ShoppingCartListItem
    {
        public string Cookie { get; set; }
        public string CartItem { get; set; }
        public string Product { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURLSmall { get; set; }
        public string Availability { get; set; }
        public int Quantity { get; set; }
        public double OurPrice { get; set; }
        public int GiftOption { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
