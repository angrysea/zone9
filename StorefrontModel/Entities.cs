using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace StorefrontModel
{
    public partial class Entities
    {
        protected string connectionString;
        protected DbProviderFactory df;

        public DbProviderFactory dbFactory
        {
            get
            {
                return df;
            }
        }

        public DbConnection getConnection()
        {
            DbConnection con = df.CreateConnection();
            con.ConnectionString = connectionString;
            con.Open();
            return con;
        }

        public string getGuid()
        {
            return System.Guid.NewGuid().ToString("N").ToUpper();
        }

    }
}
