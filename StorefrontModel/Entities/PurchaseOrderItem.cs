// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class PurchaseOrderItem
	{
        public string PurchaseOrder { get; set; }
        public string Product { get; set; }
		public string Trxtype { get; set; }
		public string GTIN { get; set; }
		public string ProductName { get; set; }
		public string Manufacturer { get; set; }
		public int Quantity { get; set; }
		public int Quantityreceived { get; set; }
		public int Receiving { get; set; }
		public double Ourcost { get; set; }
		public double Shippingweight { get; set; }
		public string Status { get; set; }
	}
}
