// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
    public class SearchRanking
    {
        public string Type { get; set; }
        public string Search { get; set; }
        public int Count { get; set; }
        public DateTime SearchTime { get; set; }
    }
}
