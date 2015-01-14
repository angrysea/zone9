using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using SFAdmin.Models;
using SFAdmin.Aspects;
using StorefrontModel;

namespace SFAdmin.Controllers
{
    [HandleError]
    public class LogisticsController : StorefrontController
    {
        [LogMethodCall]
        public ActionResult CreatePackingslips()
        {
            if (Request.HttpMethod == "POST")
            {
                List<string> selected = new List<string>();
                foreach (string key in Request.Form.AllKeys)
                {
                    if (key.StartsWith("item"))
                    {
                        string value = Request.Form[key];
                        if (value == "on")
                        {
                            string name = key.Substring(4);
                            selected.Add(name);
                        }
                    }
                }

                Dictionary<string, int> soItems = new Dictionary<string, int>();
                foreach (string key in selected)
                {
                    string value = Request.Form["qtytoship" + key];
                    if(!string.IsNullOrEmpty(value))
                    {
                        int qtyToShip = Int32.Parse(value);
                        if (qtyToShip > 0)
                        {
                            soItems[key] = qtyToShip;
                        }
                    }
                }

                List<SalesOrderItem> salesorderitems = new List<SalesOrderItem>();
                foreach (string key in soItems.Keys)
                {
                    SalesOrderItem item = context.SalesOrderItem.Where(key);
                    item.QuantityToShip = soItems[key];
                    salesorderitems.Add(item);
                }

                if (salesorderitems.Count() > 0)
                {
                    new ShippingProcessor(context).CreatePackingslips(salesorderitems);
                }
            }

            CreatePackingslipsViewData viewData = new CreatePackingslipsViewData();
            AddMasterData(viewData);
            viewData.salesOrders = new List<DetailedSalesOrder>();
            List<SalesOrder> salesOrders = null;

            salesOrders = context.SalesOrder.Execute("GetNewSalesOrders");

            foreach (SalesOrder salesOrder in salesOrders)
            {
                DetailedSalesOrder detailedSalesOrder = new DetailedSalesOrder();
                detailedSalesOrder.SalesOrder = salesOrder;
                detailedSalesOrder.Items = context.SalesOrderItem.Select("SalesOrder", salesOrder.SalesOrderID);
                viewData.salesOrders.Add(detailedSalesOrder);
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult ShipPackages()
        {
            if (Request.HttpMethod == "POST")
            {
                List<string> selected = new List<string>();
                foreach (string key in Request.Form.AllKeys)
                {
                    if (key.StartsWith("packingslip"))
                    {
                        string value = Request.Form[key];
                        if (value == "on")
                        {
                            string name = key.Substring(11);
                            selected.Add(name);
                        }
                    }
                }

                Dictionary<string, string> packages = new Dictionary<string, string>();
                foreach (string key in selected)
                {
                    string value = Request.Form["trackingnumber" + key];
                    if (!string.IsNullOrEmpty(value))
                    {
                        packages[key] = value;
                    }
                }

                ShippingProcessor shippingProcessor = new ShippingProcessor(context);
                foreach (string key in packages.Keys)
                {
                    Packingslip package = context.Packingslip.Where(key);
                    shippingProcessor.ShipPackages(package, packages[key]);
                }
            }

            ShipPackagesViewData viewData = new ShipPackagesViewData();
            AddMasterData(viewData);
            viewData.ShipPackagesData = new List<ShipPackagesData>();

            List<Packingslip> packingslips = context.Packingslip.Execute("GetOpenPackingSlips");

            foreach (Packingslip packingslip in packingslips)
            {
                ShipPackagesData shipPackagesData = new ShipPackagesData();
                shipPackagesData.Packingslip = new DetailedPackingslip();
                shipPackagesData.Packingslip.Packingslip = packingslip;
                shipPackagesData.Packingslip.Items = context.PackingslipItem.Select("Packingslip", packingslip.PackingSlipId);
                shipPackagesData.Customer = context.Customer.Where(shipPackagesData.Packingslip.Packingslip.Customer);
                shipPackagesData.Shipping = context.Address.Where(shipPackagesData.Packingslip.Packingslip.ShippingAddress);
                shipPackagesData.ShippingMethod = context.ShippingMethod.Where(shipPackagesData.Packingslip.Packingslip.Shippingmethod);

                viewData.ShipPackagesData.Add(shipPackagesData);
            }

            return View(viewData);
        }
    }
}
