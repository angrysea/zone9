// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class WishListItem
	{
		public string Code { get; set; }
		public int QuanityDesired { get; set; }
		public string Comment { get; set; }
		public int Priority { get; set; }
		public short GiftOption { get; set; }
		public DateTime AddedDate { get; set; }
	}
}
