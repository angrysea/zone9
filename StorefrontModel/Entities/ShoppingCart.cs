// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class ShoppingCart
	{
		public string Cookie { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public double OurPrice { get; set; }
        public short GiftOption { get; set; }
		public DateTime AddedDate { get; set; }
		public string ZipCode { get; set; }
		public string Country { get; set; }
	}
}
