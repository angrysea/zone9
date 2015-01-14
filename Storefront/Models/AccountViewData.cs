using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorefrontModel;

namespace Storefront.Models
{
    public class AccountViewData : SFViewData
    {
        public int PasswordLength { get; set; }
        public string EMail { get; set; }
    }

    public class AddressViewData : SFViewData
    {
        public Address Address { get; set; }
        public CreditCard CreditCard { get; set; }
        public List<StateCode> States { get; set; }
        public List<CountryCode> Countries { get; set; }
    }

    public class CreditCardViewData : SFViewData
    {
        public CreditCard CreditCard { get; set; }
        public Address Address { get; set; }
        public List<Address> Addresses { get; set; }
        public List<StateCode> States { get; set; }
        public List<CountryCode> Countries { get; set; }
    }

    public class AddressBookEntries
    {
        public Address Address { get; set; }
        public CreditCard CreditCard { get; set; }
    }

    public class AddressBookViewData : SFViewData
    {
        public List<AddressBookEntries> AddressBook { get; set; }
    }

    public class RecentOrdersViewData : SFViewData
    {
        public List<DetailedSalesOrder> RecentOrders { get; set; }
    }

    public class ViewSalesOrderViewData : SFViewData
    {
        public DetailedSalesOrder DetailedSalesOrder { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public CreditCard CreditCard { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
    }

}