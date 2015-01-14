// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Product
	{
        public string ProductNo { get; set; }
        public string GTIN { get; set; }
        public string Catalog { get; set; }
		public string Name { get; set; }
		public string Manufacturer { get; set; }
		public string Distributor { get; set; }
		public int Quantity { get; set; }
		public int Allocated { get; set; }
		public int Sold { get; set; }
		public int QuantityToOrder { get; set; }
		public int BackOrdered { get; set; }
		public int MinimumOnHand { get; set; }
		public int ReorderQuantity { get; set; }
		public int QuantityOnOrder { get; set; }
		public string Availability { get; set; }
		public double ListPrice { get; set; }
		public double OurPrice { get; set; }
		public double OurCost { get; set; }
		public int Rank { get; set; }
        public int Status { get; set; }
	}
}
