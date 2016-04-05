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

        public IEnumerable<ActivitySummaryDataContract> GetFavoriteActivities(string guestKey, string userAgent)
        {
            return Channel.GetFavoriteActivities(guestKey, userAgent);
        }
        public bool CheckForActivityInFavorites(string guestKey, string activityKey)
        {
            return Channel.CheckForActivityInFavorites(guestKey, activityKey);
        }
        public CompanyMaster CheckIfUserBelongsToCompany(string userId)
        {
            return Channel.CheckIfUserBelongsToCompany(userId);
        }
        public CompanyMaster CreateCompanyForSelectedUser(string userId, CompanyMaster company)
        {
            return Channel.CreateCompanyForSelectedUser(userId, company);
        }

        public CompanyMaster CreateCompanyForSelectedUser(string userId, string companyName, string address, string telephoneNumber, string email, string contactPerson)
        {
            CompanyMaster newCompany = new CompanyMaster
            {
                Address = address,
                ContactPerson = contactPerson,
                Email = email,
                Name = companyName,
                TelephoneNumber = telephoneNumber,
                CreatedDate = DateTime.Now,
                CreatedBy = userId,
                Rating = 0,                 
            };
            return CreateCompanyForSelectedUser(userId, newCompany);
        }
    }
}
