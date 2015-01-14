// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Customer
	{
		public string CustomerNo { get; set; }
		public string Fullname { get; set; }
		public string HomePhone { get; set; }
		public string MobilPhone { get; set; }
		public string WorkPhone { get; set; }
		public string Fax { get; set; }
		public string Email1 { get; set; }
		public string Email2 { get; set; }
		public string Email3 { get; set; }
		public string Birthdaymonth { get; set; }
		public string Birthdayyear { get; set; }
		public string URL { get; set; }
		public short CCflag { get; set; }
        public short OptOut { get; set; }
        public short QuickClick { get; set; }
        public short ViewOnlyAvailability { get; set; }
    }
}
