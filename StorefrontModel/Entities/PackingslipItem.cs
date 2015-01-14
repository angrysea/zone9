// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class PackingslipItem
	{
		public string PackingSlip { get; set; }
        public string SalesOrder { get; set; }
        public string SOItem { get; set; }
        public string ProductNo { get; set; }
        public string GTIN { get; set; }
        public int Quantity { get; set; }
		public string ProductName { get; set; }
		public double ShippingWeight { get; set; }
	}
}
