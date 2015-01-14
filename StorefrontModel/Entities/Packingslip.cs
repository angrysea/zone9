// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Packingslip
	{
        public string PackingSlipId { get; set; }
		public string Customer { get; set; }
        public string ShippingAddress { get; set; }
        public string Invoice { get; set; }
		public string Shippingmethod { get; set; }
		public string TrackingNumber { get; set; }
		public string CarrierName { get; set; }
		public string Salescoupon { get; set; }
		public DateTime Creationdate { get; set; }
	}
}
