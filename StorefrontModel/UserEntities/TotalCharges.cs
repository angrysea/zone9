using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StorefrontModel
{
    public class TotalCharges
    {
        public double ShippingCost { get; set; }
        public double TotalCost { get; set; }
        public double Taxes { get; set; }
        public double Discount { get; set; }
    }
}