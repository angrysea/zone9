// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class WebStat
	{
		public string Customer { get; set; }
		public string Remotehost { get; set; }
		public string Sourceurl { get; set; }
		public string Referer { get; set; }
		public DateTime Hitdate { get; set; }
	}
}
