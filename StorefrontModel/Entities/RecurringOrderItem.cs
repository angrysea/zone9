// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class RecurringOrderItem
	{
		public int ID { get; set; }
		public int ProductNo { get; set; }
		public int RecurringOrderID { get; set; }
		public string TrxType { get; set; }
		public int Quantity { get; set; }
		public double Unitprice { get; set; }
		public string Status { get; set; }
		public short GiftOption { get; set; }
	}
}
