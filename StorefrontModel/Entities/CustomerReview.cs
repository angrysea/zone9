// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class CustomerReview
	{
		public string Customer { get; set; }
		public string Rating { get; set; }
		public DateTime ReviewDate { get; set; }
		public string Summary { get; set; }
		public string Comment { get; set; }
	}
}
