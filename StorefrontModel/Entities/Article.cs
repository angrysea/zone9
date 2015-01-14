// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Article
	{
		public string Name { get; set; }
		public string URL { get; set; }
		public int Sortorder { get; set; }
		public string Heading { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
        public string Product { get; set; }
        public string Author { get; set; }
    }
}
