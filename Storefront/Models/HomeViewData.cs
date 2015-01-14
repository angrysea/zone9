using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorefrontModel;

namespace Storefront.Models
{
    public class FeaturedGroupsViewData
    {
        public FeaturedGroup featuredGroup { get; set; }
        public List<ListProduct> listProducts { get; set; }
    }

    public class IndexViewData : SFViewData
    {
        public List<FeaturedGroupsViewData> featuredGroups { get; set; }
        public List<ListProduct> productRankings { get; set; }
        public List<ListProduct> recentlyViewed { get; set; }
    }

    public class ShoppingCartViewData : SFViewData
    {
        public List<ListProduct> recentlyViewed { get; set; }
        public List<ShoppingCartListItem> shoppingCart { get; set; }
    }

    public class CouponData
    {
        public Coupon coupon { get; set; }
        public Product product { get; set; }
        public Details details { get; set; }
        public Manufacturer manufacturer { get; set; }
    }

    public class CouponViewData : SFViewData
    {
        public List<CouponData> coupons { get; set; }
    }

    public class CreateAccountViewData : SFViewData
    {
        public List<CountryCode> countryCodes { get; set; }
    }

    public class GroupViewData : SFViewData
    {
        public List<Category> categories { get; set; }
    }

    public class JoinViewData : SFViewData
    {
        public bool Joined { get; set; }
        public string EMail { get; set; }
    }

}
