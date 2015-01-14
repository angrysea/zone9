using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorefrontModel
{
    public class DetailedInvoice
    {
        public Invoice Invoice { get; set; }
        public List<InvoiceItem> Items { get; set; }
    }
}
