using MMC.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace MMC.Web.Adapters
{
    public class SecurityAdapter : ISecurityAdapter
    {
        public void Initialize()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("monkeycapEntities", "Account", "AccountId", "LoginEmail", autoCreateTables: true);
        }

        public void Register(string loginEmail, string password, object propertyValues)
        {
            WebSecurity.CreateUserAndAccount(loginEmail, password, propertyValues);
        }

        public bool Login(string loginEmail, string password, bool rememberMe)
        {
            return WebSecurity.Login(loginEmail, password, persistCookie: rememberMe);
        }

        public bool ChangePassword(string loginEmail, string oldPassword, string newPassword)
        {
            return WebSecurity.ChangePassword(loginEmail, oldPassword, newPassword);
        }

        public bool UserExists(string loginEmail)
        {
            return WebSecurity.UserExists(loginEmail);
        }
    }
}