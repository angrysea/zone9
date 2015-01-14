using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Storefront.Provider
{
    public interface IFormsAuthentication
    {
        void SetAuthCookie(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationWrapper : IFormsAuthentication
    {
        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
