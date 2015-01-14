// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class CheckoutTrx
	{
        public string Customer { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public string CreditCard { get; set; }
        public string ShippingMethod { get; set; }
        public double ShippingCost { get; set; }
        public double Tax { get; set; }
    }
}
