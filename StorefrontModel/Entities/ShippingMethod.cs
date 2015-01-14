// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class ShippingMethod
	{
		public string Code { get; set; }
		public string Carrier { get; set; }
		public string Country { get; set; }
		public double FixedPrice { get; set; }
		public double FreeShippingAmount { get; set; }
		public string Description { get; set; }
		public string Notes { get; set; }
        public int SortOrder { get; set; }
        public short DefaultMethod { get; set; }
        public string ServiceCode { get; set; }
    }
}
