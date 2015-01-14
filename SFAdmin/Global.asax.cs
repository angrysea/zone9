using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;

namespace SFAdmin
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801


    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Services/{Service}.svc");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
            routes.MapRoute(
                "SortBy",                                              // Route name
                "{controller}/{action}/{sortby}",                           // URL with parameters
                new { controller = "Home", action = "Index", sortby = "" }  // Parameter defaults
            );
            routes.MapRoute(
                "SimilarProducts",
                "Products/SimilarProductsActions/{productno}/{manufacturer}",
                new
                {
                    controller = "Products",
                    action = "SimilarProductsActions",
                    productno = "",
                    manufacturer = "",
                }
            );
            routes.MapRoute(
                "SimilarActions",
                "Products/{action}/{productno}/{similarproductno}/{manufacturer}",
                new
                {
                    controller = "Products",
                    action = "AddSimilarProducts",
                    productno = "",
                    manufacturer = "",
                    similarproductno = ""
                }
            );
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                MDC.Set("user", HttpContext.Current.User.Identity.Name);
            }
        }
    }
}