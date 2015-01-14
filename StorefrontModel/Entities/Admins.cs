// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Admins
	{
		public string Email { get; set; }
        public string Cookie { get; set; }
        public string Password { get; set; }
		public string PasswordQuestion { get; set; }
		public string PasswordAnswer { get; set; }
		public int Affiliate { get; set; }
		public DateTime? Refereddate { get; set; }
		public short Loggedin { get; set; }
		public int Lastsortorder { get; set; }
		public string ContinueShoppingURL { get; set; }
        public short Viewonlystatus { get; set; }
		public DateTime Creationdate { get; set; }
		public DateTime Logindate { get; set; }
		public DateTime Lastlogindate { get; set; }
		public string Comment { get; set; }
        public short IsApproved { get; set; }
		public DateTime LastActivityDate { get; set; }
		public DateTime LastPasswordChangedDate { get; set; }
		public short IsLockedOut { get; set; }
        public short IsOnline { get; set; }
		public int FailedPasswordAttemptCount { get; set; }
        public DateTime? FailedPasswordAttemptWindowStart { get; set; }
        public int FailedPasswordAnswerAttemptCount { get; set; }
        public DateTime? FailedPasswordAnswerAttemptWindowStart { get; set; }
        public DateTime? LastLockedOutDate { get; set; }
	}
}
