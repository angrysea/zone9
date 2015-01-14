// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Address
	{
        public string AddressID { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
        public short DefaultShipping { get; set; }
        public short DefaultBilling { get; set; }
    }
}
