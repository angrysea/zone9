// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Coupon
	{
		public string Code { get; set; }
		public string Product { get; set; }
		public string Manufacturer { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		public int QuantityLimit { get; set; }
		public int QuantityRequired { get; set; }
		public double PriceMinimum { get; set; }
		public double Discount { get; set; }
		public int DiscountType { get; set; }
		public short Precludes { get; set; }
		public short OneperHousehold { get; set; }
		public short SingleUse { get; set; }
		public short Display { get; set; }
		public string Imageurl { get; set; }
		public int Redemptions { get; set; }
		public DateTime ExpirationDate { get; set; }
	}
}
