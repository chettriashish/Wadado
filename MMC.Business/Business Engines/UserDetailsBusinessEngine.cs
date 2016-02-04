using Core.Common.Contracts;
using MMC.Business.Common;
using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.BusinessEngines
{
    public class UserDetailsBusinessEngine : IUserDetailsBusinessEngine
    {
        IDataRepositoryFactory _DataRepositoryFactory;
        public UserDetailsBusinessEngine(IDataRepositoryFactory dataRepositoryFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
        }
        public GuestInformationMaster AddGuestInformation(string userName, string userKey, string email)
        {
            IGuestInformationMasterRepository guestInformationRepository = _DataRepositoryFactory.GetDataRepository<IGuestInformationMasterRepository>();
            GuestInformationMaster result = guestInformationRepository.Get(userKey);
            if (result == null)
            {
                result = new GuestInformationMaster() { GuestKey = userKey, Name = userName, DOB = DateTime.Now };
                guestInformationRepository.Add(result);
            }
            return result;
        }
        public bool UpdateGuestInformation(GuestInformationMaster guestInformation, string sessionKey)
        {
            IGuestInformationMasterRepository guestInformationRepository = _DataRepositoryFactory.GetDataRepository<IGuestInformationMasterRepository>();
            guestInformationRepository.Update(guestInformation);
            return true;
        }
        public GuestInformationMaster GetUserInformation(string guestKey)
        {
            IGuestInformationMasterRepository guestInformationRepository = _DataRepositoryFactory.GetDataRepository<IGuestInformationMasterRepository>();
            return guestInformationRepository.Get(guestKey);
        }
        public bool AddToFavorites(string guestKey, string activityKey)
        {
            IGuestFavoritesRepository guestFavoritesRepository = _DataRepositoryFactory.GetDataRepository<IGuestFavoritesRepository>();
            IEnumerable<GuestFavorites> allfavorites = guestFavoritesRepository.GetAllFavorites(guestKey);
            if (allfavorites != null && allfavorites.Where(entity => entity.ActivityKey == activityKey).Count() == 0)
            {
                GuestFavorites result = new GuestFavorites() { GuestFavouritesKey = string.Format("{0}{1}", Guid.NewGuid().ToString(), Guid.NewGuid().ToString()), ActivityKey = activityKey, GuestKey = guestKey, IsDeleted = false };
                guestFavoritesRepository.Add(result);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RemoveFromFavorites(string guestKey, string activityKey)
        {
            IGuestFavoritesRepository guestFavoritesRepository = _DataRepositoryFactory.GetDataRepository<IGuestFavoritesRepository>();
            GuestFavorites removeItem = guestFavoritesRepository.GetAllFavorites(guestKey).Where(entity => entity.ActivityKey == activityKey).FirstOrDefault();
            if (removeItem != null)
            {
                guestFavoritesRepository.Remove(removeItem);
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<ActivitiesMaster> GetFavoriteActivities(string guestKey)
        {
            IGuestFavoritesRepository guestFavoritesRepository = _DataRepositoryFactory.GetDataRepository<IGuestFavoritesRepository>();
            IActivitiesMasterRepository activitiesMasterRepository
                = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

            IEnumerable<GuestFavorites> allfavorites = guestFavoritesRepository.GetAllFavorites(guestKey);

            List<ActivitiesMaster> result = new List<ActivitiesMaster>();
            if (allfavorites != null)
            {
                foreach (var item in allfavorites)
                {
                    result.Add(activitiesMasterRepository.Get().Where(entity => entity.ActivitesKey == item.ActivityKey).FirstOrDefault());
                }
            }
            return result;
        }

        public bool CheckForActivityInFavorites(string guestKey, string activityKey)
        {
            IGuestFavoritesRepository guestFavoritesRepository = _DataRepositoryFactory.GetDataRepository<IGuestFavoritesRepository>();
            if(guestFavoritesRepository.GetAllFavorites(guestKey).Where(entity => entity.ActivityKey == activityKey).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
