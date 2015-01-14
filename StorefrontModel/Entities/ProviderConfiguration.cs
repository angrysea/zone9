// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class ProviderConfiguration
	{
		public string ApplicationName { get; set; }
		public string Description { get; set; }
		public int MaxInvalidPasswordAttempts { get; set; }
		public int PasswordAttemptWindow { get; set; }
		public int MinRequiredNonAlphanumericCharacters { get; set; }
		public int MinRequiredPasswordLength { get; set; }
		public string PasswordStrengthRegularExpression { get; set; }
		public short EnablePasswordReset { get; set; }
		public short EnablePasswordRetrieval { get; set; }
		public short RequiresQuestionAndAnswer { get; set; }
		public short RequiresUniqueEmail { get; set; }
		public short WriteExceptionsToEventLog { get; set; }
		public string PasswordFormat { get; set; }
	}
}
