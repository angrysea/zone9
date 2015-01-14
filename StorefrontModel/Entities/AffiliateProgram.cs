// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class AffiliateProgram
	{
		public string Name { get; set; }
		public int LinkType { get; set; }
		public string Description { get; set; }
		public double Minpercent { get; set; }
		public double Maxpercent { get; set; }
	}
}
