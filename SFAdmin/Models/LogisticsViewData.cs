using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorefrontModel;

namespace SFAdmin.Models
{
    public class CreatePackingslipsViewData : SFViewData
    {
        public List<DetailedSalesOrder> salesOrders { get; set; }
    }

    public class ShipPackagesData
    {
        public Customer Customer { get; set; }
        public Address Shipping { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public DetailedPackingslip Packingslip { get; set; }
    }

    public class ShipPackagesViewData : SFViewData
    {
        public List<ShipPackagesData> ShipPackagesData { get; set; }
    }

}
