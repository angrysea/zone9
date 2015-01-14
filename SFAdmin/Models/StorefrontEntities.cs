using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.Common;
using StorefrontModel;

namespace SFAdmin.Models
{
    public partial class StorefrontEntities : StorefrontModel.Entities
    {
        public StorefrontEntities()
        {
            string dp =
              WebConfigurationManager.AppSettings["provider"];
            connectionString =
              WebConfigurationManager.ConnectionStrings["storefrontConnectionString"].ConnectionString;
            df = DbProviderFactories.GetFactory(dp);
        }
    }
}
