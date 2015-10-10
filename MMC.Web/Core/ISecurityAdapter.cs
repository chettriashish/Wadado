using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Web.Core
{
    public interface ISecurityAdapter
    {
        void Initialize();
        void Register(string loginEmail, string password, object propertyValues);
        bool Login(string loginEmail, string password, bool rememberMe);
        bool ChangePassword(string loginEmail, string oldPassword, string newPassword);
        bool UserExists(string loginEmail);
    }
}
