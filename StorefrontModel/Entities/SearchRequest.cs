// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
    public class SearchRequest
    {
        public string SearchId { get; set; }
        public string Description { get; set; }
        public string CustomerNo { get; set; }
        public string Categories { get; set; }
        public string Catalog { get; set; }
        public string SearchPhrase { get; set; }
        public string Manufacturer { get; set; }
        public double StartPrice { get; set; }
        public double EndPrice { get; set; }
        public string SortField { get; set; }
        public short LandingPage { get; set; }
        public short ViewOnlyAvailable { get; set; }
        public int Level { get; set; }
        public DateTime SearchTime { get; set; }
    }
}
