// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Details
	{
		public string Product { get; set; }
		public string ManufacturerProduct { get; set; }
		public string DistributorProduct { get; set; }
		public string Description { get; set; }
		public string AltDescription { get; set; }
		public string ImageURLSmall { get; set; }
		public string ImageURLMedium { get; set; }
		public string ImageURLLarge { get; set; }
		public int SalesRank { get; set; }
		public string Availability { get; set; }
		public double ShippingWeight { get; set; }
		public double Height { get; set; }
		public double Length { get; set; }
		public double Width { get; set; }
		public double HandlingCharges { get; set; }
		public string URL { get; set; }
		public short HasVariations { get; set; }
	}
}
