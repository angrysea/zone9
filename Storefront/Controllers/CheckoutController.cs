using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using StorefrontModel;
using Storefront.Models;
using Storefront.Aspects;
using Storefront.Provider;

namespace Storefront.Controllers
{

    [HandleError]
    [OutputCache(Location = OutputCacheLocation.None)]
    public class CheckoutController :  StorefrontController
    {

        [LogMethodCall]
        [Authorize]
        public ActionResult Start(string id)
        {
            context.CheckoutTrx.Delete(id);
            CheckoutTrx checkoutTrx = new CheckoutTrx();
            checkoutTrx.Customer = id;
            context.CheckoutTrx.Insert(checkoutTrx);
            return RedirectToAction("SelectShipping/" + id, "Checkout");
        }


        [LogMethodCall]
        [Authorize]
        public ActionResult Index(string id)
        {
            CheckoutViewData viewData = new CheckoutViewData();
            AddMasterData(viewData);

            CheckoutTrx checkoutTrx = context.CheckoutTrx.Where(id);

            if (checkoutTrx == null)
            {
                checkoutTrx = new CheckoutTrx();
                checkoutTrx.Customer = id;
                context.CheckoutTrx.Insert(checkoutTrx);
                return RedirectToAction("SelectShipping/" + id, "Checkout");
            }
            else if (string.IsNullOrEmpty(checkoutTrx.ShippingAddress))
            {
                return RedirectToAction("SelectShipping/" + id, "Checkout");
            }
            else if (string.IsNullOrEmpty(checkoutTrx.CreditCard))
            {
                return RedirectToAction("SelectPayment/" + id, "Checkout");
            }
            else if (string.IsNullOrEmpty(checkoutTrx.ShippingMethod))
            {
                return RedirectToAction("SelectShippingMethod/" + id, "Checkout");
            }
            else if (string.IsNullOrEmpty(checkoutTrx.BillingAddress))
            {
                return RedirectToAction("SelectBilling/" + id, "Checkout");
            }
            else
            {
                viewData.shoppingCart =
                    context.ShoppingCartListItem.Execute("GetShoppingCart",
                                                            "cookie",
                                                            viewData.cookie);
                viewData.ShippingAddress = context.Address.First("GetAddress",
                                                                 "addressid",
                                                                 checkoutTrx.ShippingAddress);
                viewData.BillingAddress = context.Address.First("GetAddress",
                                                                "addressid",
                                                                checkoutTrx.BillingAddress);
                viewData.CreditCard = context.CreditCard.Where(checkoutTrx.CreditCard);
                viewData.CreditCard.Number = MaskCreditCard(viewData.CreditCard.Number);
            }

            return View(viewData);
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult SelectShipping(string id,
                                          string customer,
                                          string addressid,
                                          string fullname,
                                          string address1,
                                          string address2,
                                          string address3,
                                          string city,
                                          string state,
                                          string zipcode,
                                          string country,
                                          string phone)
        {
            if (Request.HttpMethod != "POST")
            {
                CheckoutViewData viewData = new CheckoutViewData();
                AddMasterData(viewData);
                viewData.Addresses = context.Address.Execute("GetAddressesByCustomer",
                                                             "customer",
                                                             id);
                viewData.States = StateCodes;
                viewData.Countries = CountryCodes;
                return View(viewData);
            }

            CheckoutTrx checkoutTrx = context.CheckoutTrx.Where(customer);

            if (string.IsNullOrEmpty(addressid))
            {
                Address address = new Address();
                address.Customer = customer;
                address.AddressID = getGuid();
                address.FullName = fullname;
                address.Address1 = address1;
                address.Address2 = address2;
                address.Address3 = address3;
                address.City = city;
                address.State = state;
                address.Zip = zipcode;
                address.Country = country;
                address.Phone = phone;
                context.Address.Insert(address);
                checkoutTrx.ShippingAddress = address.AddressID;
            }
            else
            {
                checkoutTrx.ShippingAddress = addressid;
            }

            context.CheckoutTrx.Update(checkoutTrx);

            return RedirectToAction("SelectPayment/" + customer, "Checkout");
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult SelectPayment( string id,
                                           string customer,
                                           string cardid,
                                           string cardholder,
                                           string addressid,
                                           string cardnumber,
                                           string expirationmonth,
                                           string expirationyear)
        {
            CheckoutViewData viewData = new CheckoutViewData();
            AddMasterData(viewData);

            if (Request.HttpMethod != "POST")
            {
                viewData.creditCards = context.CreditCard.Select("Customer", id);
                foreach (CreditCard card in viewData.creditCards)
                {
                    card.Number = MaskCreditCard(card.Number);
                }
                return View(viewData);
            }

            CheckoutTrx checkoutTrx = context.CheckoutTrx.Where(customer);
            if (string.IsNullOrEmpty(cardid))
            {
                cardid = getGuid();
                CreditCard creditCard = new CreditCard();
                creditCard.Customer = customer;
                creditCard.CardID = cardid;
                creditCard.Address = checkoutTrx.ShippingAddress;
                creditCard.Cardholder = cardholder;
                creditCard.Number = cardnumber;
                creditCard.Expmonth = expirationmonth;
                creditCard.Expyear = expirationyear;
                context.CreditCard.Insert(creditCard);
                checkoutTrx.CreditCard = creditCard.CardID;
            }

            checkoutTrx.CreditCard = cardid;
            context.CheckoutTrx.Update(checkoutTrx);

            return RedirectToAction("SelectShippingMethod/" + customer, "Checkout");
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult SelectShippingMethod(   string id,
                                                    string customer,
                                                    string shippingmethod)
        {
            CheckoutTrx checkoutTrx = null;
            if (Request.HttpMethod != "POST")
            {
                checkoutTrx = context.CheckoutTrx.Where(id);
                CheckoutViewData viewData = new CheckoutViewData();
                AddMasterData(viewData);
                viewData.ShippingMethods = context.ShippingMethod.Select();
                viewData.ShippingAddress = context.Address.First("GetAddress", "addressid", checkoutTrx.ShippingAddress);
                viewData.shoppingCart =
                    context.ShoppingCartListItem.Execute("GetShoppingCart",
                                                            "cookie",
                                                            viewData.cookie);
                return View(viewData);
            }

            checkoutTrx = context.CheckoutTrx.Where(customer);
            if (!string.IsNullOrEmpty(shippingmethod))
            {
                checkoutTrx.ShippingMethod = shippingmethod;
                context.CheckoutTrx.Update(checkoutTrx);
            }
            return RedirectToAction("SelectBilling/" + customer, "Checkout");
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult SelectBilling(string id,
                                          string customer,
                                          string addressid,
                                          string fullname,
                                          string address1,
                                          string address2,
                                          string address3,
                                          string city,
                                          string state,
                                          string zipcode,
                                          string country,
                                          string phone)
        {
            if (Request.HttpMethod != "POST")
            {
                CheckoutViewData viewData = new CheckoutViewData();
                AddMasterData(viewData);
                viewData.Addresses = context.Address.Execute("GetAddressesByCustomer",
                                                             "customer",
                                                             id);
                viewData.States = StateCodes;
                viewData.Countries = CountryCodes;
                return View(viewData);
            }

            CheckoutTrx checkoutTrx = context.CheckoutTrx.Where(customer);

            if (string.IsNullOrEmpty(addressid))
            {
                addressid = getGuid();
                Address address = new Address();
                address.Customer = customer;
                address.AddressID = getGuid();
                address.FullName = fullname;
                address.Address1 = address1;
                address.Address2 = address2;
                address.Address3 = address3;
                address.City = city;
                address.State = state;
                address.Zip = zipcode;
                address.Country = country;
                address.Phone = phone;
                context.Address.Insert(address);
            }

            checkoutTrx.BillingAddress = addressid;
            context.CheckoutTrx.Update(checkoutTrx);

            return RedirectToAction("Index/" + customer, "Checkout");
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult CancelCheckout(string id)
        {
            context.CheckoutTrx.Delete(id);
            return RedirectToAction("Index", "Home");
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult Authorize(string id)
        {
            CheckoutViewData viewData = new CheckoutViewData();
            AddMasterData(viewData);
            return View(viewData);
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult SalesOrder(string id)
        {
            SalesOrderViewData viewData = new SalesOrderViewData();
            AddMasterData(viewData);

            CheckoutTrx checkoutTrx = context.CheckoutTrx.Where(id);
            if (checkoutTrx == null)
            {
                checkoutTrx = new CheckoutTrx();
                checkoutTrx.Customer = id;
                context.CheckoutTrx.Insert(checkoutTrx);
                return RedirectToAction("Index/" + id, "Home");
            }

            Customer customer = context.Customer.Where(id);
            List<ShoppingCartListItem> shoppingCart =
                context.ShoppingCartListItem.Execute("GetShoppingCart",
                                                     "cookie",
                                                     viewData.cookie);

            viewData.ShippingAddress = context.Address.Where(checkoutTrx.ShippingAddress);
            viewData.BillingAddress = context.Address.Where(checkoutTrx.BillingAddress);
            viewData.CreditCard = context.CreditCard.Where(checkoutTrx.CreditCard);

            SalesOrdersProcessor processor = new SalesOrdersProcessor(context);
            viewData.DetailedSalesOrder =
                processor.GenerateSalesOrder(shoppingCart,
                                             customer,
                                             viewData.BillingAddress,
                                             viewData.ShippingAddress,
                                             checkoutTrx.CreditCard);

            viewData.DetailedSalesOrder.SalesOrder.ShippingMethod = checkoutTrx.ShippingMethod;

            PayFlowProcessor payFlow = new PayFlowProcessor(context);
            CCTransaction trans = 
                payFlow.AuthorizeCC( viewData.CreditCard,
                                     viewData.DetailedSalesOrder.SalesOrder, 
                                     viewData.BillingAddress);

            bool bError = false;
            List<string> errors = new List<string>();
            if (trans.Result != 0)
            {
                errors.Add("Error processing your credit card, please try again or use another card.");
                bError = true;
            }
            if (trans.Avsaddr == "N")
            {
                errors.Add("The billing address or zip code does not match the credit card used, please correct and try again.");
            }
            if (trans.Avszip == "N")
            {
                errors.Add("The billing address or zip code does not match the credit card used, please correct and try again.");
            }

            viewData.FailedAuthorization = bError;
            
            if (bError)
            {
                viewData.errors = errors;
            }
            else
            {
                context.SalesOrder.Insert(viewData.DetailedSalesOrder.SalesOrder);
                foreach (SalesOrderItem item in viewData.DetailedSalesOrder.Items)
                {
                    context.SalesOrderItem.Insert(item);
                }
                context.CheckoutTrx.Delete(viewData.customer.CustomerNo);
                context.ShoppingCart.Delete(viewData.cookie);
            }
            
            viewData.CreditCard.Number = MaskCreditCard(viewData.CreditCard.Number);
            viewData.ShippingAddress = context.Address.First("GetAddress", "addressid", viewData.ShippingAddress.AddressID);
            viewData.BillingAddress = context.Address.First("GetAddress", "addressid", viewData.BillingAddress.AddressID); ;
            viewData.productsInCart = 0;

            return View(viewData);
        }
    }
}
