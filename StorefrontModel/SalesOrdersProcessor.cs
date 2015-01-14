using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace StorefrontModel
{
    public class SalesOrdersProcessor
    {
        private Entities context;

        public SalesOrdersProcessor(Entities context)
        {
            this.context = context;
        }

        public DetailedSalesOrder GenerateSalesOrder(
                    List<ShoppingCartListItem> shoppingCart,
                    Customer customer,
                    Address billingAddress,
                    Address shippingAddress,
                    string cardID) 
        {
            DetailedSalesOrder salesorder = new DetailedSalesOrder();
            salesorder.SalesOrder = new SalesOrder();
            salesorder.SalesOrder.SalesOrderID = context.getGuid();
            salesorder.SalesOrder.Customer = customer.CustomerNo;
            salesorder.SalesOrder.CardID = cardID;
            salesorder.SalesOrder.BillingAddress = billingAddress.AddressID;
            salesorder.SalesOrder.ShippingAddress = shippingAddress.AddressID;
            salesorder.SalesOrder.Creationdate = DateTime.Now;

            double totalcost = 0;
            double totalweight = 0;
            salesorder.Items = new List<SalesOrderItem>();
            foreach(ShoppingCartListItem cartitem in shoppingCart)
            {
                SalesOrderItem item = new SalesOrderItem();
                item.SalesOrder = salesorder.SalesOrder.SalesOrderID;
                Product product = context.Product.Where(cartitem.Product);
                Details details = context.Details.Where(cartitem.Product);

                if(product==null)
                    continue;

                item.SOItem = context.getGuid();
                item.ProductNo = cartitem.Product;
                item.GTIN = product.GTIN;
                item.ProductName = cartitem.Name; 
                item.Manufacturer = product.Manufacturer;
                item.Distributor = product.Distributor;
                item.Quantity = cartitem.Quantity;
                item.Unitprice = cartitem.OurPrice;
                item.Shippingweight = details.ShippingWeight;
                item.Handlingcharges = details.HandlingCharges;
                item.Status = "New";
                item.TrxType = "S"; // S is for sale.
                item.Zipcode = shippingAddress.Zip;
                item.Country = shippingAddress.Country;
                item.Availability = product.Availability;
                totalcost += item.Unitprice * item.Quantity;
                totalweight += item.Shippingweight * item.Quantity;
                salesorder.Items.Add(item);

                ProductRanking ranking = context.ProductRanking.Where(cartitem.Product);
                ranking.Sold += item.Quantity;
                context.ProductRanking.Update(ranking);
                product.QuantityToOrder += item.Quantity;
                context.Product.Update(product);
            }

            salesorder.SalesOrder.Status = "New";
            salesorder.SalesOrder.ShippingWeight = totalweight;
            salesorder.SalesOrder.TotalCost = totalcost;

            CalculateCouponSavings(salesorder, false);
            CalculateTax(salesorder.SalesOrder, shippingAddress);

            salesorder.SalesOrder.Total = 
                    (salesorder.SalesOrder.TotalCost - salesorder.SalesOrder.Discount) +
                    salesorder.SalesOrder.Taxes + salesorder.SalesOrder.Shipping +
                    salesorder.SalesOrder.Handling;

            return salesorder;
        }

        public void CalculateShippingCost(SalesOrder salesorder)
        {
            salesorder.Shipping = 0;
            salesorder.ShippingWeight = 0;
            //new ShippingProcessor(context).GetShippingCost(salesorder, false);
        }

        private void calculateTotals(SalesOrder salesorder, List<SalesOrderItem> items, bool ? bReady)
        {
            double totalcost = 0;
            double totalweight = 0;
      
            foreach(SalesOrderItem item in items)
            {
                if (bReady == false) 
                {
                    totalcost += item.Quantity * item.Unitprice;
                    totalweight += item.Quantity * item.Shippingweight;
                }
                else 
                {
                    totalcost += item.QuantityToShip * item.Unitprice;
                    totalweight += item.QuantityToShip * item.Shippingweight;
                }
            }

            salesorder.TotalCost = totalcost;
            salesorder.ShippingWeight = totalweight;
        }

        void CalculateCouponSavings(DetailedSalesOrder detailed, bool? bReady)
        {
            SalesOrder salesorder = detailed.SalesOrder;
            List<SalesOrderItem> items = detailed.Items;

            double totalpercent = 0;
            double totalamount = 0;
            bool freeshipping = false;
            bool precludes = false;
            string newcode = "";
            string description = "";

            if (!string.IsNullOrEmpty(salesorder.Couponcode)) 
            {
                string [] tokens = salesorder.Couponcode.Split(' ');
                HashSet<string> coupons = new HashSet<string>();
                for(int i=0; i<tokens.Length && !precludes; i++)
                {
                    if (coupons.Contains(tokens[i]))
                        continue;
                    coupons.Add(tokens[i]);
                    Coupon coupon = context.Coupon.Where(tokens[i]);
                    if (coupon != null) 
                    {
                        if (!IsCouponRedeemed(  salesorder.Customer,
                                                salesorder.SalesOrderID, 
                                                coupon)) 
                        {
                            long currentTime = DateTime.Now.Ticks;
                            long experationtime = coupon.ExpirationDate.Ticks;
                            if (currentTime < experationtime) 
                            {
                                if (coupon.Precludes>0) 
                                {
                                    precludes = true;
                                    description = "";
                                    newcode = "";
                                    totalamount = 0;
                                }

                                if (!string.IsNullOrEmpty(coupon.Product) || 
                                        !string.IsNullOrEmpty(coupon.Manufacturer))
                                {
                                    bool bUsed=false;
                                    foreach(SalesOrderItem item in items)
                                    {
                                        if (coupon.Product == item.ProductNo ||
                                                item.Manufacturer == coupon.Manufacturer) 
                                        {
                                            int qty = 0;
                                            if (bReady == false) 
                                            {
                                                if (coupon.QuantityLimit > 0 &&
                                                    item.Quantity > coupon.QuantityLimit) 
                                                {
                                                    qty = coupon.QuantityLimit;
                                                }
                                                else 
                                                {
                                                    qty = item.Quantity;
                                                }
                                            }
                                            else 
                                            {
                                                if (coupon.QuantityLimit > 0 &&
                                                    (item.QuantityToShip + item.Shipped) >
                                                        coupon.QuantityLimit) 
                                                {
                                                    qty = coupon.QuantityLimit;
                                                }
                                                else 
                                                {
                                                    qty = item.QuantityToShip;
                                                }
                                            }
                                            
                                            if (coupon.QuantityRequired > 0) 
                                            {
                                                if (coupon.QuantityRequired > qty) 
                                                {
                                                    continue;
                                                }
                                            }
    
                                            if (coupon.DiscountType == 1) 
                                            {
                                                totalamount += coupon.Discount * qty;
                                                if(!bUsed) 
                                                {
                                                    if (description.Length > 0)
                                                        description += ", ";
                                                    description += coupon.Description;
                                                    newcode += coupon.Code + " ";
                                                }
                                                bUsed=true;
                                            }
                                            else if (coupon.DiscountType == 2) 
                                            {
                                                totalamount += (item.Unitprice * qty) * coupon.Discount;
                                                if (!bUsed) 
                                                {
                                                    if (description.Length > 0)
                                                        description += ", ";
                                                    description += coupon.Description;
                                                    newcode += coupon.Code + " ";
                                                }
                                                bUsed=true;
                                            }
                                        }
                                    }
                                }
                                else 
                                {
                                    if (coupon.QuantityRequired > 0) 
                                    {
                                        int qty = 0;
                                        foreach(SalesOrderItem item in items)
                                        {
                                            qty += item.Quantity;
                                        }
                                        if (coupon.QuantityRequired > qty) 
                                        {
                                            continue;
                                        }
                                    }
                                    
                                    if (coupon.DiscountType == 1) 
                                    {
                                        if (coupon.PriceMinimum < 1 ||
                                            coupon.PriceMinimum > salesorder.TotalCost) 
                                        {
                                            totalamount += coupon.Discount;
                                            if (description.Length > 0)
                                                description += ", ";
                                            description += coupon.Description;
                                            newcode += coupon.Code + " ";
                                        }
                                    }
                                    else if (coupon.DiscountType == 2) 
                                    {
                                        if (coupon.PriceMinimum < 1 ||
                                            coupon.PriceMinimum > salesorder.TotalCost) 
                                        {
                                            totalpercent += coupon.Discount;
                                            if (description.Length > 0)
                                                description += ", ";
                                            description += coupon.Description;
                                            newcode += coupon.Code + " ";
                                        }
                                    }
                                    else if (coupon.DiscountType == 3) 
                                    {
                                        freeshipping = true;
                                        if (description.Length > 0)
                                            description += ", ";
                                        description += coupon.Description;
                                        newcode += coupon.Code + " ";
                                    }
                                }
                            }
                        }
                    }
                }
            }

            double discountamt = 0;
            discountamt = totalamount;
            discountamt += (salesorder.TotalCost - discountamt) * totalpercent;
            if (freeshipping) 
            {
                discountamt += salesorder.Shipping + salesorder.Handling;
            }

            salesorder.DiscountDescription = description;
            salesorder.Couponcode = newcode;
            salesorder.Discount = discountamt;
            salesorder.Total = salesorder.Total - discountamt;
        }

        public void CalculateTax(SalesOrder salesorder, Address shippingAddress) 
        {
            salesorder.TaxesDescription = "";
            salesorder.Taxes = 0;
	        string [] parameters = {"selltocity", "selltostate", "selltocounty" };
            object [] values = { shippingAddress.City, shippingAddress.State, shippingAddress.Country };
            List<SalesTax> salesTaxes = context.SalesTax.Execute("FindSalesTax", parameters, values);
            
            StringBuilder description = new StringBuilder();
            double taxAmount = 0;
            foreach(SalesTax tax in salesTaxes)
            {
                description.Append(tax.Description);
                description.Append(" ");
                taxAmount += ( salesorder.TotalCost  -
                               salesorder.Discount) *
                               tax.TaxRate;
            }

            if (description.Length == 0 && taxAmount>0)
            {
                description.Append("Tax");
            }

            salesorder.Description = description.ToString();
            salesorder.Taxes = taxAmount;
            salesorder.Total = ( (  salesorder.TotalCost - 
                                    salesorder.Discount ) +
                                 salesorder.Taxes +
                                 salesorder.Shipping + 
                                 salesorder.Handling );
        }

        bool IsCouponRedeemed(string customer, string salesorder, Coupon coupon)
        {
            CouponTrx couponTrx = null;
            if (coupon.SingleUse > 0) 
            {
                string [] parameters = { "CouponCode", "SalesOrder" };
                object [] values = { coupon.Code, salesorder };
                couponTrx = context.CouponTrx.First(parameters, values);
            }
            else if(coupon.OneperHousehold>0) 
            {
                string[] parameters = { "CouponCode", "SalesOrder", "Customer" };
                object[] values = { coupon.Code, salesorder, customer };
                couponTrx = context.CouponTrx.First(parameters, values);
            }
            else 
            {
                return false;
            }
            return couponTrx!=null;
        }

        public void DropShipSalesOrder(string salesOrder, string distributor)
        {
            SalesOrder salesorder = context.SalesOrder.Where(salesOrder);
            if (salesorder != null)
            {
                List<SalesOrderItem> items = context.SalesOrderItem.Select("SalesOrder", salesOrder);
                if (items.Count > 0)
                {
                    List<Product> products = new List<Product>();
                    foreach (SalesOrderItem item in items)
                    {
                        Product product = context.Product.Where(item.ProductNo);
                        product.QuantityToOrder = item.Quantity;
                        products.Add(product);
                    }
                    string poid = "";
                    //TODO: SendDropShipMail(salesOrder, distributor);
                    salesorder.DropShipped = 1;
                    string description = "Sales Order dropped shipped from distributor " +
                            distributor + ". For more details see purchase order # " +
                            poid + ".";
                    salesorder.Description = description;
                    salesorder.Status = "Drop Shipped";
                    context.SalesOrder.Update(salesorder);

                }
            }
        }

        public bool IsReadyToShip(SalesOrder salesorder)
        {
            bool bRet = true;
            List<SalesOrderItem> salesorderitems = context.SalesOrderItem.Select(salesorder.SalesOrderID);

            foreach(SalesOrderItem item in salesorderitems) {
                if (item.Status == "Ready to Ship" ||
                    item.Quantity != item.QuantityToShip)
                {
                    bRet = false;
                    break;
                }
            }
            return bRet;
        }

        public void Shipped(string id, int qty)
        {
            SalesOrderItem item = context.SalesOrderItem.Where(id);
            string status = null;

            if( (qty+item.Shipped) < item.Quantity )
            {
                status = "Partially Shipped";
            }
            else
            {
                status = "Shipped";
            }

            item.Status=status;
            item.QuantityToShip-=qty;
            item.Shipped+=qty;
            context.SalesOrderItem.Update(item);
        }

        public void Delete(string id)
        {
            List<SalesOrderItem> salesorderitems = context.SalesOrderItem.Select("SalesOrder", id);

            foreach (SalesOrderItem salesorderitem in salesorderitems)
            {
                ProductRanking ranking = context.ProductRanking.Where(salesorderitem.ProductNo);
                ranking.Sold -= salesorderitem.Quantity;
                context.ProductRanking.Update(ranking);
                Product product = context.Product.Where(salesorderitem.ProductNo);
                product.Allocated -= salesorderitem.Quantity;
                product.QuantityToOrder -= salesorderitem.Quantity;
                context.Product.Update(product);
                context.SalesOrderItem.Delete(salesorderitem);
            }
            context.SalesOrder.Delete(id);
            context.Note.Delete(id);
        }
    }
}
