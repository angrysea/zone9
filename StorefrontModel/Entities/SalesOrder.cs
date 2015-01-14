// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class SalesOrder
	{
		public string SalesOrderID { get; set; }
        public string Customer { get; set; }
        public string CardID { get; set; }
        public int Referencenumber { get; set; }
		public string Description { get; set; }
		public string Couponcode { get; set; }
		public string Giftcertificate { get; set; }
		public double TotalCost { get; set; }
		public double Discount { get; set; }
		public string DiscountDescription { get; set; }
		public double Taxes { get; set; }
		public string TaxesDescription { get; set; }
		public double Shipping { get; set; }
		public double ShippingWeight { get; set; }
		public double Handling { get; set; }
		public short OptimizeShipping { get; set; }
		public short DropShipped { get; set; }
		public double Total { get; set; }
		public string Status { get; set; }
		public string Pnref { get; set; }
		public string Authorizationcode { get; set; }
		public int Affiliate { get; set; }
		public string Emailstatus { get; set; }
		public string Salescoupon { get; set; }
		public DateTime Creationdate { get; set; }
		public string BillingAddress { get; set; }
		public string ShippingAddress { get; set; }
		public string ShippingMethod { get; set; }
	}
}
