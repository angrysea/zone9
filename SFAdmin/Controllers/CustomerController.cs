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
    public class CustomerController : StorefrontController
    {
        [LogMethodCall]
        public ActionResult Customers(string sortby)
        {
            CustomersViewData viewData = new CustomersViewData();
            
            AddMasterData(viewData);

            if (sortby == null || sortby.Length == 0)
                sortby = "Fullname";

            viewData.customers = new List<ViewCustomer>();
            List<Customer> customers = context.Customer.Select(" order by " + sortby, true);
            foreach(Customer customer in customers) 
            {
                ViewCustomer viewCustomer = new ViewCustomer();
                viewCustomer.customer = customer;
                viewCustomer.user = context.Users.Execute("GetUsersByCookie", "Cookie", customer.CustomerNo).First();
                viewData.customers.Add(viewCustomer);
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Customer(string id)
        {
            CustomerViewData viewData = new CustomerViewData();
            
            AddMasterData(viewData);

            viewData.customer = context.Customer.Where(id);
            viewData.user = context.Users.Where(id);
            viewData.user = context.Users.Execute("GetUsersByCookie", "Cookie", id).First();
            List<Address> addresses = context.Address.Select("Customer", id);

            foreach (Address address in addresses)
            {
                if (address.DefaultShipping>0)
                {
                    viewData.shippingaddress = address;
                }
            }
            viewData.creditCard = context.CreditCard.Where(id);
            viewData.salesOrders = context.SalesOrder.Select("Customer", id);
            viewData.productsInCart = context.ShoppingCart.GetCount(id);
            viewData.mode = 'E';
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Coupons()
        {
            CouponsViewData viewData = new CouponsViewData();
            AddMasterData(viewData);
            viewData.coupons = context.Coupon.Select();
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult CouponMaint(string id)
        {
            CouponViewData viewData = new CouponViewData();

            AddMasterData(viewData);
            viewData.manufacturers = context.Manufacturer.Select();
            viewData.categories = context.Category.Select();

            if (id == null || id.Length == 0)
            {
                viewData.coupon = null;
                viewData.update = false;
            }
            else
            {
                viewData.coupon = context.Coupon.Where(id);
                viewData.update = true;
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult CouponAction(string mode,
                                            string code,
                                            string description,
                                            string productid,
                                            string category,
                                            string manufacturer,
                                            string quantitylimit,
                                            string quantityrequired,
                                            string minimumprice,
                                            string discounttype,
                                            string discount,
                                            string precludes,
                                            string oneperhousehold,
                                            string singleuse,
                                            string displayonweb,
                                            string imageurl,
                                            string expirationdate)
        {
            Coupon coupon = null;
            if (mode.ToUpper().Equals("I"))
            {
                coupon = new Coupon();
            }
            else
            {
                coupon = context.Coupon.Where(code);
            }

            if (mode.ToUpper().Equals("D"))
            {
                context.Coupon.Delete(coupon);
            }
            else
            {
		        coupon.Code = code;
		        coupon.Product = productid;
		        coupon.Manufacturer = manufacturer;
		        coupon.Category = category;
		        coupon.Description = description;
                if(quantitylimit!=null && quantitylimit.Length>0)
		            coupon.QuantityLimit = Int32.Parse(quantitylimit);
                if (quantityrequired != null && quantityrequired.Length > 0)
                    coupon.QuantityRequired = Int32.Parse(quantityrequired);
                if (minimumprice != null && minimumprice.Length > 0)
                    coupon.PriceMinimum = double.Parse(minimumprice);
                if (discount != null && discount.Length > 0)
                    coupon.Discount = double.Parse(discount);
                if (discounttype != null && discounttype.Length > 0)
                    coupon.DiscountType = Int16.Parse(discounttype);
                if (precludes != null && precludes=="on")
                    coupon.Precludes = 1;
                if (oneperhousehold != null && oneperhousehold == "on")
                    coupon.OneperHousehold = 1;
                if (singleuse != null && singleuse=="on")
                    coupon.SingleUse = 1;
                if (displayonweb != null && displayonweb=="on")
                    coupon.Display = 1;
                coupon.Imageurl = imageurl;
		        coupon.Redemptions = 0;
                if (expirationdate != null && expirationdate.Length > 0)
                    coupon.ExpirationDate = DateTime.Parse(expirationdate);

                
                if (mode.ToUpper().Equals("I"))
                {
                    context.Coupon.Insert(coupon);
                }
                else
                {
                    context.Coupon.Update();
                }

            }

            return RedirectToAction("Coupons", "Customer");
        }

        [LogMethodCall]
        public ActionResult EmailCoupon(string id)
        {
            SendCouponViewData viewData = new SendCouponViewData();
            AddMasterData(viewData);
            viewData.coupon = context.Coupon.Where(id);
            viewData.update = true;
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult SalesOrders(string fromdate, string todate, string opensalesorders)
        {
            SalesOrdersViewData viewData = new SalesOrdersViewData();

            AddMasterData(viewData);
            viewData.salesOrders = new List<SalesOrderView>();
            List<SalesOrder> salesOrders = null;

            if (Request.HttpMethod != "POST" || opensalesorders == "on")
            {
                salesOrders = context.SalesOrder.Execute("GetOpenSalesOrders", (string)null, (string)null);
            }
            else
            {
	            string [] parameters = { "StartDate", "EndDate" };
                string[] values = { fromdate, todate };
                salesOrders = context.SalesOrder.Execute("GetSalesOrdersByDate", parameters, values);
            }

            foreach(SalesOrder salesOrder in salesOrders)
            {
                SalesOrderView salesOrderView = new SalesOrderView();
                salesOrderView.customer = context.Customer.Where(salesOrder.Customer);
                salesOrderView.salesOrder = salesOrder;
                salesOrderView.salesOrderItemsView = new List<SalesOrderItemView>();  
                List<SalesOrderItem> salesOrderItems = 
                    context.SalesOrderItem.Select(salesOrder.SalesOrderID);
                
                foreach(SalesOrderItem salesOrderItem in salesOrderItems)
                {
                    SalesOrderItemView salesOrderItemView = new SalesOrderItemView();
                    salesOrderItemView.salesOrderItem = salesOrderItem;
                    salesOrderItemView.product = context.Product.Where(salesOrderItem.ProductNo);
                    salesOrderView.salesOrderItemsView.Add(salesOrderItemView);
                }
                viewData.salesOrders.Add(salesOrderView);
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult SalesOrder(string id)
        {
            SalesOrderViewData viewData = new SalesOrderViewData();
            AddMasterData(viewData);

            viewData.DetailedSalesOrder = new DetailedSalesOrder();
            viewData.DetailedSalesOrder.SalesOrder = context.SalesOrder.Where(id);
            viewData.Customer = context.Customer.Where(viewData.DetailedSalesOrder.SalesOrder.Customer);
            viewData.DetailedSalesOrder.Items = context.SalesOrderItem.Select("SalesOrder", id);
            viewData.ShippingAddress = context.Address.First("GetAddress", "addressid", viewData.DetailedSalesOrder.SalesOrder.ShippingAddress);
            viewData.BillingAddress = context.Address.First("GetAddress", "addressid", viewData.DetailedSalesOrder.SalesOrder.BillingAddress);
            viewData.DetailedSalesOrder.SalesOrder.ShippingMethod = viewData.DetailedSalesOrder.SalesOrder.ShippingMethod;
            viewData.CreditCard = context.CreditCard.Where(viewData.DetailedSalesOrder.SalesOrder.CardID);
            viewData.CreditCard.Number = MaskCreditCard(viewData.CreditCard.Number);
            viewData.ShippingMethod = context.ShippingMethod.Where(viewData.DetailedSalesOrder.SalesOrder.ShippingMethod);
            viewData.distributors = context.Distributor.Select();
            NoteCount noteCount = context.NoteCount.First("NoteCount", "ReferenceNo", id);
            viewData.noteCount = noteCount.noteCount;
            return View(viewData);
        }


        [LogMethodCall]
        public ActionResult DeleteSalesOrder(string id)
        {
            new SalesOrdersProcessor(context).Delete(id);
            return RedirectToAction("SalesOrders/", "Customer");
        }

        [LogMethodCall]
        public ActionResult DropshipSalesOrder(string referenceno, string distributor)
        {
            new SalesOrdersProcessor(context).DropShipSalesOrder(referenceno, distributor);
            return RedirectToAction("SalesOrders/", "Customer");
        }

        [LogMethodCall]
        public ActionResult Invoices(string fromdate, string todate, string declinedinvoicesonly)
        {
            InvoicesViewData viewData = new InvoicesViewData();

            AddMasterData(viewData);
            viewData.invoices = new List<InvoiceView>();

            List<Invoice> invoices = null;
            if (declinedinvoicesonly == "1")
            {
                invoices = context.Invoice.Execute("GetDeclinedInvoices", (string)null, (string)null);
            }
            else
            {
                string[] parameters = { "StartDate", "EndDate" };
                string[] values = { fromdate, todate };
                invoices = context.Invoice.Execute("GetInvoicesByDate", parameters, values);
            }
            foreach (Invoice invoice in invoices)
            {
                InvoiceView invoiceView = new InvoiceView();
                invoiceView.customer = context.Customer.Where(invoice.Customer);
                invoiceView.invoice = invoice;
                viewData.invoices.Add(invoiceView);
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Invoice(string id)
        {
            InvoiceViewData viewData = new InvoiceViewData();

            AddMasterData(viewData);

            viewData.invoice = context.Invoice.Where(id);
            viewData.customer = context.Customer.Where(viewData.invoice.Customer);
            List<Address> addresses = context.Address.Select(viewData.customer.CustomerNo);

            //foreach (Address address in addresses)
            //{
            //    if (address.Type == "CurrentBilling")
            //    {
            //        viewData.billing = address;
            //        break;
            //    }
            //}
            //viewData.creditCard = context.CreditCard.Where(viewData.customer.CustomerNo);
            viewData.invoiceItems = context.InvoiceItem.Select(viewData.invoice.InvoiceID);

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult ShoppingCart(string id)
        {
            ShoppingCartViewData viewData = new ShoppingCartViewData();
            AddMasterData(viewData);
            viewData.shoppingCart = 
                context.ShoppingCartListItem.Execute("GetShoppingCart", 
                                                        "cookie", id);
            viewData.UserID = id;
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Users(string fromdate, string todate)
        {
            UsersViewData viewData = new UsersViewData();

            AddMasterData(viewData);
            viewData.users = new List<ViewUser>();

            // Non-POST requests should just display the form 
            if (Request.HttpMethod != "POST")
            {
                return View(viewData);
            }

            List<Users> users = null;
            string[] parameters = { "FromDate", "ToDate" };
            string[] values = { fromdate, todate };
            users = context.Users.Execute("GetUsersByDate", parameters, values);

            foreach (Users user in users)
            {
                ViewUser viewUser = new ViewUser();
                viewUser.user = user;
                viewUser.productsInCart = context.ShoppingCart.GetCount(user.Cookie);
                viewData.users.Add(viewUser);
            }
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Notes(string id, string referenceno, string notetext)
        {
            NotesViewData viewData = new NotesViewData();
            if (Request.HttpMethod != "POST")
            {
                viewData.ReferenceNo = id;
                viewData.Notes = context.Note.Select(id);
            }
            else
            {
                Note note = new Note();
                note.Referencenumber = referenceno;
                note.Text = notetext;
                note.Creationdate = DateTime.Now;
                context.Note.Insert(note);
                viewData.ReferenceNo = referenceno;
                viewData.Notes = context.Note.Select(referenceno);
            }

            return View(viewData);
        }
    }
}
