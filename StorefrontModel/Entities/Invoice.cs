// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Invoice
	{
		public string InvoiceID { get; set; }
		public string Salesorder { get; set; }
		public string Customer { get; set; }
		public string Billing { get; set; }
		public double Totalcost { get; set; }
		public double Discount { get; set; }
		public string DiscountDescription { get; set; }
		public double Taxes { get; set; }
		public string TaxesDescription { get; set; }
		public double ShippingCost { get; set; }
		public double Handling { get; set; }
		public double Total { get; set; }
        public string PaymentMethod { get; set; }
		public string AuthorizationCode { get; set; }
		public string Status { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
