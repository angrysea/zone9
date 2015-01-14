// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class CreditCard
	{
        public string CardID { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string Cardholder { get; set; }
        public string Number { get; set; }
		public string Expmonth { get; set; }
        public string Expyear { get; set; }
        public string CCV2 { get; set; }
	}
}
