// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
    public class SiteMapEntry
	{
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
		public string Roles { get; set; }
		public int Parent { get; set; }
    }
}
