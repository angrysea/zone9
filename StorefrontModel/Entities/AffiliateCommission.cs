// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class AffiliateCommission
	{
		public string ID { get; set; }
		public int Affiliate { get; set; }
		public int Salesorderitem { get; set; }
		public double CommissionPercent { get; set; }
		public double Commission { get; set; }
		public DateTime Creationdate { get; set; }
		public DateTime Paiddate { get; set; }
	}
}
