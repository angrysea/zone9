// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
	public class Task
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }
		public string URL { get; set; }
		public string Frequency { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime LastExecutionDate { get; set; }
	}
}
