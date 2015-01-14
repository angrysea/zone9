// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class UserInfo
	{
		public string Customer { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public int Affiliate { get; set; }
		public DateTime Refereddate { get; set; }
		public string Name { get; set; }
		public short Loggedin { get; set; }
		public string Cookie { get; set; }
		public int Lastsortorder { get; set; }
		public string ContinueShoppingURL { get; set; }
		public int Viewonlystatus { get; set; }
		public DateTime Creationdate { get; set; }
		public DateTime Logindate { get; set; }
		public DateTime Lastlogindate { get; set; }
	}
}
