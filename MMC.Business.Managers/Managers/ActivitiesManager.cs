using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MMC.Business.Contracts;
using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System.ServiceModel;
using MMC.Business.Common;
using System.Security.Permissions;
using MMC.Common;
using Core.Common.Exceptions;
using MMC.Business.Contracts.DataContracts;
using System.ServiceModel.Activation;

namespace MMC.Business.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        ReleaseServiceInstanceOnTransactionComplete = false)]
    public class ActivitiesManager : ManagerBase, IActivitiesService
    {
        const string MOBILE = "_mob";
        const string TABLET = "_tab";
        const string THUMBNAIL = "_thumb";
        public ActivitiesManager(IDataRepositoryFactory dataRepositoryFactory, IBusinessEngineFactory businessEngineFactory)
        {
            _DataRepositoryFactory = dataRepositoryFactory;
            _BusinessEngineFactory = businessEngineFactory;
            ObjectBase.Container = Bootstrapper.Bootstrapper.Initialise();
        }

        public ActivityDetailsDataContract GetAllActivities(string locationKey, string activityKey, string userAgent)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesMasterRepository activitiesMasterRepository
                = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

                ActivityDetailsDataContract allActivitiesForLocation = activitiesMasterRepository.GetActivityByLocation(locationKey: locationKey, activityKey: activityKey, userAgent: userAgent);
                return allActivitiesForLocation;
            });
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = Security.MMCAdminRole)]
        //[PrincipalPermission(SecurityAction.Demand, Name = Security.MMCUser)]
        public IEnumerable<ActivitiesMaster> GetAllBookedActivities(string loginName)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesMasterRepository activitiesMasterRepository
                        = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

                IAccountRepository accountRepository
                    = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

                Account authAccount = accountRepository.ValidateUserByLogin(loginName);
                if (authAccount == null)
                {
                    NotFoundException ex =
                    new NotFoundException(string.Format("cannot find account for login name {0} to use for security trimming", loginName));

                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }
                ///validating the account to check with actual logged in users account.
                ///This helps us cross check whether the logged in user is actually the user
                ///whose credentials are being passed.

                ValidateAuthorization(authAccount);

                return activitiesMasterRepository.GetAllActivitiesBooked(userAccountKey: authAccount.AccountKey);
            });
        }
        protected override Account LoadAuthorizationValidationAccount(string loginName)
        {
            IAccountRepository accountRepository
                = _DataRepositoryFactory.GetDataRepository<IAccountRepository>();

            Account authAccount = accountRepository.ValidateUserByLogin(loginName);

            if (authAccount == null)
            {
                throw new NotFoundException(string.Format("cannot find account for login name {0} to use for security trimming", loginName));
            }

            return authAccount;
        }
        public bool CheckForActivityAvailablity(string activityKey, int adults, int children, DateTime bookingDate, string time)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesMasterRepository activitiesMasterRepository
                    = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

                IActivityBookingRepository activityBookingRepository
                    = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();

                IActivitiesBookingEngine activitiesBookingEngine
                    = _BusinessEngineFactory.GetBusinessEngine<IActivitiesBookingEngine>();

                IEnumerable<ActivitiesMaster> allActivities = activitiesMasterRepository.Get();

                IEnumerable<ActivityBooking> allBookedActivites = activityBookingRepository.Get();

                return activitiesBookingEngine.IsActivityAvailable(activityKey, bookingDate, time, allBookedActivites, adults, children, allActivities);
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public ActivityBookingDataContract BookActivityForUser(ActivityBookingDataContract bookingDetails)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesBookingEngine activitiesBookingEngine
                  = _BusinessEngineFactory.GetBusinessEngine<IActivitiesBookingEngine>();
                ActivityBooking activityBooking = new ActivityBooking();
                activityBooking.ActivityBookingKey = bookingDetails.ActivityBookingKey;
                activityBooking.ActivityKey = bookingDetails.ActivityKey;
                activityBooking.BookingDate = Convert.ToDateTime(bookingDetails.BookingDate);
                activityBooking.ChildParticipants = bookingDetails.ChildParticipants;
                activityBooking.Participants = bookingDetails.Participants;
                activityBooking.SessionKey = bookingDetails.SessionKey;
                activityBooking.Time = bookingDetails.Time;
                activityBooking.IsConfirmed = bookingDetails.IsConfirmed;
                activityBooking.IsDeleted = bookingDetails.IsDeleted;
                activityBooking.IsCancelled = bookingDetails.IsCancelled;
                activityBooking.PaymentAmount = bookingDetails.PaymentAmount;
                activityBooking.RefundAmount = bookingDetails.RefundAmount;
                activityBooking.IsPaymentComplete = bookingDetails.IsPaymentComplete;
                activityBooking.CreatedDate = bookingDetails.CreatedDate;
                activitiesBookingEngine.BookActivityForUser(activityBooking);
                return bookingDetails;
            });
        }

        public IEnumerable<ActivityBookingDataContract> GetUsersCurrentActivityCart(string sessionKey, string userAgent)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesBookingEngine activitiesBookingEngine
                    = _BusinessEngineFactory.GetBusinessEngine<IActivitiesBookingEngine>();

                IActivitiesMasterRepository activitiesRepository = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();
                IActivityLocationRepository activityLocationRepository = _DataRepositoryFactory.GetDataRepository<IActivityLocationRepository>();
                IEnumerable<ActivityBooking> allBookedActivites = activitiesBookingEngine.GetBookedActivitiesForUser(sessionKey, default(string));
                IActivityImagesRepository imageRepository = _DataRepositoryFactory.GetDataRepository<IActivityImagesRepository>();
                ILocationsMasterRepository locationRepository = _DataRepositoryFactory.GetDataRepository<ILocationsMasterRepository>();
                List<ActivityBookingDataContract> result = new List<ActivityBookingDataContract>();
                foreach (var item in allBookedActivites)
                {
                    ActivityBookingDataContract bookedActivities = new ActivityBookingDataContract();
                    bookedActivities.ActivityBookingKey = item.ActivityBookingKey;
                    bookedActivities.ActivityKey = item.ActivityKey;
                    bookedActivities.Currency = activitiesRepository.Get(item.ActivityKey).Currency;
                    bookedActivities.Cost = activitiesRepository.Get(item.ActivityKey).Cost;
                    bookedActivities.ActivityName = activitiesRepository.Get(item.ActivityKey).Name;
                    bookedActivities.Location = locationRepository.Get(activityLocationRepository.Get().
                        Where(entity => entity.ActivityKey == item.ActivityKey).FirstOrDefault().LocationKey).LocationName.ToLower();
                    bookedActivities.BookingDate = item.BookingDate;
                    bookedActivities.ChildParticipants = item.ChildParticipants;
                    bookedActivities.Participants = item.Participants;
                    bookedActivities.SessionKey = item.SessionKey;
                    bookedActivities.Time = item.Time;
                    bookedActivities.IsConfirmed = item.IsConfirmed;
                    bookedActivities.IsDeleted = item.IsDeleted;
                    bookedActivities.IsCancelled = item.IsCancelled;
                    bookedActivities.PaymentAmount = item.PaymentAmount;
                    bookedActivities.RefundAmount = item.RefundAmount;
                    bookedActivities.IsPaymentComplete = item.IsPaymentComplete;
                    bookedActivities.CreatedDate = Convert.ToDateTime(item.CreatedDate);
                    bookedActivities.ConfirmationDate = Convert.ToDateTime(item.ConfirmationDate);
                    string thumbnailImageURL = imageRepository.Get().Where(entity => entity.ActivityKey == item.ActivityKey
                        && entity.IsThumbnail == true).FirstOrDefault().ImageURL;
                    if (userAgent == BusinessResource.SMARTPHONE)
                    {
                        bookedActivities.ThumbnailImage = string.Format("Images/{0}{1}", thumbnailImageURL, MOBILE);
                    }
                    else if (userAgent == BusinessResource.TABLET)
                    {
                        bookedActivities.ThumbnailImage = string.Format("Images/{0}{1}", thumbnailImageURL, MOBILE);
                    }
                    else
                    {
                        bookedActivities.ThumbnailImage = string.Format("Images/{0}{1}", thumbnailImageURL, MOBILE);
                    }
                    result.Add(bookedActivities);
                }
                return result;
            });
        }

        public IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocationAndType(string locationKey, string activityCategoryKey, string userAgent)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesMasterRepository activitiesMasterRepository
                = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

                IEnumerable<ActivitySummaryDataContract> allActivitiesForLocationCategory = activitiesMasterRepository.GetAllActivitiesByLocationCategory(locationKey: locationKey, activityCategoryKey: activityCategoryKey, userAgent: userAgent);
                return allActivitiesForLocationCategory;
            });
        }

        public IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocationFilteredCategory(string locationKey, string activityCategoryKey, DateTime startDate, DateTime endDate, string userAgent)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesMasterRepository activitiesMasterRepository
                = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

                IEnumerable<ActivitySummaryDataContract> allActivitiesForLocationCategory = activitiesMasterRepository.GetAllActivitiesByLocationFilteredCategory(locationKey: locationKey, activityCategoryKey: activityCategoryKey, startDate: startDate, endDate: endDate, userAgent: userAgent);
                return allActivitiesForLocationCategory;
            });
        }

        public bool RemoveSelectedActivity(string sessionKey, string activityBookingKey)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivityBookingRepository activitiesBookingRepository
                = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();
                IActivitiesBookingEngine activitiesBookingEngine
                   = _BusinessEngineFactory.GetBusinessEngine<IActivitiesBookingEngine>();

                ActivityBooking bookedActivity = activitiesBookingEngine.GetBookedActivitiesForUser(sessionKey, default(string)).Where(e => e.ActivityBookingKey == activityBookingKey).FirstOrDefault();
                activitiesBookingRepository.Remove(bookedActivity);
                return true;
            });
        }


        public IEnumerable<ActivityCategoryMaster> GetAllActivityCategories()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivityCategoryMasterRepository activityCategoryMasterRepository
                = _DataRepositoryFactory.GetDataRepository<IActivityCategoryMasterRepository>();

                IEnumerable<ActivityCategoryMaster> allActivityCategory = activityCategoryMasterRepository.Get().OrderBy(entity => entity.ActivityCategory);
                return allActivityCategory;
            });
        }

        public IEnumerable<ActivityTypeMaster> GetAllActivitySubCategories()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivityTypeMasterRepository activitySubCategoryRepository
                = _DataRepositoryFactory.GetDataRepository<IActivityTypeMasterRepository>();
                IEnumerable<ActivityTypeMaster> allActivitiesTypes = activitySubCategoryRepository.Get().OrderBy(entity => entity.ActivityType);
                return allActivitiesTypes;
            });
        }


        public void SaveCategories(ActivityCategoryMaster activityCategory)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IActivityCategoryMasterRepository activityCategoryMasterRepository
                = _DataRepositoryFactory.GetDataRepository<IActivityCategoryMasterRepository>();

                if (activityCategory.ActivityCategoryKey.Trim() == string.Empty || activityCategoryMasterRepository.Get(activityCategory.ActivityCategoryKey) == null)
                {
                    activityCategory.ActivityCategoryKey = Guid.NewGuid().ToString();
                    activityCategoryMasterRepository.Add(activityCategory);
                }
                else
                {
                    ActivityCategoryMaster existingActivityCategory = activityCategoryMasterRepository.Get(activityCategory.ActivityCategoryKey);
                    if (existingActivityCategory.ActivityCategory != activityCategory.ActivityCategory)
                    {
                        activityCategoryMasterRepository.Update(activityCategory);
                    }
                }
            });
        }

        public void SaveSubCategories(ActivityTypeMaster activitySubCategory)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IActivityTypeMasterRepository activitySubCategoryMasterRepository
                = _DataRepositoryFactory.GetDataRepository<IActivityTypeMasterRepository>();

                if (activitySubCategory.ActivityTypeKey.Trim() == string.Empty || activitySubCategoryMasterRepository.Get(activitySubCategory.ActivityTypeKey) == null)
                {
                    activitySubCategory.ActivityTypeKey = Guid.NewGuid().ToString();
                    activitySubCategoryMasterRepository.Add(activitySubCategory);
                }
                else
                {
                    ActivityTypeMaster existingActivityCategory = activitySubCategoryMasterRepository.Get(activitySubCategory.ActivityTypeKey);
                    if (existingActivityCategory.ActivityType != activitySubCategory.ActivityType)
                    {
                        activitySubCategoryMasterRepository.Update(activitySubCategory);
                    }
                }
            });
        }

        public IEnumerable<ActivityTypeMaster> GetSubCategoriesForSelectedActivity(string activityCategoryKey)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivityTypeMasterRepository activitySubCategoryRepository
                = _DataRepositoryFactory.GetDataRepository<IActivityTypeMasterRepository>();

                return activitySubCategoryRepository.GetAllTypesForSelectedCategory(activityCategoryKey);
            });
        }
        public void SaveActivityCategoryMapping(IEnumerable<string> activityTypeKeys, string activityCategoryKey)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IActivityTypeCategoryRepository activityTypeCategoryRepository
                = _DataRepositoryFactory.GetDataRepository<IActivityTypeCategoryRepository>();

                IEnumerable<ActivityTypeCategory> currentMappedTypes = activityTypeCategoryRepository.Get().Where(e => e.ActivityCategoryKey == activityCategoryKey);
                //creating new mappings
                foreach(var activityTypeKey in activityTypeKeys)
                {
                    if (!(currentMappedTypes.Where(e => e.ActivityTypeKey == activityTypeKey).Count() > 0))
                    {
                        ActivityTypeCategory newType = new ActivityTypeCategory() { ActivityTypeCategoryKey = Guid.NewGuid().ToString(), ActivityTypeKey = activityTypeKey, ActivityCategoryKey = activityCategoryKey, IsPrimary = false };
                        activityTypeCategoryRepository.Add(newType);
                    }
                }
                //Removing all mappings that have been deleted
                foreach (var item in currentMappedTypes)
                {
                    if (!activityTypeKeys.Contains(item.ActivityTypeKey))
                    {
                        activityTypeCategoryRepository.Remove(item);
                    }
                }
            });
        }
    }
}
