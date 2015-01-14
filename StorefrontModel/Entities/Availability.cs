// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Availability
	{
		public string Code { get; set; }
		public string Description { get; set; }
		public string Priority { get; set; }
        public int ExpectedWait { get; set; }
	}
}
