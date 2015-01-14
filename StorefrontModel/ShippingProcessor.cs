using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPSXml;
using log4net;

namespace StorefrontModel
{
    public class ShippingPackage 
    {
        public string zip { get; set; }
        public string country { get; set; }
        public double weight { get; set; }
        public double value { get; set; }
    }

    public class ShippingProcessor
    {
        private Entities context;
        static private Company company = null;
        static private String strpackage = "02";

        public ShippingProcessor(Entities context)
        {
            this.context = context;
            if (company == null)
            {
                company = context.Company.First("Active", "1");
            }
        }

        public int CreatePackingslips(List<SalesOrderItem> salesorderitems)
        {
            int total = 0;
            var log = LogManager.GetLogger("ShippingProcessor");

            try
            {
                foreach (SalesOrderItem salesorderitem in salesorderitems)
                {
                    salesorderitem.Status = "Ready to Ship";
                    context.SalesOrderItem.Update(salesorderitem);
                }
            }
            catch (Exception e)
            {
                log.Error("Error Updating item status", e);
                throw e;
            }

            try
            {
                List<SalesOrder> salesOrders = context.SalesOrder.Execute("GetReadyToShip");
                foreach (SalesOrder salesorder in salesOrders)
                {
                    bool bReady = true;
                    DetailedSalesOrder detailed = new DetailedSalesOrder();
                    detailed.SalesOrder = salesorder;
                    detailed.Items = 
                            context.SalesOrderItem.Select("SalesOrder", salesorder.SalesOrderID);

                    if (salesorder.OptimizeShipping == 0)
                    {
                        foreach (SalesOrderItem soitem in detailed.Items)
                        {
                            if (!soitem.Status.Equals("Ready to Ship") ||
                                    soitem.Quantity != soitem.QuantityToShip)
                            {
                                bReady = false;
                                break;
                            }
                        }
                    }

                    if (bReady) 
                    {
                        ProcessSalesOrder(detailed);
                        UpdateStatus(detailed);
                    }
                    else 
                    {
                        if(salesorder.OptimizeShipping>0 || salesorder.Status == "New")
                        {
                            GetShippingCost(detailed, true);
                        }
                        else 
                        {
                            TotalCharges totalCharges = 
                                context.TotalCharges.First("GetTotalCharges", "salesorder", salesorder);
                            if(totalCharges.ShippingCost>0) 
                            {
                                double shippingleft = salesorder.Shipping-totalCharges.ShippingCost;
                                if(shippingleft>0) 
                                {
                                    if (salesorder.TotalCost - (totalCharges.TotalCost+salesorder.TotalCost)<.01) 
                                    {
                                      salesorder.Shipping = shippingleft;
                                      salesorder.Taxes = salesorder.Taxes-totalCharges.Taxes;
                                      salesorder.Discount = salesorder.Discount-totalCharges.Discount;
                                    }
                                    else 
                                    {
                                        salesorder.Shipping = shippingleft *
                                                                ((salesorder.TotalCost -
                                                                     salesorder.TotalCost) /
                                                                   salesorder.TotalCost);
                                    }
                                }
                                salesorder.Total = salesorder.Total + salesorder.Shipping;
                            }
                        }
                        context.SalesOrder.Update(salesorder);
                    }
                    total++;
                }
            }
            catch (Exception e)
            {
                log.Error("Error processing GetReadyToShip", e);
                throw e;
            }

            return total;
        }

        private void UpdateStatus(DetailedSalesOrder salesorder)
        {
            bool hasShipped = false;
            bool hasNew = false;
            bool hasProcessed = false;
            bool hasFailed = false;

            foreach(SalesOrderItem item in salesorder.Items)
            {
                if (!string.IsNullOrEmpty(item.Status) )
                {
                    if(item.Status == "New") 
                    {
                        hasNew = true;
                    }
                    else if (item.Status == "Shipped") 
                    {
                        hasShipped = true;
                    }
                    else if (item.Status == "Proccessed") 
                    {
                        hasProcessed = true;
                        if (item.Quantity > item.QuantityToShip + item.Shipped)
                            hasNew = true;
                    }
                    else if (item.Status=="Partially Proccessed") 
                    {
                        hasProcessed = true;
                        hasNew = true;
                    }
                    else if (item.Status == "Failed")
                    {
                        hasFailed = true;
                    }
                    else if (item.Status == "Partially Shipped")
                    {
                        hasNew = true;
                        hasShipped = true;
                    }
                }

                StringBuilder  status = new StringBuilder ();
                if (hasFailed)
                    status.Append("Declined Credit Card ");

                if (hasProcessed) 
                {
                    if (!hasShipped && !hasNew && !hasFailed)
                        status.Append("Proccessed ");
                    else
                        status.Append("Partially Proccessed ");
                }
    
                if (hasShipped) 
                {
                    if (!hasProcessed && !hasNew && !hasFailed)
                        status.Append("Shipped ");
                    else
                        status.Append("Partially Shipped ");
                }

                if (hasShipped || hasProcessed || hasShipped || hasNew || hasFailed) 
                {
                    salesorder.SalesOrder.Status = status.ToString();
                    context.SalesOrder.Update(salesorder.SalesOrder);
                }
            }
        }

        void ShipPackingslipItems(DetailedPackingslip detailed) 
        {
            SalesOrdersProcessor salesOrdersProcessor = new SalesOrdersProcessor(context);
            foreach (PackingslipItem packingslipItem in detailed.Items)
            {
                salesOrdersProcessor.Shipped(packingslipItem.SOItem,
                                             packingslipItem.Quantity);
                ShipProduct(packingslipItem.ProductNo,
                            packingslipItem.Quantity);
            }
        }

        void ShipProduct(string productNo, int qty)
        {
            Product product = context.Product.Where(productNo);

		    product.Quantity = product.Quantity - qty;
		    product.Allocated = product.Allocated - qty;
		    product.Sold = product.Sold + qty;
            if(product.Quantity<1) 
            {
                product.Status = 2;
            }
            context.Product.Update(product);
        }

        public void ShipPackages(Packingslip packingslip, string trackingnumber) 
        {
            DetailedPackingslip detailedPS = new DetailedPackingslip();
            detailedPS.Packingslip = packingslip;
            packingslip.TrackingNumber = trackingnumber;
            context.Packingslip.Update(packingslip);
            detailedPS.Items = context.PackingslipItem.Select(packingslip.PackingSlipId);
            ShipPackingslipItems(detailedPS);

            SendOrderShippedMail(packingslip);

            foreach (PackingslipItem item in detailedPS.Items) 
            {
                DetailedSalesOrder detailed = new DetailedSalesOrder();
                detailed.SalesOrder = context.SalesOrder.Where(item.SalesOrder);
                detailed.Items =
                    context.SalesOrderItem.Select("SalesOrder", item.SalesOrder);
                UpdateStatus(detailed);
            }
        }

        public void GetShippingCost(DetailedSalesOrder salesorder, bool bToShip)
        {
            double totalcharges = 0;
            double handlingfee = 0;
            double totalweight = 0;

            ShippingMethod shippingMethod = context.ShippingMethod.Where(salesorder.SalesOrder.ShippingMethod);
            Carrier carrier = context.Carrier.Where(shippingMethod.Carrier);
            Address shippingaddress = context.Address.Where(salesorder.SalesOrder.ShippingAddress);

            double fixedprice = shippingMethod.FixedPrice;

            Dictionary<string, ShippingPackage> packages = new Dictionary<string, ShippingPackage>();
            foreach (SalesOrderItem item in salesorder.Items)
            {
                int qty = 0;
                if (!bToShip)
                {
                    qty = item.Quantity;
                }
                else
                {
                    if (item.QuantityToShip == 0)
                    {
                        continue;
                    }
                    qty = item.QuantityToShip;
                }


                totalweight += item.Shippingweight * qty;
                string key = item.Zipcode + item.Country;
                
                ShippingPackage shippingPackage = null;
                if (packages.Count > 0 && packages.ContainsKey(key))
                {
                    shippingPackage = packages[key];
                }

                if (handlingfee < item.Handlingcharges)
                {
                    handlingfee = item.Handlingcharges;
                }

                if (shippingPackage == null)
                {
                    shippingPackage = new ShippingPackage();
                    shippingPackage.zip = item.Zipcode;
                    shippingPackage.country = item.Country;
                    shippingPackage.weight = item.Shippingweight;
                    shippingPackage.value = qty * item.Unitprice;
                    packages.Add(key, shippingPackage);
                }
                else
                {
                    shippingPackage.weight += (item.Shippingweight * qty);
                    shippingPackage.value = qty * item.Unitprice;
                }
            }

            if (totalweight == 0)
                throw new Exception("Invalid Shipping information");

            salesorder.SalesOrder.ShippingWeight = totalweight;

            if (fixedprice > 0)
            {
                totalcharges = totalweight * fixedprice;
            }
            else if (carrier.Name=="UPS")
            {
                UpsRequester requester = new UpsRequester(context);
                RatingServiceSelectionResponse response =
                    requester.RateRequest(company,
                                            carrier,
                                            shippingMethod,
                                            packages,
                                            shippingaddress);

                if (response.Response.ResponseStatusCode == "0") 
                {
                    throw new Exception("Invalid Shipping information");
                }
                else 
                {
                    foreach(RatedShipmentType ratedShipment in response.RatedShipment)
                    {
                        totalcharges += Double.Parse(ratedShipment.TotalCharges.MonetaryValue);
                    }
                }
            }
            else if (carrier.Name=="USPS")
            {
            }

            
            salesorder.SalesOrder.Shipping = totalcharges;
            salesorder.SalesOrder.Handling = handlingfee;
            salesorder.SalesOrder.ShippingWeight = totalweight;

            double newdiscounts=0;
            if (shippingMethod.FreeShippingAmount > 0 &&
                shippingMethod.FreeShippingAmount <= salesorder.SalesOrder.TotalCost) 
            {
                newdiscounts = salesorder.SalesOrder.Handling + salesorder.SalesOrder.Shipping;
                salesorder.SalesOrder.Discount = newdiscounts;
                salesorder.SalesOrder.DiscountDescription = "Free Shipping";
            }
            salesorder.SalesOrder.Total = (salesorder.SalesOrder.Total + totalcharges + handlingfee)- newdiscounts;
        }

        private void ProcessSalesOrder(DetailedSalesOrder salesorder)
        {
            PayFlowProcessor payflow = new PayFlowProcessor(context);
            DetailedInvoice invoice = GenerateInvoice(salesorder);

            UpdateSalesOrderItems(salesorder.Items, "Proccessing");

            //CCTransaction origTrans = context.CCTransaction.Where(salesorder.SalesOrder.Pnref);
            //CCTransaction response = payflow.Capture(origTrans, "N");
            CCTransaction response = new CCTransaction();
            response.RespMSG = "0";

            if (response.RespMSG == "0")
            {
                invoice.Invoice.AuthorizationCode = response.Authcode;
                DetailedPackingslip packingslip = GeneratePackingslip(salesorder);
                invoice.Invoice.Status = "closed";
                context.Invoice.Insert(invoice.Invoice);
                foreach (InvoiceItem item in invoice.Items)
                {
                    context.InvoiceItem.Insert(item);
                }

                packingslip.Packingslip.Invoice = invoice.Invoice.InvoiceID;
                context.Packingslip.Insert(packingslip.Packingslip);
                foreach (PackingslipItem item in packingslip.Items)
                {
                    context.PackingslipItem.Insert(item);
                }
                UpdateSalesOrderItemsProcessed(salesorder.Items);
            }
            else
            {
                invoice.Invoice.Status = response.RespMSG;
                context.Invoice.Insert(invoice.Invoice);
                UpdateSalesOrderItems(salesorder.Items, "Failed");
                SendDeclindedCreditCardMail(salesorder.SalesOrder);
            }
        }

        void UpdateSalesOrderItems(List<SalesOrderItem> items, string status) 
        {
            foreach(SalesOrderItem item in items)
            {
                item.Status = status;
                context.SalesOrderItem.Update(item);
            }
        }

        void UpdateSalesOrderItemsProcessed(List<SalesOrderItem> items)
        {
            foreach (SalesOrderItem item in items)
            {
                if (item.Quantity == item.QuantityToShip + item.Shipped)
                {
                    item.Status = "Proccessed";
                }
                else
                {
                    item.Status = "Partially Proccessed";
                }
                context.SalesOrderItem.Update(item);
            }
        }

        private DetailedInvoice GenerateInvoice(DetailedSalesOrder detailed)
        {
            DetailedInvoice invoice = new DetailedInvoice();
            invoice.Invoice = new Invoice();
            invoice.Invoice.InvoiceID = context.getGuid();
            invoice.Invoice.Customer = detailed.SalesOrder.Customer;
            invoice.Invoice.Billing = detailed.SalesOrder.BillingAddress;
            invoice.Invoice.Totalcost = detailed.SalesOrder.TotalCost;
            invoice.Invoice.Salesorder = detailed.SalesOrder.SalesOrderID;
            invoice.Invoice.Discount = detailed.SalesOrder.Discount;
            invoice.Invoice.DiscountDescription = detailed.SalesOrder.DiscountDescription;
            invoice.Invoice.Taxes = detailed.SalesOrder.Taxes;
            invoice.Invoice.TaxesDescription = detailed.SalesOrder.TaxesDescription;
            invoice.Invoice.ShippingCost = detailed.SalesOrder.Shipping;
            invoice.Invoice.Handling = detailed.SalesOrder.Handling;
            invoice.Invoice.Total = detailed.SalesOrder.Total;
            invoice.Invoice.CreationDate = DateTime.Now;

            invoice.Items = new List<InvoiceItem>();
            foreach(SalesOrderItem salesorderitem in detailed.Items)
            {
                InvoiceItem item = new InvoiceItem();
                item.Invoice = invoice.Invoice.InvoiceID;
                item.SOItem = salesorderitem.SOItem;
                item.UnitPrice = salesorderitem.Unitprice;
                item.TotalPrice = salesorderitem.Unitprice *salesorderitem.QuantityToShip;
                item.Product = salesorderitem.ProductNo;
                item.Quantity = salesorderitem.QuantityToShip;
                item.GiftOption = salesorderitem.GiftOption;
                invoice.Items.Add(item);
            }
            return invoice;
        }

        private DetailedPackingslip GeneratePackingslip(DetailedSalesOrder salesorder)
        {
            DetailedPackingslip packingslip = new DetailedPackingslip();
            packingslip.Packingslip = new Packingslip();
            packingslip.Packingslip.PackingSlipId = context.getGuid();
            packingslip.Packingslip.Customer = salesorder.SalesOrder.Customer;
            packingslip.Packingslip.Shippingmethod = salesorder.SalesOrder.ShippingMethod;
            packingslip.Packingslip.ShippingAddress = salesorder.SalesOrder.ShippingAddress;
            packingslip.Packingslip.Salescoupon = salesorder.SalesOrder.Couponcode;
            packingslip.Packingslip.Creationdate = DateTime.Now;

            packingslip.Items = new List<PackingslipItem>();
            foreach (SalesOrderItem salesorderitem in salesorder.Items)
            {
                PackingslipItem packingslipItem = new PackingslipItem();
                packingslipItem.PackingSlip = packingslip.Packingslip.PackingSlipId;
                packingslipItem.SalesOrder = salesorder.SalesOrder.SalesOrderID;
                packingslipItem.SOItem = salesorderitem.SOItem;
                packingslipItem.Quantity = salesorderitem.QuantityToShip;
                packingslipItem.ProductNo = salesorderitem.ProductNo;
                packingslipItem.GTIN = salesorderitem.GTIN;
                packingslipItem.ProductName = salesorderitem.ProductName;
                packingslip.Items.Add(packingslipItem);
            }
            return packingslip;
        }

        private void SendOrderShippedMail(Packingslip packingslip)
        {
            Customer customer = context.Customer.Where(packingslip.Customer);
            new EmailProcessor(context).SendEmail(  "OrderShipped",
                                                    customer.Email1, 
                                                    " Packing Slip # " + 
                                                    packingslip.PackingSlipId,
                                                    packingslip.PackingSlipId);
        }

        private void SendDeclindedCreditCardMail(SalesOrder salesorder)
        {

            Customer customer = context.Customer.Where(salesorder.Customer);
            new EmailProcessor(context).SendEmail(  "CreditCardDeclinded",
                                                    customer.Email1,
                                                    " Packing Slip # " +
                                                    salesorder.SalesOrderID,
                                                    salesorder.SalesOrderID);
        }
    }
}
