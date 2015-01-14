using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFAdmin.Models
{
    public class AccountViewData : SFViewData
    {
        public int PasswordLength { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
    }
}
