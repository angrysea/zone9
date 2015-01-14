using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFAdmin.Provider
{
    class ProviderException : Exception
    {
        public ProviderException(string msg)
            : base(msg)
        {
        }
    }
}
