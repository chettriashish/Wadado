using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Login.Contracts
{
    public interface ILoginService
    {
        T LogUserSession<T>(Func<T> codeToExecute);
        T AddUserInformation<T>(Func<T> codeToExecute);
    }
}
