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
    public class AccountController :  StorefrontController
    {
        public AccountController()
            : this(null, null)
        {
        }

        public AccountController(IFormsAuthentication formsAuth, MembershipProvider provider)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationWrapper();
            Provider = provider ?? new StorefrontProvider();
        }

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public MembershipProvider Provider
        {
            get;
            private set;
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult ChangePassword(string currentpassword, string password, string confirmpassword)
        {
            AccountViewData viewData = new AccountViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);
            viewData.PasswordLength = Provider.MinRequiredPasswordLength;


            // Non-POST requests should just display the ChangePassword form 
            if (Request.HttpMethod != "POST")
            {
                return View(viewData);
            }

            // Basic parameter validation
            List<string> errors = new List<string>();
            if (String.IsNullOrEmpty(currentpassword))
            {
                errors.Add("You must specify a current password.");
            }
            if (password == null || password.Length < Provider.MinRequiredPasswordLength)
            {
                errors.Add(String.Format(CultureInfo.InvariantCulture,
                         "You must specify a new password of {0} or more characters.",
                         Provider.MinRequiredPasswordLength));
            }
            if (!String.Equals(password, confirmpassword, StringComparison.Ordinal))
            {
                errors.Add("The new password and confirmation password do not match.");
            }

            if (errors.Count == 0)
            {
                MembershipUser currentUser = Provider.GetUser(User.Identity.Name, true /* userIsOnline */);
                bool changeSuccessful = false;
                try
                {
                    changeSuccessful = currentUser.ChangePassword(currentpassword, password);
                }
                catch
                {
                    errors.Add("Unknown error please contact customer support.");
                }

                if (changeSuccessful)
                {
                    return RedirectToAction("YourAccount", "Account", 
                                                 new{
                                                      msg = "Password successfully changed."
                                                 } );
                }
                else
                {
                    errors.Add("The current password is incorrect or the new password is invalid.");
                }
            }

            viewData.errors = errors;
            return View(viewData);
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult ChangePasswordSuccess()
        {
            AccountViewData viewData = new AccountViewData();
            AddMasterData(viewData);
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Login(string email, string password, string gotourl)
        {
            SFViewData viewData = new SFViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            if (Request.HttpMethod != "POST")
            {
                viewData.gotourl = Request.Params["ReturnUrl"];
                return View(viewData);
            }

            List<string> errors = new List<string>();
            if (String.IsNullOrEmpty(email))
            {
                errors.Add("You must specify a Email address.");
            }

            if (errors.Count == 0)
            {
                bool loginSuccessful = Provider.ValidateUser(email, password);
                viewData.user = context.Users.Where(email);
                if (loginSuccessful)
                {
                    FormsAuth.SetAuthCookie(email, true);
                    if (viewData.cookie != viewData.user.Cookie)
                    {
                        string [] parameters = {"cookie", "oldcookie"};
                        object [] values = { viewData.user.Cookie, viewData.cookie };
                        context.ShoppingCart.ExecuteProc("MoveCart", parameters, values);
                        viewData.cookie = viewData.user.Cookie;
                    }
                    SetStorefrontCookie(viewData.user.Cookie, Response);
                    viewData.cookie = viewData.user.Cookie;
                    if (gotourl == null || gotourl.Length==0)
                    {
                        gotourl = "/Home/Index/" + viewData.cookie;
                    }
                    return Redirect(gotourl); 
                }
                else
                {
                    errors.Add("The username or password provided is incorrect.");
                }
            }

            viewData.errors = errors;
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Logout()
        {
            FormsAuth.SignOut();
            SetStorefrontCookie(Response);
            return RedirectToAction("Index", "Home");
        }

        [LogMethodCall]
        public ActionResult SignOut()
        {
            return Logout();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult YourAccount()
        {
            AccountViewData viewData = new AccountViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);
            string msg = Request.Params["msg"];
            if (msg != null && msg.Length>0)
            {
                List<string> errors = new List<string>();
                errors.Add("Password successfully changed.");
                viewData.errors = errors;
            }

            return View(viewData);
        }


        [LogMethodCall]
        public ActionResult CreateAccount(string id, string gotourl)
        {
            AccountViewData viewData = new AccountViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);
            viewData.PasswordLength = Provider.MinRequiredPasswordLength;
            viewData.update = false;
            viewData.EMail = id;
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult CreateAccountAction
                                         (  string mode,
                                            string gotourl,
                                            string fullname,
                                            string email,
                                            string confirmemail,
                                            string password,
                                            string confirmpassword)
        {
            List<string> errors = null;
            errors = new List<string>();

            if (String.IsNullOrEmpty(email))
            {
                errors.Add("You must specify an email address.");
            }

            if (password == null || password.Length < Provider.MinRequiredPasswordLength)
            {
                errors.Add(String.Format(CultureInfo.InvariantCulture,
                         "You must specify a password of {0} or more characters.",
                         Provider.MinRequiredPasswordLength));
            }

            if (!String.Equals(password, confirmpassword, StringComparison.Ordinal))
            {
                errors.Add("The password and confirmation do not match.");
            }

            if (errors.Count == 0)
            {
                string cookie = GetStorefrontCookie(Request, Response);
                MembershipCreateStatus createStatus;
                MembershipUser newUser = 
                    Provider.CreateUser(cookie, 
                                        password, 
                                        email, 
                                        null, 
                                        null, 
                                        true,
                                        null, 
                                        out createStatus);
                if (newUser != null)
                {
                    Customer customer = new Customer();
                    customer.CustomerNo = cookie;
                    customer.Email1 = email;
                    customer.Fullname = fullname;
                    customer.OptOut = 1;
                    context.Customer.Insert(customer);
                    FormsAuth.SetAuthCookie(email, false);
                    if (gotourl == null)
                    {
                        gotourl = "/Account/Index";
                    }
                    return Redirect(gotourl);
                }
                else
                {
                    errors.Add(ErrorCodeToString(createStatus));
                }
            }

            AccountViewData viewData = new AccountViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);
            viewData.PasswordLength = Provider.MinRequiredPasswordLength;
            viewData.update = false;
            viewData.errors = errors;
            viewData.EMail = email;

            return View(viewData);
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult CreditCardList(string id)
        {
            AddressBookViewData viewData = new AddressBookViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            viewData.AddressBook = new List<AddressBookEntries>();
            List<CreditCard> creditCards = context.CreditCard.Select("Customer", id);
            foreach (CreditCard creditCard in creditCards)
            {
                AddressBookEntries addressBookEntries = new AddressBookEntries();
                addressBookEntries.CreditCard = creditCard;
                Address address = context.Address.First("GetAddress", 
                                                        "addressid", 
                                                        creditCard.Address, 
                                                        new Address());
                addressBookEntries.Address = address;
                addressBookEntries.CreditCard.Number = MaskCreditCard(addressBookEntries.CreditCard.Number);
                viewData.AddressBook.Add(addressBookEntries);
            }

            return View(viewData);
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult CreditCardAdd(string id,
                                            string cardholder,
                                            string cctype,
                                            string cardnumber,
                                            string CCV2,
                                            string expirationmonth,
                                            string expirationyear,
                                            string addressid,
                                            string fullname,
                                            string address1,
                                            string address2,
                                            string address3,
                                            string city,
                                            string state,
                                            string zipcode,
                                            string country,
                                            string phone )
        {
            CreditCardViewData viewData = new CreditCardViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            if (Request.HttpMethod != "POST")
            {
                viewData.States = StateCodes;
                viewData.Countries = CountryCodes;
                viewData.Addresses = context.Address.Execute("GetAddressesByCustomer",
                                                                "customer",
                                                                viewData.customer.CustomerNo);
                return View(viewData);
            }

            if (string.IsNullOrEmpty(addressid))
            {
                if (string.IsNullOrEmpty(fullname))
                {
                    addressid = getGuid();
                    Address address = new Address();
                    address.Customer = viewData.customer.CustomerNo;
                    address.AddressID = addressid;
                    address.FullName = fullname;
                    address.Address1 = address1;
                    address.Address2 = address2;
                    address.Address3 = address3;
                    address.City = city;
                    address.Country = country;
                    address.State = state;
                    address.Zip = zipcode;
                    address.Phone = phone;
                    address.Description = fullname + " " + city;
                    context.Address.Insert(address);
                }
            }

            CreditCard creditCard = new CreditCard();
            creditCard.CardID = getGuid();
            creditCard.Customer = viewData.cookie;
            creditCard.Type = cctype;
            creditCard.Address = addressid;
            creditCard.Cardholder = cardholder;
            creditCard.Number = cardnumber;
            creditCard.CCV2 = CCV2;
            creditCard.Expmonth = expirationmonth;
            creditCard.Expyear = expirationyear;
            context.CreditCard.Insert(creditCard);

            return RedirectToAction("CreditCardList/" + viewData.customer.CustomerNo, "Account");
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult CreditCardMaint(string id,
                                            string cardholder,
                                            string cardid,
                                            string addressid,
                                            string cardnumber,
                                            string expirationmonth,
                                            string expirationyear )
        {
            CreditCardViewData viewData = new CreditCardViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            if (Request.HttpMethod != "POST")
            {
                viewData.States = StateCodes;
                viewData.Countries = CountryCodes;
                viewData.CreditCard = context.CreditCard.Where(id);
                viewData.Address = context.Address.First("GetAddress",
                                                        "addressid",
                                                        viewData.CreditCard.Address);
                viewData.Addresses = context.Address.Execute("GetAddressesByCustomer",
                                                                "customer",
                                                                viewData.customer.CustomerNo);
                return View(viewData);
            }

            CreditCard creditCard = context.CreditCard.Where(id);
            creditCard.Customer = viewData.cookie;
            creditCard.Address = addressid;
            creditCard.Cardholder = cardholder;
            creditCard.Number = cardnumber;
            creditCard.Expmonth = expirationmonth;
            creditCard.Expyear = expirationyear;
            context.CreditCard.Update(creditCard);

            return RedirectToAction("CreditCardList/" + viewData.customer.CustomerNo, "Account");
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult CreditCardNewAddr(  string id,
                                                string mode,
                                                string cardid,
                                                string customer,
                                                string cardholder,
                                                string cardnumber,
                                                string expirationmonth,
                                                string expirationyear,
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
            CreditCardViewData viewData = new CreditCardViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            if (mode == "U")
            {
                CreditCard creditCard = new CreditCard();
                creditCard.CardID = cardid;
                creditCard.Customer = customer;
                creditCard.Address = addressid;
                creditCard.Cardholder = cardholder;
                creditCard.Number = cardnumber;
                creditCard.Expmonth = expirationmonth;
                creditCard.Expyear = expirationyear;
                viewData.CreditCard = creditCard;
                viewData.States = StateCodes;
                viewData.Countries = CountryCodes;
                viewData.Addresses = context.Address.Execute("GetAddressesByCustomer",
                                                                "customer",
                                                                viewData.customer.CustomerNo); 
                return View(viewData);
            }

            addressid = getGuid();
            CreditCard card = context.CreditCard.Where(cardid);
            card.CardID = cardid;
            card.Customer = customer;
            card.Address = addressid;
            card.Cardholder = cardholder;
            card.Number = cardnumber;
            card.Expmonth = expirationmonth;
            card.Expyear = expirationyear;
            context.CreditCard.Update(card);

            Address address = new Address();
            address.Customer = viewData.customer.CustomerNo;
            address.AddressID = addressid;
            address.FullName = fullname;
            address.Address1 = address1;
            address.Address2 = address2;
            address.Address3 = address3;
            address.City = city;
            address.Country = country;
            address.State = state;
            address.Zip = zipcode;
            address.Phone = phone;
            address.Description = fullname + " " + city;
            context.Address.Insert(address);
            return RedirectToAction("CreditCardList/" + viewData.customer.CustomerNo, "Account");
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult CreditCardDelete(string id, string code)
        {
            context.CreditCard.Delete(id);
            return RedirectToAction("CreditCardList/" + code, "Account");
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult Wishlist(string id)
        {
            SFViewData viewData = new SFViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);
            return View(viewData);
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult AddressBook( string id)
        {
            AddressBookViewData viewData = new AddressBookViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            viewData.AddressBook = new List<AddressBookEntries>();
            List<Address> addresses = context.Address.Execute(  "GetAddressesByCustomer",
                                                                "customer",
                                                                id );
            foreach (Address address in addresses)
            {
                AddressBookEntries addressBookEntries = new AddressBookEntries();
                string [] parameters = { "Customer", "Address" };
                object [] values = { viewData.customer.CustomerNo, address.AddressID };
                addressBookEntries.CreditCard = context.CreditCard.First(parameters, values, new CreditCard());
                if (addressBookEntries.CreditCard != null &&
                    addressBookEntries.CreditCard.Number != null )
                {
                    addressBookEntries.CreditCard.Number = MaskCreditCard(addressBookEntries.CreditCard.Number);
                }
                addressBookEntries.Address = address;
                viewData.AddressBook.Add(addressBookEntries);
            }

            return View(viewData);
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult AddressDelete(string id, string code)
        {
            context.Address.Delete(id);

            CreditCard creditCard = context.CreditCard.Where(code);
            if (creditCard != null)
            {
                creditCard.Address = "";
                context.CreditCard.Update();
            }

            return RedirectToAction("AddressBook/" + code, "Account");
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult AddressAdd( string id,
                                        string fullname,
                                        string address1,
                                        string address2,
                                        string address3,
                                        string city,
                                        string state,
                                        string zipcode,
                                        string country,
                                        string phone,
                                        string defaultbilling )
        {
            AddressViewData viewData = new AddressViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);
            
            if (Request.HttpMethod != "POST")
            {
                viewData.update = false;
                viewData.States = StateCodes;
                viewData.Countries = CountryCodes;

                return View(viewData);
            }

            Address shipping = 
                context.Address.First( "GetDefaultShipping", 
                                       "customer", 
                                       id );

            Address address = new Address();
            address.Customer = viewData.customer.CustomerNo;
            address.AddressID = getGuid();
            address.FullName = fullname;
            address.Address1 = address1;
            address.Address2 = address2;
            address.Address3 = address3;
            address.City = city;
            address.Country = country;
            address.State = state;
            address.Zip = zipcode;
            address.Phone = phone;
            address.Description = fullname + " " + city;

            if(defaultbilling != null)
            {
                address.DefaultBilling = Int16.Parse(defaultbilling);
            }
            else 
            {
                address.DefaultBilling = 0;
            }

            address.DefaultShipping = (short)(shipping == null ? 1 : 0);
            context.Address.Insert(address);

            return RedirectToAction("AddressBook/" + viewData.customer.CustomerNo, "Account");
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult AddressMaint( string id,
                                          string addressid,
                                          string fullname,
                                          string address1,
                                          string address2,
                                          string address3,
                                          string city,
                                          string state,
                                          string zipcode,
                                          string country,
                                          string phone,
                                          string defaultshipping,
                                          string cardholder,
                                          string cardnumber,
                                          string expirationmonth,
                                          string expirationyear )
        {
            AddressViewData viewData = new AddressViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            if (Request.HttpMethod != "POST")
            {
                viewData.States = StateCodes;
                viewData.Countries = CountryCodes;
                viewData.Address = context.Address.Where(id);
                string [] parameters = { "Customer", "Address" };
                object [] values = { viewData.customer.CustomerNo, viewData.Address.AddressID };
                viewData.CreditCard = context.CreditCard.First(parameters, values);
                return View(viewData);
            }

            Address address = context.Address.Where(addressid);
            address.Customer = viewData.cookie;
            address.AddressID = addressid;
            address.FullName = fullname;
            address.Address1 = address1;
            address.Address2 = address2;
            address.Address3 = address3;
            address.City = city;
            address.State = state;
            address.Zip = zipcode;
            address.Country = country;
            address.Phone = phone;
            if (defaultshipping != null)
            {
                address.DefaultShipping = Int16.Parse(defaultshipping);
            }
            else
            {
                address.DefaultShipping = 0;
            }

            context.Address.Update(address);

            if (!string.IsNullOrEmpty(cardnumber))
            {
                CreditCard creditCard = new CreditCard();
                creditCard.CardID = getGuid();
                creditCard.Customer = viewData.cookie;
                creditCard.Address = addressid;
                creditCard.Cardholder = cardholder;
                creditCard.Number = cardnumber;
                creditCard.Expmonth = expirationmonth;
                creditCard.Expyear = expirationyear;
                context.CreditCard.Insert(creditCard);
            }

            return RedirectToAction("AddressBook/" + viewData.customer.CustomerNo, "Account");
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult RecentOrders(string id)
        {
            RecentOrdersViewData viewData = new RecentOrdersViewData ();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            if (string.IsNullOrEmpty(id))
                id = viewData.cookie;

            List<SalesOrder> orders = context.SalesOrder.Select("Customer", id);
            if (orders.Count > 0)
            {
                viewData.RecentOrders = new List<DetailedSalesOrder>();

                foreach (SalesOrder order in orders)
                {
                    DetailedSalesOrder detailed = new DetailedSalesOrder();
                    detailed.SalesOrder = order;
                    detailed.Items = context.SalesOrderItem.Select("SalesOrder", order.SalesOrderID);
                    viewData.RecentOrders.Add(detailed);
                }
            }
            return View(viewData);
        }


        [LogMethodCall]
        [Authorize]
        public ActionResult SalesOrder(string id)
        {
            ViewSalesOrderViewData viewData = new ViewSalesOrderViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            viewData.DetailedSalesOrder = new DetailedSalesOrder();
            viewData.DetailedSalesOrder.SalesOrder = context.SalesOrder.Where(id);
            if (viewData.DetailedSalesOrder.SalesOrder.Customer != viewData.customer.CustomerNo)
            {
                List<string> errors = new List<string>();
                errors.Add("Error invalid sales order id.");
                viewData.errors = errors;
                return View(viewData);
            }

            viewData.DetailedSalesOrder.Items = context.SalesOrderItem.Select("SalesOrder", id);
            Customer customer = context.Customer.Where(viewData.cookie);
            viewData.ShippingAddress = context.Address.First("GetAddress", "addressid", viewData.DetailedSalesOrder.SalesOrder.ShippingAddress);
            viewData.BillingAddress = context.Address.First("GetAddress", "addressid", viewData.DetailedSalesOrder.SalesOrder.BillingAddress);
            viewData.DetailedSalesOrder.SalesOrder.ShippingMethod = viewData.DetailedSalesOrder.SalesOrder.ShippingMethod;
            viewData.CreditCard = context.CreditCard.Where(viewData.DetailedSalesOrder.SalesOrder.CardID);
            viewData.CreditCard.Number = MaskCreditCard(viewData.CreditCard.Number);

            return View(viewData);
        }

        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
