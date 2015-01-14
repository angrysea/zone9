// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class GiftCertificate
	{
		public string Code { get; set; }
		public string Description { get; set; }
		public double Amount { get; set; }
		public short Display { get; set; }
		public string ImageURL { get; set; }
		public int Balance { get; set; }
		public int Redemptions { get; set; }
		public DateTime ExpirationDate { get; set; }
	}
}
