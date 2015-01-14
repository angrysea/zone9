// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class RecurringOrder
	{
		public int ID { get; set; }
		public string Description { get; set; }
		public int Customer { get; set; }
		public int Billingaddress { get; set; }
		public int Shippingaddress { get; set; }
		public int Shippingmethod { get; set; }
		public short Optimizeshipping { get; set; }
		public int Frequency { get; set; }
		public DateTime Startdate { get; set; }
		public DateTime Enddate { get; set; }
		public DateTime Creationdate { get; set; }
	}
}
