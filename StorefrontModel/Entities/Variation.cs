// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Variation
	{
		public string Product { get; set; }
		public int Quantity { get; set; }
		public string ClothingSize { get; set; }
		public string ClothingColor { get; set; }
		public string Fabric { get; set; }
		public double Price { get; set; }
		public double SalePrice { get; set; }
		public string ShipDate { get; set; }
		public string Availability { get; set; }
		public string MultiMerchant { get; set; }
		public string MerchantSKU { get; set; }
		public string ImageUrl1 { get; set; }
		public string ImageUrl2 { get; set; }
		public string ImageUrl3 { get; set; }
		public string ImageUrl4 { get; set; }
	}
}
