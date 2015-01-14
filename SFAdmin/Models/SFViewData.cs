using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SFAdmin.Models;
using StorefrontModel;

namespace SFAdmin.Models
{
    public class SFViewData
    {
        public Company company { get; set; }
        public SiteTheme theme { get; set; }
        public string email { get; set; }
        public List<string> errors { get; set; }
        public bool update { get; set; }
        public string msg { get; set; }
        public string cookie { get; set; }
        public bool bLoggingIn { get; set; }
    }
}
