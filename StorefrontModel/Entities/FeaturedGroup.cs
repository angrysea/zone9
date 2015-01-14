// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class FeaturedGroup
	{
		public string Name { get; set; }
		public string Catalog { get; set; }
		public int Sortorder { get; set; }
		public string Type { get; set; }
		public string Heading { get; set; }
		public string Comments { get; set; }
		public int ArticleID { get; set; }
		public short Active { get; set; }
	}
}
