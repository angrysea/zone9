// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class CCTransaction
	{
		public string Trx { get; set; }
		public string Salesorder { get; set; }
		public int Result { get; set; }
		public string Pnref { get; set; }
        public string OrigPnref { get; set; }
		public string RespMSG { get; set; }
		public string Authcode { get; set; }
		public string Avsaddr { get; set; }
		public string Avszip { get; set; }
        public string Duplicate { get; set; }
        public string PreFpsMsg { get; set; }
        public string PostFpsMsg { get; set; }
        public string Status { get; set; }
        public string Errors { get; set; }
        public string VerboseMsg { get; set; }
    }
}
