using Core.Common.Contracts;
using MMC.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.Contracts.RepositoryInterfaces
{
    public interface IUserApplicationActivityRepository : IDataRepository<UserApplicationActivityDetails>
    {
        UserApplicationActivityDetails LogUserSession();
        void UpdateUserSession(string sessionKey, bool isUserLoggedIn, string loginMethod);
    }
}
