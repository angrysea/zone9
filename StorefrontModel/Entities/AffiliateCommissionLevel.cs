// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class AffiliateCommissionLevel
	{
		public string ID { get; set; }
		public int Program { get; set; }
		public string Description { get; set; }
		public string Linktype { get; set; }
		public double StartValue { get; set; }
		public double EndValue { get; set; }
		public double CommissionPercent { get; set; }
	}
}
