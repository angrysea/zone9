using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorefrontModel
{
    public class DetailedPackingslip
    {
        public Packingslip Packingslip { get; set; }
        public List<PackingslipItem> Items { get; set; }
    }
}
