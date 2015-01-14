// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class PayFlowPro
	{
		public string HostAddress { get; set; }
		public int HostPort { get; set; }
		public string PartnerID { get; set; }
		public int Timeout { get; set; }
		public string Vendor { get; set; }
		public string Logon { get; set; }
		public string Password { get; set; }
		public string ProxyAddress { get; set; }
		public int ProxyPort { get; set; }
		public string ProxyLogon { get; set; }
		public string ProxyPassword { get; set; }
	}
}
