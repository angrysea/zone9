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
using Storefront.Models;
using StorefrontModel;

namespace Storefront.Provider
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

        private Users SelectUserWithValidation(string email, string password)
        {
            string [] parameters = { "Email", "Password" };
            object [] values = { email, password };
            return context.Users.First(parameters, values);
        }
            
        private Users SelectUser(string email)
        {
            return context.Users.First("Email", email);
        }

        private Users SelectUserByKey(string key)
        {
            return context.Users.First("ID", key);
        }

        private List<Users> SelectAllUsers()
        {
            return context.Users.Select();
        }

        private bool InsertUser(Users user)
        {
            context.Users.Insert(user);
            return true;
        }

        private bool UpdateUser(Users user)
        {
            context.Users.Update(user);
            return true;
        }

        private bool DeleteUser(Users user)
        {
            context.Users.Delete(user);
            return true;
        }

        public override bool ChangePassword(string email, string oldPwd, string newPwd)
        {
            Users user = SelectUserWithValidation(email, oldPwd);

            if (user==default(Users))
                return false;

            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(email, newPwd, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Change password canceled due to new password validation failure.");


            user.Password = newPwd;
            return UpdateUser(user);
        }

        public override bool 
            ChangePasswordQuestionAndAnswer(string email,
                                            string password,
                                            string newPwdQuestion,
                                            string newPwdAnswer)
        {
            Users user = SelectUserWithValidation(email, password);

            if (user == default(Users))
                return false;

            user.PasswordQuestion = newPwdQuestion;
            user.PasswordAnswer = newPwdAnswer;
            return UpdateUser(user);
        }

        public override MembershipUser CreateUser(string username,
                                                  string password,
                                                  string email,
                                                  string passwordQuestion,
                                                  string passwordAnswer,
                                                  bool isApproved,
                                                  object providerUserKey,
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

                Users user = new Users();
		        user.Cookie = username;
				user.Email = email;
				user.Password = EncodePassword(password);
				user.PasswordQuestion = passwordQuestion;
				user.PasswordAnswer = EncodePassword(passwordAnswer);
				user.Affiliate = 0;
				user.Refereddate = createDate;
				user.Loggedin = 0;
				user.Lastsortorder = 0;
				user.ContinueShoppingURL = "";
				user.Creationdate = createDate;
				user.Logindate = createDate;
				user.Lastlogindate = createDate;
				user.IsApproved = (short)(isApproved==true?1:0);
				user.LastActivityDate= createDate;
				user.LastPasswordChangedDate= createDate;
				user.IsLockedOut = 0;
				user.IsOnline = 0;
				user.FailedPasswordAttemptCount = 0;
                user.FailedPasswordAnswerAttemptCount = 0;

                if (InsertUser(user))
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

        public override bool DeleteUser(string email, bool deleteAllRelatedData)
        {
            Users user = SelectUser(email);

            if (user != default(Users))
            {
                return DeleteUser(user);
            }
            return false;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            List<Users> userList = SelectAllUsers();
            MembershipUserCollection users = new MembershipUserCollection();

            int startindex = (pageIndex-1)*pageSize;
            int total = 0;
            int max = userList.Count;
            for (int i = startindex; i < startindex + pageSize && i < max; i++, total++)
            {
                MembershipUser u = GetUserFromReader(userList[i]);
                users.Add(u);
            }
            totalRecords = total;
            return users;
        }

        public override int GetNumberOfUsersOnline()
        {

            TimeSpan onlineSpan = new TimeSpan(0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0);
            DateTime compareTime = DateTime.Now.Subtract(onlineSpan);
            int numOnline = 0;

            return numOnline;
        }

        public override string GetPassword(string email, string answer)
        {
            if (!EnablePasswordRetrieval)
            {
                throw new ProviderException("Password Retrieval Not Enabled.");
            }

            if (PasswordFormat == MembershipPasswordFormat.Hashed)
            {
                throw new ProviderException("Cannot retrieve Hashed passwords.");
            }

            string [] parameters = { "Email", "PasswordAnswer" };
            object [] values = { email, answer };
            Users user = context.Users.First(parameters, values);



            if (RequiresQuestionAndAnswer && !CheckPassword(answer, user.PasswordAnswer))
            {
                UpdateFailureCount(email, "passwordAnswer");

                throw new MembershipPasswordException("Incorrect password answer.");
            }


            string password = user.Password;
            if (PasswordFormat == MembershipPasswordFormat.Encrypted)
            {
                password = UnEncodePassword(password);
            }

            return password;
        }



        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            Users user = SelectUser(email);
            if (user == default(Users))
                return null;
            if (userIsOnline)
            {
                user.LastActivityDate = DateTime.Now;
                UpdateUser(user);
            }
            return GetUserFromReader(user);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            Users user = SelectUserByKey(providerUserKey.ToString());
            if (user == default(Users))
                return null;
            if (userIsOnline)
            {
                user.LastActivityDate = DateTime.Now;
                UpdateUser(user);
            }
            return GetUserFromReader(user);
        }

        private MembershipUser GetUserFromReader(Users user)
        {
            return new MembershipUser("StorefrontProvider",
                                      user.Email,
                                      user.Cookie,
                                      user.Email,
                                      user.PasswordQuestion,
                                      user.Comment,
                                      user.IsApproved=='Y'?true:false,
                                      user.IsLockedOut == 'Y' ? true : false,
                                      user.Creationdate,
                                      user.Lastlogindate,
                                      user.LastActivityDate,
                                      user.LastPasswordChangedDate,
                                      (DateTime)user.LastLockedOutDate);
        }

        public override bool UnlockUser(string Email)
        {
            Users user = SelectUser(Email);
            if (user == default(Users))
                return false;

            user.IsLockedOut = 0;
            user.LastLockedOutDate = DateTime.Now;

            return UpdateUser(user);
        }

        public override string GetUserNameByEmail(string email)
        {
            Users user = context.Users.First("Email", email);

            if (user == null)
                return "";

            return user.Email;
        }

        public override string ResetPassword(string email, string answer)
        {
            if (!EnablePasswordReset)
            {
                throw new NotSupportedException("Password reset is not enabled.");
            }

            if (answer == null && RequiresQuestionAndAnswer)
            {
                UpdateFailureCount(email, "passwordAnswer");

                throw new ProviderException("Password answer required for password reset.");
            }

            string newPassword =
              System.Web.Security.Membership.GeneratePassword(newPasswordLength, MinRequiredNonAlphanumericCharacters);


            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(email, newPassword, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Reset password canceled due to password validation failure.");

            Users user = SelectUser(email);
            if(user==default(Users))
            {
                throw new MembershipPasswordException("The supplied user name is not found.");
            }

            if (RequiresQuestionAndAnswer && !CheckPassword(answer, user.PasswordAnswer))
            {
                UpdateFailureCount(email, "passwordAnswer");

                throw new MembershipPasswordException("Incorrect password answer.");
            }

            user.Password = EncodePassword(newPassword);
            user.LastPasswordChangedDate = DateTime.Now;
            if (!UpdateUser(user))
                return "";
            return user.Password;
        }


        public override void UpdateUser(MembershipUser user)
        {
            Users u = SelectUser(user.Email);
            u.Email = user.Email;
            u.Comment = user.Comment;
            u.IsApproved = (short)(user.IsApproved?1:0);
            UpdateUser(u);
        }

        public override bool ValidateUser(string email, string password)
        {
            bool isValid = false;

            Users user = SelectUserWithValidation(email, password);
            if (user != default(Users))
            {
                if (CheckPassword(password, user.Password))
                {
                    if (user.IsApproved == 1)
                    {
                        isValid = true;
                        user.Lastlogindate = DateTime.Now;
                        UpdateUser(user);

                    }
                }
                else
                {
                    UpdateFailureCount(email, "password");
                }
            }
            return isValid;
        }

        private void UpdateFailureCount(string email, string failureType)
        {
            Users user = SelectUser(email);
            DateTime windowStart = new DateTime();
            int failureCount = 0;


            if (failureType == "password")
            {
                failureCount = user.FailedPasswordAttemptCount;
                windowStart = (DateTime)user.FailedPasswordAttemptWindowStart;
            }
            else if (failureType == "passwordAnswer")
            {
                failureCount = user.FailedPasswordAnswerAttemptCount;
                windowStart = (DateTime)user.FailedPasswordAnswerAttemptWindowStart;
            }
            DateTime windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);

            if (failureCount == 0 || DateTime.Now > windowEnd)
            {
                if (failureType == "password") {
                    user.FailedPasswordAttemptCount = 1;
                    user.FailedPasswordAttemptWindowStart = DateTime.Now;
                }
                else if (failureType == "passwordAnswer") {
                    user.FailedPasswordAnswerAttemptCount = 1;
                    user.FailedPasswordAnswerAttemptWindowStart = DateTime.Now;
                }
                UpdateUser(user);
            }
            else
            {
                if (failureCount++ >= MaxInvalidPasswordAttempts)
                {
                    user.IsLockedOut = 1;
                    user.LastLockedOutDate = DateTime.Now;
                    UpdateUser(user);
                }
                else
                {
                    if (failureType == "password")
                    {
                        user.FailedPasswordAttemptCount = failureCount;
                    }
                    else if (failureType == "passwordAnswer")
                    {
                        user.FailedPasswordAnswerAttemptCount = failureCount;
                    }
                    UpdateUser(user);
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


        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            List<Users> userList = context.Users.Select("Email like @Email", "Email", usernameToMatch);

            MembershipUserCollection users = new MembershipUserCollection();

            int startIndex = pageSize * pageIndex;
            int endIndex = startIndex + pageSize - 1;

            int startindex = (pageIndex - 1) * pageSize;
            int total = 0;
            int max = userList.Count;
            for (int i = startindex; i < startindex + pageSize && i < max; i++, total++)
            {
                MembershipUser u = GetUserFromReader(userList[i]);
                users.Add(u);
            }
            totalRecords = total;
            return users;

        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            List<Users> userList = context.Users.Select("Email like @Email", "Email", emailToMatch);

            MembershipUserCollection users = new MembershipUserCollection();

            int startIndex = pageSize * pageIndex;
            int endIndex = startIndex + pageSize - 1;

            int startindex = (pageIndex - 1) * pageSize;
            int total = 0;
            int max = userList.Count;
            for (int i = startindex; i < startindex + pageSize && i < max; i++, total++)
            {
                MembershipUser u = GetUserFromReader(userList[i]);
                users.Add(u);
            }
            totalRecords = total;
            return users;
        }
    }
}
