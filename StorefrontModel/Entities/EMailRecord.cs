// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class EMailRecord
	{
		public string Email { get; set; }
		public DateTime SentDate { get; set; }
		public int ReferenceNo { get; set; }
		public string Type { get; set; }
		public string Recipient { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
	}
}
