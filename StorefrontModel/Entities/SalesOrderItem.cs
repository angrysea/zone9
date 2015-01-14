// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class SalesOrderItem
	{
        public string SOItem { get; set; }
        public string SalesOrder { get; set; }
		public string ProductNo { get; set; }
		public string TrxType { get; set; }
		public string GTIN { get; set; }
		public string ProductName { get; set; }
		public string Manufacturer { get; set; }
		public string Distributor { get; set; }
		public int Quantity { get; set; }
		public int Shipped { get; set; }
		public int QuantityToShip { get; set; }
		public double Unitprice { get; set; }
		public double Shippingweight { get; set; }
		public double Handlingcharges { get; set; }
		public string Status { get; set; }
		public string Availability { get; set; }
		public string ExchangeID { get; set; }
		public short GiftOption { get; set; }
		public string Zipcode { get; set; }
		public string Country { get; set; }
	}
}
