using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorefrontModel
{
    public class DetailedSalesOrder
    {
        public SalesOrder SalesOrder { get; set; }
        public List<SalesOrderItem> Items { get; set; }
    }
}
