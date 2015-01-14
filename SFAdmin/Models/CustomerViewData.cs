using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorefrontModel;

namespace SFAdmin.Models
{
    public class CustomerViewData : SFViewData
    {
        public Customer customer { get; set; }
        public Users user { get; set; }
        public Address billingaddress { get; set; }
        public Address shippingaddress { get; set; }
        public CreditCard creditCard { get; set; }
        public List<SalesOrder> salesOrders { get; set; }
        public int productsInCart { get; set; }
        public char mode { get; set; }
    }

    public class ViewCustomer
    {
        public Customer customer { get; set; }
        public Users user { get; set; }
    }

    public class ViewUser
    {
        public Users user { get; set; }
        public int productsInCart { get; set; }
    }

    public class UsersViewData : SFViewData
    {
        public List<ViewUser> users { get; set; }
    }

    public class ShoppingCartViewData : SFViewData
    {
        public string UserID { get; set; }
        public List<ShoppingCartListItem> shoppingCart { get; set; }
    }

    public class CustomersViewData : SFViewData
    {
        public List<ViewCustomer> customers { get; set; }
        public char mode { get; set; }
    }

    public class CouponsViewData : SFViewData
    {
        public List<Coupon> coupons { get; set; }
    }

    public class CouponViewData : SFViewData
    {
        public Coupon coupon { get; set; }
        public List<Manufacturer> manufacturers { get; set; }
        public List<Category> categories { get; set; }
    }

    public class SendCouponViewData : SFViewData
    {
        public Coupon coupon { get; set; }
    }

    public class SalesOrderItemView
    {
        public SalesOrderItem salesOrderItem { get; set; }
        public Product product { get; set; }
    }

    public class SalesOrderView
    {
        public SalesOrder salesOrder { get; set; }
        public Customer customer { get; set; }
        public List<SalesOrderItemView> salesOrderItemsView { get; set; }
    }

    public class SalesOrdersViewData : SFViewData
    {
        public List<SalesOrderView> salesOrders { get; set; }
    }

    public class SalesOrderViewData : SFViewData
    {
        public DetailedSalesOrder DetailedSalesOrder { get; set; }
        public Customer Customer { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public CreditCard CreditCard { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public List<Distributor> distributors { get; set; }
        public int noteCount { get; set; }
    }

    public class InvoiceView
    {
        public Invoice invoice { get; set; }
        public Customer customer { get; set; }
    }

    public class InvoicesViewData : SFViewData
    {
        public List<InvoiceView> invoices { get; set; }
    }

    public class InvoiceViewData : SFViewData
    {
        public Invoice invoice { get; set; }
        public Customer customer { get; set; }
        public Address billing { get; set; }
        public CreditCard creditCard { get; set; }
        public List<InvoiceItem> invoiceItems { get; set; }
    }

    public class NotesViewData 
    {
        public string ReferenceNo { get; set; }
        public List<Note> Notes { get; set; }
    }
}