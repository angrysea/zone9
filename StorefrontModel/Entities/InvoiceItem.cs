// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class InvoiceItem
	{
        public string Invoice { get; set; }
        public string SOItem { get; set; }
        public string Product { get; set; }
		public double UnitPrice { get; set; }
		public double TotalPrice { get; set; }
		public int Quantity { get; set; }
		public short GiftOption { get; set; }
	}
}
