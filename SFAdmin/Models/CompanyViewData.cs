using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorefrontModel;

namespace SFAdmin.Models
{
    public class CompanyViewData : SFViewData
    {
        public List<SiteTheme> themes { get; set; }
        public List<ShippingMethod> shippingMethods { get; set; }
    }

    public class ThemeViewData : SFViewData
    {
    }

    public class CatalogViewData : SFViewData
    {
        public Catalog catalog { get; set; }
        public List<SiteTheme> themes { get; set; }
    }

    public class CatalogsViewData : SFViewData
    {
        public List<Catalog> catalogs { get; set; }
    }

    public class CarriersViewData : SFViewData
    {
        public List<Carrier> carriers { get; set; }
    }

    public class CarrierViewData : SFViewData
    {
        public Carrier carrier { get; set; }
    }

    public class ShippingMethodsViewData : SFViewData
    {
        public List<ShippingMethod> shippingMethods { get; set; }
    }

    public class ShippingMethodViewData : SFViewData
    {
        public ShippingMethod shippingMethod { get; set; }
        public List<Carrier> carriers { get; set; }
    }

    public class AvailabilitiesViewData : SFViewData
    {
        public List<Availability> availabilities { get; set; }
    }

    public class AvailabilityViewData : SFViewData
    {
        public Availability availability { get; set; }
    }

    public class LinksViewData : SFViewData
    {
        public List<Link> links { get; set; }
    }

    public class LinkViewData : SFViewData
    {
        public Link link { get; set; }
    }

    public class EMailListsViewData : SFViewData
    {
        public List<EMailList> emailLists { get; set; }
    }

    public class EMailListViewData : SFViewData
    {
        public EMailList emailList { get; set; }
    }
}
