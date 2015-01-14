// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class MailConfiguration
	{
		public string MailHost { get; set; }
		public string MailFromName { get; set; }
		public string MailFromAddress { get; set; }
		public string MailAuthUser { get; set; }
		public string MailAuthPassword { get; set; }
		public string MailToRecipients { get; set; }
		public string MailToListWarning { get; set; }
		public string MailToListError { get; set; }
		public string MailReplyToList { get; set; }
		public string MailURL { get; set; }
		public string MailIntroduction { get; set; }
		public string MailBody { get; set; }
		public string MailSignature { get; set; }
		public string MailSubject { get; set; }
	}
}
