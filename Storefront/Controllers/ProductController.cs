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
    public class ProductController : StorefrontController
    {
        [LogMethodCall]
        public ActionResult Index(string id)
        {
            DetailsViewData viewData = new DetailsViewData();
            AddMasterData(viewData);

            viewData.product = context.Product.Where(id);
            viewData.details = context.Details.Where(id); ;
            viewData.availability = context.Availability.Where(viewData.product.Availability);
            viewData.productSpecifications = context.ProductSpecification.Select("Product", id);
            viewData.manufacturer = context.Manufacturer.Where(viewData.product.Manufacturer);
            viewData.similarProducts = context.ListProduct.Execute("GetSimularProductList", "product", id);

            string [] parameters = { "cookie", "ignoreProduct" };
            object [] values = { viewData.cookie, id };
            viewData.recentlyViewed = context.ListProduct.Execute("GetRecentlyViewedIgnore", parameters, values);

            string [] parameters2 = { "cookie", "productno" };
            context.RecentlyViewed.ExecuteProc("UpdateRecentlyViewedProduct", parameters2, values);

            ProductRanking productRanking = context.ProductRanking.Where(id);
            if (productRanking == null)
            {
                productRanking = new ProductRanking();
                productRanking.Product = id;
                productRanking.Sold = 0;
                productRanking.Views = 1;
                context.ProductRanking.Insert(productRanking);
            }
            else
            {
                productRanking.Views++;
                context.ProductRanking.Update();
            }

            return View(viewData);
        }
    }
}