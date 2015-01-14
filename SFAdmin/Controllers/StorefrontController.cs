using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using SFAdmin.Models;
using StorefrontModel;

namespace SFAdmin.Controllers
{
    public class StorefrontController : Controller
    {
        protected StorefrontEntities context;
        static private string strCookieName = "STOREFRONTCOOKIE";

        public StorefrontController()
        {
            context = new StorefrontEntities();
        }

        public void AddMasterData(SFViewData viewData) 
        {
            viewData.company = (Company)Session["company"];
            viewData.theme = (SiteTheme)Session["theme"];
            viewData.email = User.Identity.Name;
            if (viewData.company == null)
            {
                viewData.company = context.Company.First();
                viewData.theme = context.SiteTheme.Where(viewData.company.Theme);
                Session["company"] = viewData.company;
                Session["theme"] = viewData.theme;
            }
            viewData.cookie = GetStorefrontCookie(Request, Response);
            viewData.bLoggingIn = false;
        }

        static public string GetStorefrontCookie(HttpRequestBase Request, HttpResponseBase Response)
        {
            HttpCookie cookie = Request.Cookies[strCookieName];
            if (cookie != null)
            {
                return cookie.Value.ToString();
            }
            return SetStorefrontCookie(Response);
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

        static public string getGuid()
        {
            return System.Guid.NewGuid().ToString("N");
        }

        protected string MaskCreditCard(string cardNumber)
        {
            int len = cardNumber.Length;
            string cc = new string('X', len - 4);
            cardNumber = cc + cardNumber.Substring(len - 4);
            return cardNumber;
        }

    }
}
