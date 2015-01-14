using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorefrontModel;

namespace Storefront.Models
{
    public class CheckoutViewData : SFViewData
    {
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public CreditCard CreditCard { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public List<ShippingMethod> ShippingMethods { get; set; }
        public List<Address> Addresses { get; set; }
        public List<CreditCard> creditCards { get; set; }
        public List<ShoppingCartListItem> shoppingCart { get; set; }
        public List<StateCode> States { get; set; }
        public List<CountryCode> Countries { get; set; }
    }


    public class SalesOrderViewData : SFViewData
    {
        public DetailedSalesOrder DetailedSalesOrder { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public CreditCard CreditCard { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public bool FailedAuthorization { get; set; }
    }

}