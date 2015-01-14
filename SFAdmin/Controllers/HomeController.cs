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
    public class HomeController : StorefrontController
    {
        [LogMethodCall]
        public ActionResult Index()
        {
            SFViewData viewData = new SFViewData();
            AddMasterData(viewData);

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult About()
        {
            ViewData["Title"] = "About Page";

            return View();
        }

    }
}
