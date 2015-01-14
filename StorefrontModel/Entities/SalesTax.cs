// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class SalesTax
	{
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string SellToCity { get; set; }
        public string SellToState { get; set; }
        public string SellToCountry { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
		public double TaxRate { get; set; }
	}
}
