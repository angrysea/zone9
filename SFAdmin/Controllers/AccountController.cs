using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using SFAdmin.Models;
using SFAdmin.Provider;
using SFAdmin.Aspects;

namespace SFAdmin.Controllers
{
    [HandleError]
    [OutputCache(Location = OutputCacheLocation.None)]
    public class AccountController : StorefrontController
    {
        public AccountController()
            : this(null, null)
        {
        }

        public AccountController(IFormsAuthentication formsAuth, StorefrontProvider provider)
        {
            FormsAuth = formsAuth ?? new FormsAuthenticationWrapper();
            Provider = provider ?? new StorefrontProvider();
        }

        public IFormsAuthentication FormsAuth
        {
            get;
            private set;
        }

        public StorefrontProvider Provider
        {
            get;
            private set;
        }

        [LogMethodCall]
        [Authorize]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            AccountViewData viewData = new AccountViewData();
            AddMasterData(viewData);
            viewData.PasswordLength = Provider.MinRequiredPasswordLength;


            // Non-POST requests should just display the ChangePassword form 
            if (Request.HttpMethod != "POST")
            {
                return View(viewData);
            }

            // Basic parameter validation
            List<string> errors = new List<string>();
            if (String.IsNullOrEmpty(currentPassword))
            {
                errors.Add("You must specify a current password.");
            }
            if (newPassword == null || newPassword.Length < Provider.MinRequiredPasswordLength)
            {
                errors.Add(String.Format(CultureInfo.InvariantCulture,
                         "You must specify a new password of {0} or more characters.",
                         Provider.MinRequiredPasswordLength));
            }
            if (!String.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            {
                errors.Add("The new password and confirmation password do not match.");
            }

            if (errors.Count == 0)
            {
                MembershipUser currentUser = Provider.GetUser(User.Identity.Name, true /* userIsOnline */);
                bool changeSuccessful = false;
                try
                {
                    changeSuccessful = currentUser.ChangePassword(currentPassword, newPassword);
                }
                catch
                {
                    // An exception is thrown if the new password does not meet the provider's requirements
                }

                if (changeSuccessful)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    errors.Add("The current password is incorrect or the new password is invalid.");
                }
            }

            viewData.errors = errors;
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult ChangePasswordSuccess()
        {
            AccountViewData viewData = new AccountViewData();
            AddMasterData(viewData);
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Login(string email, string password, bool? rememberMe)
        {
            SFViewData viewData = new SFViewData();
            AddMasterData(viewData);

            viewData.bLoggingIn = true;
            if (Request.HttpMethod != "POST")
            {
                return View(viewData);
            }

            List<string> errors = new List<string>();
            if (String.IsNullOrEmpty(email))
            {
                errors.Add("You must specify a e-mail address.");
            }
            if (errors.Count == 0)
            {
                bool loginSuccessful = Provider.ValidateUser(email, password);

                if (loginSuccessful)
                {

                    FormsAuth.SetAuthCookie(email, rememberMe ?? false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    errors.Add("The e-mail address or password provided is incorrect.");
                }
            }
            viewData.errors = errors;
            viewData.email = email;
            return View(viewData);
        }

        [LogMethodCall]
        public ActionResult Logout()
        {
            FormsAuth.SignOut();
            return RedirectToAction("Login", "Account");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity is WindowsIdentity)
            {
                throw new InvalidOperationException("Windows authentication is not supported.");
            }
        }

        [LogMethodCall]
        public ActionResult Register(string email, string password, string confirmPassword)
        {
            AccountViewData viewData = new AccountViewData();
            AddMasterData(viewData);
            viewData.PasswordLength = Provider.MinRequiredPasswordLength;

            if (Request.HttpMethod != "POST")
            {
                return View(viewData);
            }

            List<string> errors = new List<string>();
            if (String.IsNullOrEmpty(email))
            {
                errors.Add("You must specify an email address.");
            }
            if (password == null || password.Length < Provider.MinRequiredPasswordLength)
            {
                errors.Add(String.Format(CultureInfo.InvariantCulture,
                         "You must specify a password of {0} or more characters.",
                         Provider.MinRequiredPasswordLength));
            }
            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                errors.Add("The password and confirmation do not match.");
            }

            if (errors.Count == 0)
            {
                string cookie = GetStorefrontCookie(Request, Response);
                MembershipCreateStatus createStatus;
                MembershipUser newUser = Provider.CreateUser(cookie, password, email, null, null, true, null, out createStatus);

                if (newUser != null)
                {
                    FormsAuth.SetAuthCookie(email, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    errors.Add(ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            viewData.errors = errors;
            viewData.EMail = email;
            return View(viewData);
        }

        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "E-mail address already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
