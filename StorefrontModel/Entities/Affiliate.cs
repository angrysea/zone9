// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Affiliate
	{
		public string Name { get; set; }
		public string First { get; set; }
		public string Last { get; set; }
		public string Company { get; set; }
		public string Payeename { get; set; }
		public string TaxID { get; set; }
		public string Description { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string Address3 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
		public string Fax { get; set; }
		public string Contact { get; set; }
		public string Email { get; set; }
		public string Websitename { get; set; }
		public string Affiliateurl { get; set; }
		public string PassWord { get; set; }
		public int PlanID { get; set; }
		public int Productssold { get; set; }
		public double Totalsales { get; set; }
		public double Totalcommission { get; set; }
		public DateTime Creationdate { get; set; }
	}
}
