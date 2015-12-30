using Core.Common.Contracts;
using MMC.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Common
{
    public interface IUserDetailsBusinessEngine : IBusinessEngine
    {
        GuestInformationMaster AddGuestInformation(string userName, string userKey, string email);
        bool UpdateGuestInformation(GuestInformationMaster guestInformation, string sessionKey);

        GuestInformationMaster GetUserInformation(string sessionKey);
        bool AddToFavorites(string guestKey, string activityKey);
        bool RemoveFromFavorites(string guestKey, string activityKey);
        IEnumerable<ActivitiesMaster> GetFavorites(string guestKey);
        bool CheckForActivityInFavorites(string guestKey, string activityKey);
    }
}
