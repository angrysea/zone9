using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SFAdmin.Models;
using StorefrontModel;

namespace SFAdmin.Models
{
    public class PurchaseOrdersViewData : SFViewData
    {
        public List<PurchaseOrder> purchaseOrders { get; set; }
    }

    public class PurchaseOrderViewData : SFViewData
    {
        public PurchaseOrder purchaseOrder { get; set; }
        public List<PurchaseOrderItem> purchaseOrderItems { get; set; }
    }

    public class PurchaseOrderMaintCreateData : SFViewData
    {
        public List<ShippingMethod> shippingMethods { get; set; }
        public List<Distributor> distributors { get; set; }
        public List<Product> products { get; set; }
    }
}
