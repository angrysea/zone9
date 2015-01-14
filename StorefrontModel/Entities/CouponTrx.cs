// Title:        Company
// Copyright:    Copyright (c) 2008

using System;
using System.Collections.Generic;


namespace StorefrontModel
{
    public class CouponTrx
    {
        public string Code { get; set; }
        public string CouponProduct { get; set; }
        public string Product { get; set; }
        public string Customer { get; set; }
        public string SalesOrder { get; set; }
        public DateTime RedemptionDate { get; set; }
        public double Savings { get; set; }
    }
}
