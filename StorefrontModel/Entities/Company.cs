// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Company
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string Address3 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Country { get; set; }
		public string Phone { get; set; }
		public string CustomerService { get; set; }
		public string Support { get; set; }
		public string Fax { get; set; }
		public string EMail1 { get; set; }
		public string EMail2 { get; set; }
		public string EMail3 { get; set; }
		public string DefaultShipping { get; set; }
        public int FreeShipping { get; set; }
        public double FreeShippingMin { get; set; }
        public string BaseURL { get; set; }
		public string BaseSecureURL { get; set; }
		public string BaseAffiliateURL { get; set; }
		public int CurrentAffiliatePlan { get; set; }
		public string CompanyURL { get; set; }
		public string LinkExchangeText { get; set; }
		public string Sitemap { get; set; }
		public string WebStatID { get; set; }
		public string GoogleConversionID { get; set; }
		public string OvertureConversionID { get; set; }
		public string ShoppingConversionID { get; set; }
		public byte UseModRewrite { get; set; }
		public byte SiteSeal { get; set; }
		public string SalesOrderCoupon { get; set; }
		public string Theme { get; set; }
		public string Keyword { get; set; }
		public string Keyword1 { get; set; }
		public string Keyword2 { get; set; }
		public string Keyword3 { get; set; }
		public string Keyword4 { get; set; }
		public string Keyword5 { get; set; }
		public int InStockOnly { get; set; }
		public string PassWord { get; set; }
        public int Active { get; set; }
    }
}
