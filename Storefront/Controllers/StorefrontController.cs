using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Storefront.Models;
using StorefrontModel;

namespace Storefront.Controllers
{
    public class StorefrontController : Controller
    {
        static private string strCookieName = "STOREFRONTCOOKIE";
        protected StorefrontEntities context;

        public StorefrontController()
        {
            context = new StorefrontEntities();
        }

        public void AddMasterData(SFViewData viewData)
        {
            viewData.company = (Company)Session["company"];
            viewData.theme = (SiteTheme)Session["theme"];

            if (viewData.company == null)
            {
                viewData.company = context.Company.First("Active", "1");
                viewData.theme = context.SiteTheme.Where(viewData.company.Theme);
                Session["company"] = viewData.company;
                Session["theme"] = viewData.theme;
            }

            viewData.manufacturers = context.Manufacturer.Select("Active", "1");
            viewData.bBrandMenu = true;
            if (!viewData.bInSearch)
            {
                viewData.groups = new List<ProductGroup>();
                foreach (GroupType groupType in
                    context.GroupType.Execute("GetGroups", "level", 1))
                {
                    ProductGroup group = new ProductGroup();
                    group.grouptype = groupType;
                    List<Category> catagories = context.Category.Select("GroupType", groupType.Name);
                    foreach (Category category in catagories)
                    {
                        group.catagories.Add(category);
                    }
                    viewData.groups.Add(group);
                }
            }

            viewData.productsInCart = 0;
            viewData.cookie = GetStorefrontCookie(Request, Response);
            if (viewData.cookie != null)
            {
                ItemsInCart itemsInCart = context.ItemsInCart.First("ItemsInCart", "cookie", viewData.cookie);
                if (itemsInCart != null)
                {
                    viewData.productsInCart = itemsInCart.productCount;
                }

                viewData.user = context.Users.First("GetUsersByCookie", "cookie", viewData.cookie);
                if (viewData.user != null)
                {
                    viewData.customer =
                        context.Customer.Where(viewData.cookie);
                }
            }
        }

        static public string GetStorefrontCookie(HttpRequestBase Request, HttpResponseBase Response)
        {
            string cookie = null;
            if (Request.Browser.Cookies)
            {
                HttpCookie httpCookie = Request.Cookies[strCookieName];
                if (httpCookie != null)
                {
                    cookie = httpCookie.Value.ToString();
                }
                else
                {
                    cookie = SetStorefrontCookie(Response);
                }
            }

            return cookie;
        }

        static public string GetStorefrontCookie(HttpRequestBase Request)
        {
            HttpCookie cookie = Request.Cookies[strCookieName];
            if (cookie != null)
            {
                return cookie.Value.ToString();
            }
            return null;
        }

        static public string SetStorefrontCookie(HttpResponseBase Response)
        {
            string id = getGuid();
            HttpCookie cookie = new HttpCookie(strCookieName);
            cookie.Value = id;
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.AppendCookie(cookie);
            return id;
        }

        static public string SetStorefrontCookie(string id, HttpResponseBase Response)
        {
            HttpCookie cookie = new HttpCookie(strCookieName);
            cookie.Value = id;
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.AppendCookie(cookie);
            return id;
        }

        static public string getGuid()
        {
            return System.Guid.NewGuid().ToString("N").ToUpper();
        }

        protected string MaskCreditCard(string cardNumber)
        {
            int len = cardNumber.Length;
            string cc = new string('X', len - 4);
            cardNumber = cc + cardNumber.Substring(len - 4);
            return cardNumber;
        }

        private static List<StateCode> stateCodes = null;
        public static List<StateCode> StateCodes
        {
            get
            {
                if (stateCodes == null)
                {
                    StorefrontEntities storefrontEntities = new StorefrontEntities();
                    stateCodes = storefrontEntities.StateCode.Select();
                }
                return stateCodes;
            }
        }

        private static List<CountryCode> countryCodes = null;
        public static List<CountryCode> CountryCodes
        {
            get
            {
                if (countryCodes == null)
                {
                    StorefrontEntities storefrontEntities = new StorefrontEntities();
                    countryCodes = storefrontEntities.CountryCode.Select();
                }
                return countryCodes;
            }
        }
    }
}
