using System;
using System.Collections.Generic;
using System.Text;
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
    public class HomeController : StorefrontController
    {
        [LogMethodCall]
        public ActionResult Index()
        {
            IndexViewData viewData = new IndexViewData();
            AddMasterData(viewData);

            List<FeaturedGroup> featuredGroupList = context.FeaturedGroup.Select(" order by Sortorder", true);
            viewData.featuredGroups = new List<FeaturedGroupsViewData>();
            foreach (FeaturedGroup featuredGroup in featuredGroupList)
            {
                FeaturedGroupsViewData featuredGroupsViewData = new FeaturedGroupsViewData();
                featuredGroupsViewData.featuredGroup = featuredGroup;
                featuredGroupsViewData.listProducts = 
                    context.ListProduct.Execute("GetFeaturedProductList", "name", featuredGroup.Name);
                viewData.featuredGroups.Add(featuredGroupsViewData); 
            }
            viewData.productRankings = context.ListProduct.Execute("GetProductRankingByViewed");
            viewData.recentlyViewed = 
                context.ListProduct.Execute("GetRecentlyViewedProducts", "cookie", viewData.cookie);
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult About()
        {
            SFViewData viewData = new SFViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);
            return View();
        }

        [LogMethodCall]
        public ActionResult Contactus()
        {
            SFViewData viewData = new SFViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);
        
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult PrivacySecurity()
        {
            SFViewData viewData = new SFViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult ReturnPolicy()
        {
            SFViewData viewData = new SFViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Coupons()
        {
            CouponViewData viewData = new CouponViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            viewData.coupons = new List<CouponData>();

            List<Coupon> coupons = context.Coupon.Select("Display", "1");
            foreach (Coupon coupon in coupons)
            {
                CouponData couponData = new CouponData();
                couponData.coupon = coupon;
                if (coupon.Product != null && coupon.Product.Length > 0)
                {
                    couponData.product = context.Product.Where(coupon.Product);
                    couponData.details = context.Details.Where(coupon.Product);
                }

                if (coupon.Manufacturer!=null && coupon.Manufacturer.Length > 0)
                {
                    couponData.manufacturer =
                        context.Manufacturer.Where(coupon.Manufacturer);
                }

                viewData.coupons.Add(couponData);
            }

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult ShoppingCart()
        {
            ShoppingCartViewData viewData = new ShoppingCartViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);
            context.CheckoutTrx.Delete(viewData.cookie);
            viewData.shoppingCart = 
                context.ShoppingCartListItem.Execute("GetShoppingCart", 
                                                        "cookie", 
                                                        viewData.cookie);
            viewData.recentlyViewed =
                context.ListProduct.Execute("GetRecentlyViewedProducts", "cookie", viewData.cookie);
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult AddToCart(string product)
        {
            string cookie = GetStorefrontCookie(Request, Response);
            string [] parameters = { "cookie", "cartitem", "product" };
            object [] values = { cookie, getGuid(), product };
            context.ShoppingCart.ExecuteProc("AddToCart", parameters, values);
            return RedirectToAction("ShoppingCart", "Home");
        }

        [LogMethodCall]
        public ActionResult RemoveFromCart(string id)
        {
            if (id != null)
            {
                string cookie = GetStorefrontCookie(Request);
                if (cookie != null)
                {
                    string [] parameters = { "cookie", "product" };
                    object [] values = { cookie, id };
                    context.ShoppingCart.ExecuteProc("RemoveFromCart", parameters, values);
                }
            }
            return RedirectToAction("ShoppingCart", "Home");
        }

        [LogMethodCall]
        public ActionResult Brands()
        {
            SFViewData viewData = new SFViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult ShippingRates()
        {
            SFViewData viewData = new SFViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Group(string id)
        {
            GroupViewData viewData = new GroupViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);
            viewData.categories = context.Category.Select("GroupType", id);
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult UpdateQuantity(string cookie)
        {
            List<string> products = new List<string>();
            string [] parameters = {"cookie", "product", "quantity"};
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.StartsWith("qty_"))
                {
                    int qty = int.Parse(Request.Form[key]);
                    string product = key.Substring(4);
                    int orgQty = int.Parse(Request.Form[product]);
                    if (qty != orgQty)
                    {
                        object [] values = {cookie, product, qty.ToString() };
                        context.ShoppingCart.ExecuteProc("UpdateCartQty", parameters, values);
                    }
                }
            }
            return RedirectToAction("ShoppingCart", "Home");
        }

        [LogMethodCall]
        public ActionResult Search(string user, string cookie, string search)
        {
            SearchProcessor processor = new SearchProcessor(context);
            SearchRequest request = new SearchRequest();
            StringBuilder builder = new StringBuilder("Index/");

            request.SearchId = getGuid();
            request.LandingPage = 0;
            request.SearchPhrase = search.Trim();
            request.SearchTime = DateTime.Now;
            processor.SearchItems(request);
            context.SearchRequest.Insert(request);
            builder.Append(request.SearchId);
            builder.Append("/1");

            return RedirectToAction(builder.ToString(), "Search");
        }

        [LogMethodCall]
        public ActionResult JoinOurList(string cookie, string emailaddress, string opt)
        {
            JoinViewData viewData = new JoinViewData();
            viewData.bBreadcrumbs = true;
            AddMasterData(viewData);

            viewData.EMail = emailaddress;
            viewData.Joined = false;
            EMailList emailList = context.EMailList.Where(emailaddress);

            if (emailList == null ||
                    emailList.Email == null ||
                        emailList.Email.Length == 0)
            {
                emailList = new EMailList();
                emailList.Email = emailaddress;
                emailList.Optout = Int16.Parse(opt);
                emailList.Creationdate = System.DateTime.Now;
                context.EMailList.Insert(emailList);
                viewData.Joined = true;
            }
            else
            {
                emailList.Optout = Int16.Parse(opt);
                context.EMailList.Update();
            }

            return View(viewData);
        }
    }
}
