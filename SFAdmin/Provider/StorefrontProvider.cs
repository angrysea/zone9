using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Web.Configuration;
using System.Diagnostics;
using System.Web.Security;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using SFAdmin.Models;
using StorefrontModel;

namespace SFAdmin.Provider
{
    public class StorefrontProvider : MembershipProvider
    {
        protected StorefrontEntities context;

        public StorefrontProvider()
        {
            context = new StorefrontEntities();
            ProviderConfiguration = context.ProviderConfiguration.First();
        }

        private int newPasswordLength = 8;
        //private string eventLog = "Application";
        //private string exceptionMessage = "An exception occurred. Please check the Event Log.";
        private MachineKeySection machineKey;
        public ProviderConfiguration ProviderConfiguration { get; set; }
        protected MembershipPasswordFormat pPasswordFormat;


        public override string ApplicationName { 
            get {
                return ProviderConfiguration.ApplicationName; 
            }
            set {
                ProviderConfiguration.ApplicationName = value;
            }
        }

        public override bool EnablePasswordReset 
        { 
            get {
                return ProviderConfiguration.EnablePasswordReset==0?false:true; 
            }
        }

        public override bool EnablePasswordRetrieval
        { 
            get{
                return ProviderConfiguration.EnablePasswordRetrieval==0?false:true;
            }
        }

        public override int MaxInvalidPasswordAttempts
        { 
            get {
                return ProviderConfiguration.MaxInvalidPasswordAttempts; 
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        { 
            get {
                return ProviderConfiguration.MinRequiredNonAlphanumericCharacters; 
            }
        }

        public override int MinRequiredPasswordLength
        { 
            get {
                return ProviderConfiguration.MinRequiredPasswordLength; 
            }
        }

        public override int PasswordAttemptWindow
        { 
            get {
                return ProviderConfiguration.PasswordAttemptWindow; 
            }
        }
        public override MembershipPasswordFormat PasswordFormat
        { 
            get {
                return pPasswordFormat; 
            }
        }

        public override string PasswordStrengthRegularExpression
        { 
            get {
                return ProviderConfiguration.PasswordStrengthRegularExpression; 
            }
        }

        public override bool RequiresQuestionAndAnswer
        { 
            get {
                return ProviderConfiguration.RequiresQuestionAndAnswer==0?false:true; 
            }
        }

        public override bool RequiresUniqueEmail
        { 
            get {
                return ProviderConfiguration.RequiresUniqueEmail==0?false:true; 
            }
        }

        public void Initialize(ProviderConfiguration providerConfiguration)
        {
            ProviderConfiguration = providerConfiguration;
            string temp_format = ProviderConfiguration.PasswordFormat;
            if (temp_format == null)
            {
                temp_format = "Hashed";
            }

            switch (temp_format)
            {
                case "Hashed":
                    pPasswordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    pPasswordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    pPasswordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }

            Configuration cfg =
              WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");

            if (machineKey.ValidationKey.Contains("AutoGenerate"))
                if (PasswordFormat != MembershipPasswordFormat.Clear)
                    throw new ProviderException("Hashed or Encrypted passwords " +
                                                "are not supported with auto-generated keys.");
        }

        private Admins SelectAdminWithValidation(string adminname, string password)
        {
            string[] parameters = { "Email", "Password" };
            string [] values = { adminname, password };
            return context.Admins.First(parameters, values);
        }
            
        private Admins SelectAdmin(string adminname)
        {
            return context.Admins.First("Email", adminname);
        }

        private Admins SelectAdminByKey(string key)
        {
            return context.Admins.First("ID", key);
        }

        private List<Admins> SelectAllAdmins()
        {
            return context.Admins.Select();
        }

        private bool InsertAdmin(Admins admin)
        {
            context.Admins.Insert(admin);
            return true;
        }

        private bool UpdateAdmin(Admins admin)
        {
            context.Admins.Update(admin);
            return true;
        }

        private bool DeleteUser(Admins admin)
        {
            context.Admins.Delete(admin);
            return true;
        }

        public override bool ChangePassword(string email, string oldPwd, string newPwd)
        {
            Admins admin = SelectAdminWithValidation(email, oldPwd);

            if (admin==default(Admins))
                return false;

            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(email, newPwd, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Change password canceled due to new password validation failure.");


            admin.Password = newPwd;
            return UpdateAdmin(admin);
        }



        public override bool ChangePasswordQuestionAndAnswer(string adminname,
                      string password,
                      string newPwdQuestion,
                      string newPwdAnswer)
        {
            Admins admin = SelectAdminWithValidation(adminname, password);

            if (admin == default(Admins))
                return false;

            admin.PasswordQuestion = newPwdQuestion;
            admin.PasswordAnswer = newPwdAnswer;
            return UpdateAdmin(admin);
        }

        public override MembershipUser CreateUser(  string name,
                                                    string password,
                                                    string email,
                                                    string passwordQuestion,
                                                    string passwordAnswer,
                                                    bool isApproved,
                                                    object providerAdminKey,
                                                    out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(email, password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(email) != "")
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser u = GetUser(email, false);

            if (u == null)
            {
                DateTime createDate = DateTime.Now;

                Admins admin = new Admins();
                admin.Email = email;
                admin.Cookie = name;
				admin.Password= EncodePassword(password);
				admin.PasswordQuestion = passwordQuestion;
				admin.PasswordAnswer = EncodePassword(passwordAnswer);
				admin.Affiliate = 0;
				admin.Refereddate = createDate;
				admin.Loggedin = 0;
				admin.Lastsortorder = 0;
				admin.ContinueShoppingURL = "";
				admin.Viewonlystatus = 0;
				admin.Creationdate = createDate;
				admin.Logindate = createDate;
				admin.Lastlogindate = createDate;
				admin.IsApproved = (short)(isApproved==true?1:0);
				admin.LastActivityDate= createDate;
				admin.LastPasswordChangedDate= createDate;
				admin.IsLockedOut = 0;
				admin.IsOnline = 0;
				admin.FailedPasswordAttemptCount = 0;
                admin.FailedPasswordAnswerAttemptCount = 0;

                if (InsertAdmin(admin))
                {
                    status = MembershipCreateStatus.Success;
                }
                else
                {
                    status = MembershipCreateStatus.ProviderError;
                    return null;
                }
                return GetUser(email, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }
            return null;
        }

        public override bool DeleteUser(string adminname, bool deleteAllRelatedData)
        {
            Admins admin = SelectAdmin(adminname);

            if (admin != default(Admins))
            {
                return DeleteUser(admin);
            }
            return false;
        }


        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            List<Admins> adminList = SelectAllAdmins();
            MembershipUserCollection admins = new MembershipUserCollection();

            int startindex = (pageIndex-1)*pageSize;
            int total = 0;
            int max = adminList.Count;
            for (int i = startindex; i < startindex + pageSize && i < max; i++, total++)
            {
                MembershipUser u = GetUserFromReader(adminList[i]);
                admins.Add(u);
            }
            totalRecords = total;
            return admins;
        }

        public override int GetNumberOfUsersOnline()
        {

            TimeSpan onlineSpan = new TimeSpan(0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0);
            DateTime compareTime = DateTime.Now.Subtract(onlineSpan);
            int numOnline = 0;

            return numOnline;
        }

        public override string GetPassword(string adminname, string answer)
        {
            if (!EnablePasswordRetrieval)
            {
                throw new ProviderException("Password Retrieval Not Enabled.");
            }

            if (PasswordFormat == MembershipPasswordFormat.Hashed)
            {
                throw new ProviderException("Cannot retrieve Hashed passwords.");
            }

            string[] parameters = { "Name", "PasswordAnswer" };
            string[] values = { adminname, answer };
            Admins admin = context.Admins.First(parameters, values);

            if (RequiresQuestionAndAnswer && !CheckPassword(answer, admin.PasswordAnswer))
            {
                UpdateFailureCount(adminname, "passwordAnswer");

                throw new MembershipPasswordException("Incorrect password answer.");
            }

            string password = admin.Password;
            if (PasswordFormat == MembershipPasswordFormat.Encrypted)
            {
                password = UnEncodePassword(password);
            }

            return password;
        }



        public override MembershipUser GetUser(string userName, bool adminIsOnline)
        {
            Admins admin = SelectAdmin(userName);
            if (admin == default(Admins))
                return null;
            if (adminIsOnline)
            {
                admin.LastActivityDate = DateTime.Now;
                UpdateAdmin(admin);
            }
            return GetUserFromReader(admin);
        }

        public override MembershipUser GetUser(object providerAdminKey, bool adminIsOnline)
        {
            Admins admin = SelectAdminByKey(providerAdminKey.ToString());
            if (admin == default(Admins))
                return null;
            if (adminIsOnline)
            {
                admin.LastActivityDate = DateTime.Now;
                UpdateAdmin(admin);
            }
            return GetUserFromReader(admin);
        }

        private MembershipUser GetUserFromReader(Admins admin)
        {
            return new MembershipUser("StorefrontProvider",
                                      admin.Email,
                                      admin.Cookie,
                                      admin.Email,
                                      admin.PasswordQuestion,
                                      admin.Comment,
                                      admin.IsApproved=='Y'?true:false,
                                      admin.IsLockedOut == 'Y' ? true : false,
                                      admin.Creationdate,
                                      admin.Lastlogindate,
                                      admin.LastActivityDate,
                                      admin.LastPasswordChangedDate,
                                      (DateTime)admin.LastLockedOutDate);
        }

        public override bool UnlockUser(string userName)
        {
            Admins admin = SelectAdmin(userName);
            if (admin == default(Admins))
                return false;

            admin.IsLockedOut = 0;
            admin.LastLockedOutDate = DateTime.Now;

            return UpdateAdmin(admin);
        }

        public override string GetUserNameByEmail(string email)
        {
            Admins admin = context.Admins.First("Email", email);

            if (admin == null)
                return "";

            return admin.Email;
        }

        public override string ResetPassword(string adminname, string answer)
        {
            if (!EnablePasswordReset)
            {
                throw new NotSupportedException("Password reset is not enabled.");
            }

            if (answer == null && RequiresQuestionAndAnswer)
            {
                UpdateFailureCount(adminname, "passwordAnswer");

                throw new ProviderException("Password answer required for password reset.");
            }

            string newPassword =
              System.Web.Security.Membership.GeneratePassword(newPasswordLength, MinRequiredNonAlphanumericCharacters);


            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(adminname, newPassword, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Reset password canceled due to password validation failure.");

            Admins admin = SelectAdmin(adminname);
            if(admin==default(Admins))
            {
                throw new MembershipPasswordException("The supplied admin name is not found.");
            }

            if (RequiresQuestionAndAnswer && !CheckPassword(answer, admin.PasswordAnswer))
            {
                UpdateFailureCount(adminname, "passwordAnswer");

                throw new MembershipPasswordException("Incorrect password answer.");
            }

            admin.Password = EncodePassword(newPassword);
            admin.LastPasswordChangedDate = DateTime.Now;
            if (!UpdateAdmin(admin))
                return "";
            return admin.Password;
        }


        public override void UpdateUser(MembershipUser user)
        {
            Admins u = SelectAdmin(user.UserName);
            u.Email = user.Email;
            u.Comment = user.Comment;
            u.IsApproved = (short)(user.IsApproved ? 1 : 0);
            UpdateAdmin(u);
        }


        //
        // MembershipProvider.ValidateAdmin
        //

        public override bool ValidateUser(string adminname, string password)
        {
            bool isValid = false;

            Admins admin = SelectAdminWithValidation(adminname, password);
            if (admin != default(Admins))
            {
                if (CheckPassword(password, admin.Password))
                {
                    if (admin.IsApproved == 1)
                    {
                        isValid = true;
                        admin.Lastlogindate = DateTime.Now;
                        UpdateAdmin(admin);

                    }
                }
                else
                {
                    UpdateFailureCount(adminname, "password");
                }
            }
            return isValid;
        }

        private void UpdateFailureCount(string adminname, string failureType)
        {
            Admins admin = SelectAdmin(adminname);
            DateTime windowStart = new DateTime();
            int failureCount = 0;


            if (failureType == "password")
            {
                failureCount = admin.FailedPasswordAttemptCount;
                windowStart = (DateTime)admin.FailedPasswordAttemptWindowStart;
            }
            else if (failureType == "passwordAnswer")
            {
                failureCount = admin.FailedPasswordAnswerAttemptCount;
                windowStart = (DateTime)admin.FailedPasswordAnswerAttemptWindowStart;
            }
            DateTime windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);

            if (failureCount == 0 || DateTime.Now > windowEnd)
            {
                if (failureType == "password") {
                    admin.FailedPasswordAttemptCount = 1;
                    admin.FailedPasswordAttemptWindowStart = DateTime.Now;
                }
                else if (failureType == "passwordAnswer") {
                    admin.FailedPasswordAnswerAttemptCount = 1;
                    admin.FailedPasswordAnswerAttemptWindowStart = DateTime.Now;
                }
                UpdateAdmin(admin);
            }
            else
            {
                if (failureCount++ >= MaxInvalidPasswordAttempts)
                {
                    admin.IsLockedOut = 1;
                    admin.LastLockedOutDate = DateTime.Now;
                    UpdateAdmin(admin);
                }
                else
                {
                    if (failureType == "password")
                    {
                        admin.FailedPasswordAttemptCount = failureCount;
                    }
                    else if (failureType == "passwordAnswer")
                    {
                        admin.FailedPasswordAnswerAttemptCount = failureCount;
                    }
                    UpdateAdmin(admin);
                }
            }
        }

        private bool CheckPassword(string password, string dbpassword)
        {
            string pass1 = password;
            string pass2 = dbpassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Encrypted:
                    pass2 = UnEncodePassword(dbpassword);
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = EncodePassword(password);
                    break;
                default:
                    break;
            }

            if (pass1 == pass2)
            {
                return true;
            }

            return false;
        }


        private string EncodePassword(string password)
        {
            string encodedPassword = password;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    encodedPassword =
                      Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    HMACSHA1 hash = new HMACSHA1();
                    hash.Key = HexToByte(machineKey.ValidationKey);
                    encodedPassword =
                      Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                    break;
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return encodedPassword;
        }


        private string UnEncodePassword(string encodedPassword)
        {
            string password = encodedPassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    password =
                      Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Cannot unencode a hashed password.");
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return password;
        }

        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }


        public override MembershipUserCollection FindUsersByName(string adminnameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            List<Admins> adminList = context.Admins.Select("Email like @Email", "Email", adminnameToMatch);

            MembershipUserCollection admins = new MembershipUserCollection();

            int startIndex = pageSize * pageIndex;
            int endIndex = startIndex + pageSize - 1;

            int startindex = (pageIndex - 1) * pageSize;
            int total = 0;
            int max = adminList.Count;
            for (int i = startindex; i < startindex + pageSize && i < max; i++, total++)
            {
                MembershipUser u = GetUserFromReader(adminList[i]);
                admins.Add(u);
            }
            totalRecords = total;
            return admins;

        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            List<Admins> adminList = context.Admins.Select("Email like @Email", "Email", emailToMatch);

            MembershipUserCollection admins = new MembershipUserCollection();

            int startIndex = pageSize * pageIndex;
            int endIndex = startIndex + pageSize - 1;

            int startindex = (pageIndex - 1) * pageSize;
            int total = 0;
            int max = adminList.Count;
            for (int i = startindex; i < startindex + pageSize && i < max; i++, total++)
            {
                MembershipUser u = GetUserFromReader(adminList[i]);
                admins.Add(u);
            }
            totalRecords = total;
            return admins;

        }
    }
}
