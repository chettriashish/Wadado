using Core.Common.ServiceModel;
using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using MMC.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Proxies
{
    public class UserClient : UserClientBase<IUsersService>, IUsersService
    {
        public UserSessionDataContract LogUserSession()
        {
            return Channel.LogUserSession();
        }

        public UserSessionDataContract AddGuestInformation(UserSessionDataContract userInformation)
        {
            return Channel.AddGuestInformation(userInformation);
        }

        public bool UpdateGuestInformation(UserSessionDataContract userInformation)
        {
            return Channel.UpdateGuestInformation(userInformation);
        }

        public Task<UserSessionDataContract> LogUserSessionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserSessionDataContract> AddGuestInformationAsync(UserSessionDataContract userInformation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateGuestInformationAsync(UserSessionDataContract userInformation)
        {
            throw new NotImplementedException();
        }


        public UserSessionDataContract GetGuestInformation(string guestKey)
        {
            return Channel.GetGuestInformation(guestKey);
        }

        public Task<UserSessionDataContract> GetGuestInformationAsync(string guestKey)
        {
            throw new NotImplementedException();
        }


        public bool AddToFavorites(string guestKey, string activityKey)
        {
            return Channel.AddToFavorites(guestKey, activityKey);
        }

        public IEnumerable<ActivitySummaryDataContract> RemoveFromFavorites(string guestKey, string activityKey, string userAgent)
        {
            return Channel.RemoveFromFavorites(guestKey, activityKey, userAgent);
        }

        public IEnumerable<ActivitySummaryDataContract> GetFavorites(string guestKey, string userAgent)
        {
            return Channel.GetFavorites(guestKey, userAgent);
        }
        public bool CheckForActivityInFavorites(string guestKey, string activityKey)
        {
            return Channel.CheckForActivityInFavorites(guestKey, activityKey);
        }
    }
}
