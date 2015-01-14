using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using SFAdmin.Models;
using SFAdmin.Aspects;
using StorefrontModel;
using System.Collections.Specialized;

namespace SFAdmin.Controllers
{
    [HandleError]
    public class InventoryController : StorefrontController
    {
        [LogMethodCall]
        public ActionResult PurchaseOrders(string fromdate, string todate, string declinedpurchaseOrdersonly)
        {
            PurchaseOrdersViewData viewData = new PurchaseOrdersViewData();

            AddMasterData(viewData);
            string[] parameters = { "FromDate", "ToDate" };
            string[] values = { fromdate, todate };
            viewData.purchaseOrders = context.PurchaseOrder.Execute("GetPurchaseOrdersByDate", parameters, values);
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult CreatePurchaseOrder(string distributorname, string shippingmethod, string dropship)
        {
            Distributor distributor = context.Distributor.Where(distributorname);
            ShippingMethod shippingMethod = context.ShippingMethod.Where(shippingmethod);

            PurchaseOrder purchaseOrder = new PurchaseOrder();
            purchaseOrder.Distributor = distributor.Name;
            purchaseOrder.Description = distributor.Description;
            purchaseOrder.ShippingMethod = shippingMethod.Code;
            purchaseOrder.Status = "Ordered";
            purchaseOrder.Dropship = (short)(dropship=="on"?1:0);

            if (purchaseOrder.Dropship==0)
            {
                purchaseOrder.Handling = distributor.Dropshipfee;
            }
            else
            {
                purchaseOrder.Handling = distributor.Handlingfee;
            }

            double totalcost = 0;
            double totalweight = 0;
            List<PurchaseOrderItem> purchaseOrderItems = new List<PurchaseOrderItem>();
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.StartsWith("product"))
                {
                    if (Request.Form[key] == "on")
                    {
                        string productno = key.Substring(10);
                        int qty = Int32.Parse(Request.Form["qtytoorder" + productno]);
                        if (qty > 0)
                        {
                            PurchaseOrderItem purchaseOrderItem = new PurchaseOrderItem();
                            Product product = context.Product.Where(productno);
                            Details detail = context.Details.Where(productno);
                            product.QuantityToOrder = qty;
                            purchaseOrderItem.Product = product.ProductNo;
                            purchaseOrderItem.Trxtype = "Order";
                            purchaseOrderItem.ProductName = product.Name;
                            purchaseOrderItem.Manufacturer = product.Manufacturer;
                            purchaseOrderItem.Quantity = qty;
                            purchaseOrderItem.Ourcost = product.OurCost;
                            totalcost += purchaseOrderItem.Ourcost * qty;
                            purchaseOrderItem.Shippingweight = detail.ShippingWeight;
                            totalweight += purchaseOrderItem.Shippingweight * product.QuantityToOrder;
                            purchaseOrderItem.Status = "Ordered";
                            purchaseOrderItems.Add(purchaseOrderItem);
                        }
                    }
                }
            }

            purchaseOrder.TotalCost = totalcost;
            purchaseOrder.Shipping = 0;
            purchaseOrder.ShippingWeight = totalweight;
            purchaseOrder.Total = totalcost + purchaseOrder.Handling;

            context.PurchaseOrder.Insert(purchaseOrder);
            foreach (PurchaseOrderItem purchaseOrderItem in purchaseOrderItems)
            {
                context.PurchaseOrderItem.Insert(purchaseOrderItem);
            }
            return RedirectToAction("Inventory", "PurchaseOrder");
        }

        [LogMethodCall]
        public ActionResult ReceivePurchaseOrder(string id)
        {
            PurchaseOrder purchaseOrder = context.PurchaseOrder.Where(id);
            List<PurchaseOrderItem> purchaseOrderItems = context.PurchaseOrderItem.Select(id);
            bool partial = false;
            foreach (PurchaseOrderItem purchaseOrderItem in purchaseOrderItems)
            {
                int qty = Int32.Parse(Request.Form["qtyreceiving" + purchaseOrderItem.Product]);
                purchaseOrderItem.Receiving = qty;
                purchaseOrderItem.Quantityreceived =
                    purchaseOrderItem.Quantityreceived + purchaseOrderItem.Receiving;
                if (purchaseOrderItem.Quantityreceived < purchaseOrderItem.Quantity)
                {
                    purchaseOrderItem.Status = "Partially Received";
                    partial = true;
                }
                else
                {
                    purchaseOrderItem.Status = "Received";
                }
            }

            if (partial)
            {
                purchaseOrder.Status = "Partially Received";
            }
            else
            {
                purchaseOrder.Status = "Received";
            }

            context.PurchaseOrder.Update(purchaseOrder);
            foreach (PurchaseOrderItem purchaseOrderItem in purchaseOrderItems)
            {
                context.PurchaseOrderItem.Update(purchaseOrderItem);
            }
            return RedirectToAction("Inventory", "PurchaseOrder");
        }

        [LogMethodCall]
        public ActionResult PurchaseOrderDelete(string id)
        {
            context.PurchaseOrder.Delete(id);
            context.PurchaseOrderItem.Delete(id);
            return RedirectToAction("Inventory", "PurchaseOrders");
        }
    }
}
