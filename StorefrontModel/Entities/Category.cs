// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Category
	{
		public string Name { get; set; }
		public string GroupType { get; set; }
		public int SortOrder { get; set; }
		public string URL { get; set; }
		public short Active { get; set; }
		public string LongName { get; set; }
		public double StartPrice { get; set; }
		public double EndPrice { get; set; }
        public string Description { get; set; }
        public string Parent { get; set; }
        public string Logo { get; set; }
    }
}
