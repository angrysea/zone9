// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class GroupType
	{
        public string Name { get; set; }
        public string Type { get; set; }
        public string Catalog { get; set; }
		public int Sortorder { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
        public int Level { get; set; }
    }
}
