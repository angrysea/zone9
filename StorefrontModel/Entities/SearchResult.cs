// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class SearchResult
	{
        public string SearchId { get; set; }
        public string CustomerNo { get; set; }
        public string Search { get; set; }
		public int ProductsFound { get; set; }
		public DateTime SearchTime { get; set; }
	}
}
