// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Link
	{
		public string Url { get; set; }
		public string Category { get; set; }
		public string Header { get; set; }
		public string Description { get; set; }
		public string Email { get; set; }
		public int Emailssent { get; set; }
		public DateTime? Emailssentdate { get; set; }
		public short Linkback { get; set; }
        public string SiteName { get; set; }
        public string RecipicateURL { get; set; }
        public string WebMasterName { get; set; }
        public string Password { get; set; }
        public short Accepted  { get; set; }
	}
}
