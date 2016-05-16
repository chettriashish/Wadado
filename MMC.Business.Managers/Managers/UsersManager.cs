using Core.Common.Contracts;
using Core.Common.Core;
using MMC.Business.Common;
using MMC.Business.Contracts;
using MMC.Business.Contracts.DataContracts;
using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class UsersManager : ManagerBase, IUsersService
    {
        const string MOBILE = "_mob";
        const string TABLET = "_tab";
        const string THUMBNAIL = "_thumb";
        public UsersManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
            ObjectBase.Container = Bootstrapper.Bootstrapper.Initialise();
        }
        public UserSessionDataContract LogUserSession()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IUserApplicationActivityRepository userApplicationActivityRepository
                        = _DataRepositoryFactory.GetDataRepository<IUserApplicationActivityRepository>();
                UserApplicationActivityDetails userActivityDetails = userApplicationActivityRepository.LogUserSession();
                UserSessionDataContract result = new UserSessionDataContract();
                result.SessionKey = userActivityDetails.SessionKey;
                result.IsUserLoggedIn = userActivityDetails.UserLoggedIn;
                result.LoginMethod = userActivityDetails.LoginMethod;

                return result;
            });
        }
        [OperationBehavior(TransactionScopeRequired = true)]
        public UserSessionDataContract AddGuestInformation(UserSessionDataContract userInformation)
        {
            return ExecuteFaultHandledOperation(() =>
                {
                    IUserDetailsBusinessEngine userDetailsBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IUserDetailsBusinessEngine>();
                    IActivitiesBookingEngine activitiesBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IActivitiesBookingEngine>();
                    GuestInformationMaster guestInformation = userDetailsBusinessEngine.AddGuestInformation(userInformation.Name, userInformation.GuestKey, userInformation.Email);
                    activitiesBusinessEngine.UpdateActivityForUser(userInformation.SessionKey, userInformation.GuestKey);
                    IUserApplicationActivityRepository userApplicationActivityRepository
                       = _DataRepositoryFactory.GetDataRepository<IUserApplicationActivityRepository>();
                    userApplicationActivityRepository.UpdateUserSession(userInformation.SessionKey, userInformation.IsUserLoggedIn, userInformation.LoginMethod);
                    //for existing users
                    if (guestInformation.DOB != default(DateTime))
                    {
                        userInformation.DOB = guestInformation.DOB;
                        userInformation.Address = guestInformation.Address;
                        userInformation.City = guestInformation.City;
                        userInformation.Email = guestInformation.Email;
                    }
                    return userInformation;
                });
        }
        [OperationBehavior(TransactionScopeRequired = true)]
        public bool UpdateGuestInformation(UserSessionDataContract userInformation)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IUserDetailsBusinessEngine userDetailsBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IUserDetailsBusinessEngine>();
                IActivitiesBookingEngine activitiesBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IActivitiesBookingEngine>();
                activitiesBusinessEngine.UpdateActivityForUser(userInformation.SessionKey, userInformation.GuestKey);
                GuestInformationMaster guestInformation = new GuestInformationMaster();
                guestInformation.GuestKey = userInformation.GuestKey;
                guestInformation.Address = userInformation.Address;
                guestInformation.City = userInformation.City;
                guestInformation.DOB = userInformation.DOB;
                guestInformation.Email = userInformation.Email;
                guestInformation.Name = userInformation.Name;
                guestInformation.PhoneNumber = userInformation.PhoneNumber;
                guestInformation.Pin = userInformation.Pin;
                guestInformation.State = userInformation.State;
                return userDetailsBusinessEngine.UpdateGuestInformation(guestInformation, userInformation.SessionKey);
            });
        }

        public UserSessionDataContract GetGuestInformation(string guestKey)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IUserDetailsBusinessEngine userDetailsBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IUserDetailsBusinessEngine>();
                IActivitiesBookingEngine activitiesBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IActivitiesBookingEngine>();
                GuestInformationMaster guestInformation = userDetailsBusinessEngine.GetUserInformation(guestKey);
                UserSessionDataContract result = new UserSessionDataContract();
                result.GuestKey = guestInformation.GuestKey;
                result.Address = guestInformation.Address;
                result.City = guestInformation.City;
                result.DOB = guestInformation.DOB;
                result.Email = guestInformation.Email;
                result.Name = guestInformation.Name;
                result.PhoneNumber = guestInformation.PhoneNumber;
                result.Pin = guestInformation.Pin;
                result.State = guestInformation.State;
                return result;
            });
        }


        public bool AddToFavorites(string guestKey, string activityKey)
        {
            return ExecuteFaultHandledOperation(() =>
                {
                    IUserDetailsBusinessEngine userDetailsBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IUserDetailsBusinessEngine>();
                    return userDetailsBusinessEngine.AddToFavorites(guestKey, activityKey);
                });
        }

        public IEnumerable<ActivitySummaryDataContract> RemoveFromFavorites(string guestKey, string activityKey, string userAgent)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<ActivitySummaryDataContract> result = new List<ActivitySummaryDataContract>();
                IUserDetailsBusinessEngine userDetailsBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IUserDetailsBusinessEngine>();
                userDetailsBusinessEngine.RemoveFromFavorites(guestKey, activityKey);
                return GetFavoriteActivities(guestKey, userAgent);
            });
        }

        public IEnumerable<ActivitySummaryDataContract> GetFavoriteActivities(string guestKey, string userAgent)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                List<ActivitySummaryDataContract> result = new List<ActivitySummaryDataContract>();
                IUserDetailsBusinessEngine userDetailsBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IUserDetailsBusinessEngine>();
                IEnumerable<ActivitiesMaster> activitiesResult = userDetailsBusinessEngine.GetFavoriteActivities(guestKey);
                IActivityCategoryMasterRepository activitiesCategoryMasterRepository = _DataRepositoryFactory.GetDataRepository<IActivityCategoryMasterRepository>();
                IActivityTypeCategoryRepository activityTypeRepository = _DataRepositoryFactory.GetDataRepository<IActivityTypeCategoryRepository>();
                IActivityLocationRepository activityLocationRepository = _DataRepositoryFactory.GetDataRepository<IActivityLocationRepository>();
                ILocationsMasterRepository locationMasterRepository = _DataRepositoryFactory.GetDataRepository<ILocationsMasterRepository>();
                IActivityImagesRepository imageRepository = _DataRepositoryFactory.GetDataRepository<IActivityImagesRepository>();
                ITopOfferMappingRepository topOffersMappingRepository = _DataRepositoryFactory.GetDataRepository<ITopOfferMappingRepository>();
                foreach (var item in activitiesResult)
                {
                    ActivitySummaryDataContract summary = new ActivitySummaryDataContract();
                    summary.ActivityName = item.Name;
                    summary.ActivityKey = item.ActivitesKey;
                    string activityCategoryKey = activityTypeRepository.Get().Where(entity => entity.ActivityTypeKey == item.ActivityTypeKey && entity.IsPrimary == true).FirstOrDefault().ActivityCategoryKey;
                    summary.ActivityCategory = activitiesCategoryMasterRepository.Get().Where(entity => entity.ActivityCategoryKey == activityCategoryKey).FirstOrDefault().ActivityCategory;
                    string locationKey = activityLocationRepository.Get().Where(entity => entity.LocationKey == item.LocationKey).FirstOrDefault().LocationKey;
                    summary.Location = locationMasterRepository.Get().Where(entity => entity.LocationKey == locationKey).FirstOrDefault().LocationName;
                    if (userAgent == BusinessResource.SMARTPHONE)
                    {
                        summary.ThumbNailURL = string.Format("{0}{1}{2}",
                          imageRepository.Get().Where(entity => entity.ActivityKey == item.ActivitesKey && entity.IsThumbnail == true).FirstOrDefault().ImageURL
                          , MOBILE
                          , THUMBNAIL);
                    }
                    else if (userAgent == BusinessResource.TABLET)
                    {
                        summary.ThumbNailURL = string.Format("{0}{1}{2}",
                            imageRepository.Get().Where(entity => entity.ActivityKey == item.ActivitesKey && entity.IsThumbnail == true).FirstOrDefault().ImageURL
                            , TABLET
                            , THUMBNAIL);
                    }
                    else
                    {
                        summary.ThumbNailURL = string.Format("{0}{1}",
                          imageRepository.Get().Where(entity => entity.ActivityKey == item.ActivitesKey && entity.IsThumbnail == true).FirstOrDefault().ImageURL
                          , THUMBNAIL);
                    }
                    if (topOffersMappingRepository.CheckAndFetchSingleOfferExists(item.ActivitesKey, BusinessResource.SINGLE) != null)
                    {
                        summary.IsSpecialOffer = true;
                    }
                    else
                    {
                        summary.IsSpecialOffer = false;
                    }
                    //TBD ON INFORMATION COLLECTED
                    summary.IsTopTrending = false;
                    result.Add(summary);
                }
                return result;
            });
        }

        public bool CheckForActivityInFavorites(string guestKey, string activityKey)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IUserDetailsBusinessEngine userDetailsBusinessEngine = _BusinessEngineFactory.GetBusinessEngine<IUserDetailsBusinessEngine>();
                return userDetailsBusinessEngine.CheckForActivityInFavorites(guestKey, activityKey);
            });
        }

        public CompanyMaster CreateCompanyForSelectedUser(string userId, CompanyMaster company)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ICompanyMasterRepository companyMasterRepository = _DataRepositoryFactory.GetDataRepository<ICompanyMasterRepository>();
                IUserCompanyMappingRepository userCompanyRepository = _DataRepositoryFactory.GetDataRepository<IUserCompanyMappingRepository>();

                if (!string.IsNullOrWhiteSpace(company.CompanyKey))
                {
                    CompanyMaster updateCompany = companyMasterRepository.Get(company.CompanyKey);
                    updateCompany.Address = company.Address;
                    updateCompany.Name = company.Name;
                    updateCompany.ContactPerson = company.ContactPerson;
                    updateCompany.Email = company.Email;
                    updateCompany.LocationCoordinates = company.LocationCoordinates;
                    companyMasterRepository.Update(updateCompany);
                }

                else
                {
                    company.CompanyKey = Guid.NewGuid().ToString();
                    companyMasterRepository.Add(company);
                    UserCompanyMapping userCompany = new UserCompanyMapping
                    {
                        UserCompanyKey = Guid.NewGuid().ToString(),
                        UserKey = userId,
                        CompanyKey = company.CompanyKey,
                        IsDeleted = false,
                    };
                    userCompanyRepository.Add(userCompany);
                }                
                return company;
            });
        }

        public CompanyMaster CheckIfUserBelongsToCompany(string userId)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IUserCompanyMappingRepository userCompanyRepository = _DataRepositoryFactory.GetDataRepository<IUserCompanyMappingRepository>();
                if (userCompanyRepository.IsUserMappedToCompany(userId))
                {
                    return userCompanyRepository.GetUserCompanyDetails(userId);
                }
                else
                {
                    return new CompanyMaster();
                }
            });
        }

        public bool RegisterUser(string email, string password)
        {
            IRegisteredUsersRepository registeredUsersRepository = _DataRepositoryFactory.GetDataRepository<IRegisteredUsersRepository>();

            return ExecuteFaultHandledOperation(() =>
            {
                return registeredUsersRepository.RegisterUser(email, password);
            });
        }

        public bool CheckAdminUser(string email)
        {
            IRegisteredUsersRepository registeredUsersRepository = _DataRepositoryFactory.GetDataRepository<IRegisteredUsersRepository>();

            return ExecuteFaultHandledOperation(() =>
            {
                return registeredUsersRepository.CheckAdminUser(email);
            });
        }
    }
}
