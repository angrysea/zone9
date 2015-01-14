// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class PurchaseOrder
	{
		public string ID { get; set; }
		public int ReferenceNumber { get; set; }
		public string Description { get; set; }
		public double TotalCost { get; set; }
		public short Dropship { get; set; }
		public double Shipping { get; set; }
		public double ShippingWeight { get; set; }
		public double Handling { get; set; }
		public double Total { get; set; }
		public string Status { get; set; }
		public string TrackingNumber { get; set; }
		public string EmailStatus { get; set; }
		public DateTime CreationDate { get; set; }
		public string Distributor { get; set; }
		public string ShippingMethod { get; set; }
	}
}
