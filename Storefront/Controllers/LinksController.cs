using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Storefront.Models;
using Storefront.Aspects;
using StorefrontModel;

namespace Storefront.Controllers
{
    [HandleError]
    public class LinksController : StorefrontController
    {
        [LogMethodCall]
        public ActionResult Category(string id)
        {
            LinksViewData viewData = new LinksViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);
            viewData.category = id;
            viewData.links = context.Link.Select("Category", id);
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Links()
        {
            LinkCategoriesViewData viewData = new LinkCategoriesViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            viewData.categories = context.LinkCategory.Select();

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Signup( string sitename,
                                    string URL,
                                    string description,
                                    string reciprocate,
                                    string webmaster,
                                    string webmasteremail,
                                    string password,
                                    string category )
        {
            AddLinkViewData viewData = new AddLinkViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            viewData.categories = context.LinkCategory.Select();
            if (Request.HttpMethod != "POST")
            {
                return View(viewData);
            }

            Link link = new Link();
	        link.Url = URL;
            link.Category = category;
	        link.Description = description;
	        link.Email = webmasteremail;
	        link.Emailssent = 0;
	        link.Linkback = 0;
            link.SiteName = sitename;
            link.RecipicateURL = reciprocate;
            link.WebMasterName = webmaster;
            link.Password = password;
            link.Accepted = 0;

            context.Link.Insert(link);

            return RedirectToAction("Links", "Links");
        }
    }
}