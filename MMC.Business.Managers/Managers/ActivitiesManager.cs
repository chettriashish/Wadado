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
using System.Security.Cryptography;

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
        const string MMC = "MMC";
        const string ALL = "All";
        const string ACTIVITY = "Activity";
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
                ITopOffersRepository topOffersRepository = _DataRepositoryFactory.GetDataRepository<ITopOffersRepository>();
                IActivityImagesRepository activityImageRepository = _DataRepositoryFactory.GetDataRepository<IActivityImagesRepository>();
                ITopOfferMappingRepository topOffersMappingRepository = _DataRepositoryFactory.GetDataRepository<ITopOfferMappingRepository>();
                ActivityDetailsDataContract allActivitiesForLocation = activitiesMasterRepository.GetActivityByLocation(locationKey: locationKey, activityKey: activityKey, userAgent: userAgent);
                IEnumerable<TopOffers> topOfferForSelectedActivity =  topOffersRepository.GetOffersForActivity(activityKey);
                IEnumerable<TopOfferMapping> allTopOffersMapping = topOffersMappingRepository.GetAllTopActivitiesOfferForSelectedLocation(locationKey);
                List<TopOffersDataContract> offers = new List<TopOffersDataContract>();
                foreach (var item in topOfferForSelectedActivity)
                {
                    if (allTopOffersMapping.Any(e => e.TopOfferKey == item.TopOffersKey))
                    {
                        TopOfferMapping offerMapping = allTopOffersMapping.FirstOrDefault(e => e.TopOfferKey == item.TopOffersKey);                        
                        ActivityImages img = activityImageRepository.GetImagesForSelectedActivity(allActivitiesForLocation.ActivityKey).FirstOrDefault(e => e.IsDefault == true);                        
                        TopOffersDataContract topOffer = new TopOffersDataContract
                        {
                            TopOffersKey = item.TopOffersKey,
                            Discount = offerMapping.Discount,                            
                            ImageURL = string.Format("Images/{0}", img.ImageURL),
                            Currency = allActivitiesForLocation.Currency,
                            Location = allActivitiesForLocation.Location,
                            Key = offerMapping.MappingType,
                            OfferType = ACTIVITY,
                            Value = allActivitiesForLocation.Name,
                            Rating = allActivitiesForLocation.UserRating
                        };
                        offers.Add(topOffer);
                    }                  
                }
                if (topOfferForSelectedActivity.Count() > 0)
                {
                    allActivitiesForLocation.IsTopOffer = true;
                    allActivitiesForLocation.AllTopOffers = offers;
                }
                else
                {
                    allActivitiesForLocation.IsTopOffer = false;
                    allActivitiesForLocation.AllTopOffers = new List<TopOffersDataContract>();
                }
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

                ActivitiesMaster activity = activitiesMasterRepository.Get(activityKey);

                IEnumerable<ActivityBooking> allBookedActivites = activityBookingRepository.Get();

                return activitiesBookingEngine.IsActivityAvailable(activityKey, bookingDate, time, allBookedActivites, adults, children, activity);
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
                activityBooking.BookingNumber = string.Format("{0}{1}", MMC, GetUniqueKey());
                activityBooking.ActivityPricingKey = bookingDetails.ActivityPricingKey;
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
                IActivityPriceMappingRepository priceMappingRepository = _DataRepositoryFactory.GetDataRepository<IActivityPriceMappingRepository>();
                ILocationsMasterRepository locationRepository = _DataRepositoryFactory.GetDataRepository<ILocationsMasterRepository>();
                List<ActivityBookingDataContract> result = new List<ActivityBookingDataContract>();
                foreach (var item in allBookedActivites)
                {
                    ActivityBookingDataContract bookedActivities = new ActivityBookingDataContract();
                    bookedActivities.ActivityBookingKey = item.ActivityBookingKey;
                    bookedActivities.ActivityKey = item.ActivityKey;
                    bookedActivities.Currency = activitiesRepository.Get(item.ActivityKey).Currency;
                    bookedActivities.Cost = priceMappingRepository.Get(item.ActivityPricingKey).PriceForAdults;
                    bookedActivities.CostChild = priceMappingRepository.Get(item.ActivityPricingKey).PriceForChildren;
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

        public IEnumerable<EmailDataContract> GetUsersBookingDetails(string sessionKey, string userAgent)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesBookingEngine activitiesBookingEngine
                    = _BusinessEngineFactory.GetBusinessEngine<IActivitiesBookingEngine>();

                IActivitiesMasterRepository activitiesRepository = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();
                IActivityLocationRepository activityLocationRepository = _DataRepositoryFactory.GetDataRepository<IActivityLocationRepository>();
                IEnumerable<ActivityBooking> allBookedActivites = activitiesBookingEngine.GetBookedActivitiesForUser(sessionKey, default(string));
                IActivityImagesRepository imageRepository = _DataRepositoryFactory.GetDataRepository<IActivityImagesRepository>();
                IActivityPriceMappingRepository priceMappingRepository = _DataRepositoryFactory.GetDataRepository<IActivityPriceMappingRepository>();
                ILocationsMasterRepository locationRepository = _DataRepositoryFactory.GetDataRepository<ILocationsMasterRepository>();
                IActivityCompanyRepository activityCompanyMasterRepository = _DataRepositoryFactory.GetDataRepository<IActivityCompanyRepository>();
                List<EmailDataContract> result = new List<EmailDataContract>();
                foreach (var item in allBookedActivites)
                {
                    EmailDataContract bookedActivities = new EmailDataContract();                                        
                    bookedActivities.Currency = activitiesRepository.Get(item.ActivityKey).Currency;                   
                    bookedActivities.ActivityName = activitiesRepository.Get(item.ActivityKey).Name;
                    bookedActivities.Address = locationRepository.Get(activityLocationRepository.Get().
                        Where(entity => entity.ActivityKey == item.ActivityKey).FirstOrDefault().LocationKey).LocationName.ToLower();
                    bookedActivities.BookingDate = item.BookingDate;
                    bookedActivities.ChildParticipants = item.ChildParticipants;
                    bookedActivities.Participants = item.Participants;                    
                    bookedActivities.Time = item.Time;                    
                    bookedActivities.PaymentAmount = item.PaymentAmount;
                    bookedActivities.Email = item.Email;
                    bookedActivities.Duration = activitiesRepository.Get(item.ActivityKey).Duration;
                    bookedActivities.ThingsToCarry = activitiesRepository.Get(item.ActivityKey).ThingsToCarry;
                    bookedActivities.CancellationPolicy = activitiesRepository.Get(item.ActivityKey).CancellationPolicy;
                    bookedActivities.BookingNumber = item.BookingNumber;
                    bookedActivities.PriceOption = priceMappingRepository.Get(item.ActivityPricingKey).OptionDescription;
                    bookedActivities.ContactNumber = activityCompanyMasterRepository.GetCompanyByActivity(item.ActivityKey).TelephoneNumber;
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

        public IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocation(string locationKey, string userAgent)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesMasterRepository activitiesMasterRepository
                = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

                IEnumerable<ActivitySummaryDataContract> allActivitiesForLocationCategory = activitiesMasterRepository.GetAllActivitiesByLocation(locationKey: locationKey, userAgent: userAgent);
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
                foreach (var activityTypeKey in activityTypeKeys)
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

        public void SaveActivityDetails(ActivityDetailsDataContract activity, Dictionary<string, bool> activityDays,
            IEnumerable<string> activityTimes, string locationKey, string activityTypeKey, string user)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IActivitiesMasterRepository activitiesRepository = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();
                IActivityDaySchedulerRepository activityDaySchedulerRepository = _DataRepositoryFactory.GetDataRepository<IActivityDaySchedulerRepository>();
                IActivityTimeSchedulerRepository activityTimeSchedulerRepository = _DataRepositoryFactory.GetDataRepository<IActivityTimeSchedulerRepository>();
                IActivityDatesRepository activityDatesRepository = _DataRepositoryFactory.GetDataRepository<IActivityDatesRepository>();
                IActivityPriceMappingRepository activityPriceMappingRepository = _DataRepositoryFactory.GetDataRepository<IActivityPriceMappingRepository>();
                IActivityTagMappingRepository activityTagRepository = _DataRepositoryFactory.GetDataRepository<IActivityTagMappingRepository>();
                ActivitiesMaster activitiesMaster = new ActivitiesMaster();
                activitiesMaster.ActivityEndTime = activity.ActivityEndTime;
                activitiesMaster.ActivityLocation = activity.LatLong;
                activitiesMaster.ActivityStartTime = activity.ActivityStartTime;
                activitiesMaster.CancellationPolicy = activity.CancellationPolicy;
                activitiesMaster.Address = activity.Location;
                activitiesMaster.AverageUserRating = activity.UserRating;
                activitiesMaster.DifficultyRating = activity.DifficultyRating;
                activitiesMaster.AllowInstantBooking = activity.AllowInstantBooking;
                activitiesMaster.Currency = activity.Currency;
                activitiesMaster.Description = activity.Description;
                activitiesMaster.MinAdults = activity.MinPeople;
                activitiesMaster.MinChildren = activity.MinChildren;
                activitiesMaster.Name = activity.Name;
                activitiesMaster.CostForChild = activity.CostForChild;
                activitiesMaster.Duration = activity.Duration;
                activitiesMaster.MaxAdults = activity.NumAdults;
                activitiesMaster.MaxChildren = activity.NumChildren;                
                activitiesMaster.NumAdults = activity.NumAdults;
                activitiesMaster.NumChildren = activity.NumChildren;
                activitiesMaster.LocationKey = locationKey;
                activitiesMaster.ActivityTypeKey = activityTypeKey;
                activitiesMaster.CreatedDate = DateTime.Now;
                activitiesMaster.CreatedBy = user;
                activitiesMaster.IsPermitRequired = activity.IsPermitRequired;
                activitiesMaster.Included = activity.Included;
                activitiesMaster.Advice = activity.Advice;
                activitiesMaster.Comission = activity.Comission;
                activitiesMaster.ThingsToCarry = activity.ThingsToCarry;
                if (activity.ActivityKey == default(string))
                {
                    activitiesMaster.ActivitesKey = Guid.NewGuid().ToString();
                    //This has to be changed later
                    activitiesMaster.IsValidated = true;
                    activitiesRepository.Add(activitiesMaster);
                }
                else
                {
                    activitiesMaster.ActivitesKey = activity.ActivityKey;
                    //This has to be changed later
                    activitiesMaster.IsValidated = true;
                    activitiesRepository.Update(activitiesMaster);
                }
                //Getting existing tags and removing it
                List<ActivityTagMapping> activityTags = activityTagRepository.GetTagsForSelectedActivity(activity.ActivityKey);
                if (activityTags.Count() > 0)
                {
                    foreach (var item in activityTags)
                    {
                        activityTagRepository.Remove(item);
                    }
                }
                //Adding fresh tags
                if (activity.Tags.Count() > 0)
                {
                    foreach (var item in activity.Tags)
                    {
                        ActivityTagMapping tag = new ActivityTagMapping
                        {
                            ActivityTagKey = Guid.NewGuid().ToString(),
                            ActivityKey = activity.ActivityKey,
                            Tag = item
                        };
                        activityTagRepository.Add(tag);
                    }
                }
                ActivityDayScheduler dayScheduler = activityDaySchedulerRepository.Get().Where(e => e.ActivityKey == activitiesMaster.ActivitesKey).FirstOrDefault();
                if (dayScheduler == null)
                {
                    dayScheduler = new ActivityDayScheduler();
                    dayScheduler.ActivityDaySchedulerKey = Guid.NewGuid().ToString();
                    dayScheduler.ActivityKey = activitiesMaster.ActivitesKey;
                }


                IEnumerable<ActivityPriceMapping> allPriceMappings = activityPriceMappingRepository.GetAllPriceOptionsForSelectedActivity(activitiesMaster.ActivitesKey);

                if (allPriceMappings != null)
                {
                    foreach (var item in allPriceMappings)
                    {
                        if (activity.AllPriceOptions != null && activity.AllPriceOptions.Any(e => e.ActivityPricingKey == item.ActivityPricingKey))
                        {
                            item.CommissionAmount = activity.AllPriceOptions.FirstOrDefault(e => e.ActivityPricingKey == item.ActivityPricingKey).CommissionAmount;
                            item.CommissionPercentage = activity.AllPriceOptions.FirstOrDefault(e => e.ActivityPricingKey == item.ActivityPricingKey).CommissionPercentage;
                            item.CreatedDate = activity.AllPriceOptions.FirstOrDefault(e => e.ActivityPricingKey == item.ActivityPricingKey).CreatedDate;
                            item.CreatedBy = activity.AllPriceOptions.FirstOrDefault(e => e.ActivityPricingKey == item.ActivityPricingKey).CreatedBy;
                            item.NumAdults = activity.AllPriceOptions.FirstOrDefault(e => e.ActivityPricingKey == item.ActivityPricingKey).NumAdults;
                            item.NumChild = activity.AllPriceOptions.FirstOrDefault(e => e.ActivityPricingKey == item.ActivityPricingKey).NumChild;
                            item.OptionDescription = activity.AllPriceOptions.FirstOrDefault(e => e.ActivityPricingKey == item.ActivityPricingKey).OptionDescription;
                            item.PriceForAdults = activity.AllPriceOptions.FirstOrDefault(e => e.ActivityPricingKey == item.ActivityPricingKey).PriceForAdults;
                            item.PriceForChildren = activity.AllPriceOptions.FirstOrDefault(e => e.ActivityPricingKey == item.ActivityPricingKey).PriceForChildren;
                            activityPriceMappingRepository.Update(item);
                        }
                        else
                        {
                            activityPriceMappingRepository.Remove(item);
                        }
                    }
                }

                if (activity.AllPriceOptions != null && activity.AllPriceOptions.Count() > 0)
                {
                    foreach (var item in activity.AllPriceOptions)
                    {
                        if (string.IsNullOrEmpty(item.ActivityPricingKey))
                        {
                            item.ActivityPricingKey = Guid.NewGuid().ToString();
                            item.ActivityKey = activitiesMaster.ActivitesKey;
                            item.CreatedBy = user;
                            item.CreatedDate = DateTime.Now;
                            activityPriceMappingRepository.Add(item);
                        }                        
                    }
                }

                if (!activity.IsEvent)
                {
                    int count = 0;
                    foreach (var day in activityDays)
                    {
                        switch (count)
                        {
                            case 0: dayScheduler.IsSunday = day.Value; break;
                            case 1: dayScheduler.IsMonday = day.Value; break;
                            case 2: dayScheduler.IsTuesday = day.Value; break;
                            case 3: dayScheduler.IsWednesday = day.Value; break;
                            case 4: dayScheduler.IsThursday = day.Value; break;
                            case 5: dayScheduler.IsFriday = day.Value; break;
                            case 6: dayScheduler.IsSaturday = day.Value; break;
                        }
                        count++;
                    }

                    if (activityDaySchedulerRepository.Get(dayScheduler.ActivityDaySchedulerKey) != null)
                    {
                        activityDaySchedulerRepository.Update(dayScheduler);
                    }
                    else
                    {
                        activityDaySchedulerRepository.Add(dayScheduler);
                    }

                    List<ActivityTimeScheduler> timeScheduler = activityTimeSchedulerRepository.Get().Where(e => e.ActivityKey == activitiesMaster.ActivitesKey).ToList();
                    if (timeScheduler != null && timeScheduler.Count() > 0)
                    {
                        foreach (ActivityTimeScheduler item in timeScheduler)
                        {
                            activityTimeSchedulerRepository.Remove(item);
                        }
                    }
                    //setting activity times after clearing the old time values
                    //creating new activiy times
                    timeScheduler = new List<ActivityTimeScheduler>();
                    foreach (string time in activityTimes)
                    {
                        ActivityTimeScheduler newTime = new ActivityTimeScheduler() { ActivityTimeSchedulerKey = Guid.NewGuid().ToString() };
                        newTime.ActivityKey = activitiesMaster.ActivitesKey;
                        newTime.ActivityTime = time;
                        activityTimeSchedulerRepository.Add(newTime);
                    }
                }
                else
                {
                    List<ActivityDates> allActivityDates = activityDatesRepository.Get().Where(e => e.ActivityKey == activitiesMaster.ActivitesKey).ToList();
                    if (allActivityDates.Count() > 0)
                    {
                        foreach (var item in allActivityDates)
                        {
                            activityDatesRepository.Remove(item);
                        }
                    }
                    foreach (var item in activity.AllActivityUniqueDates)
                    {
                        item.ActivityDatesKey = Guid.NewGuid().ToString();
                        item.ActivityKey = activitiesMaster.ActivitesKey;
                        activityDatesRepository.Add(item);
                    }
                }
                if (activity.AllTopOffers.Count() > 0)
                {
                    ITopOffersRepository topOffersRepository = _DataRepositoryFactory.GetDataRepository<ITopOffersRepository>();
                    ITopOfferMappingRepository topOffersMappingRepository = _DataRepositoryFactory.GetDataRepository<ITopOfferMappingRepository>();
                    List<TopOfferMapping> updateOfferMapping = new List<TopOfferMapping>();
                    foreach (var item in activity.AllTopOffers)
                    {
                        if (topOffersRepository.Get(item.TopOffersKey) != null)
                        {
                            TopOffers offer = topOffersRepository.Get(item.TopOffersKey);
                            offer.ImageUrl = activity.ImageURL;
                            offer.OfferEndDate = item.OfferEndDate;
                            offer.OfferStartDate = item.OfferStartDate;
                            offer.LocationKey = locationKey;

                            IEnumerable<TopOfferMapping> offerMapping = topOffersMappingRepository.Get().Where(e => e.TopOfferKey == item.TopOffersKey && e.MappingKey == activity.ActivityKey);
                            foreach (var mapping in offerMapping)
                            {
                                topOffersMappingRepository.Remove(mapping);
                            }
                            TopOfferMapping mappingOffer = new TopOfferMapping
                            {
                                TopOfferKey = item.TopOffersKey,
                                MappingKey = activity.ActivityKey,
                                Discount = item.Discount,
                                IsDeleted = false,
                                MappingType = ACTIVITY,
                                TopOfferMappingKey = Guid.NewGuid().ToString()
                            };
                            updateOfferMapping.Add(mappingOffer);
                        }
                    }
                    //Adding all top offer mapping
                    topOffersMappingRepository.AddAll(updateOfferMapping);                    
                }               
            });
        }


        private string GetUniqueKey()
        {
            int maxSize = 8;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            for (int i = 0; i < maxSize; i++)
            {
                byte b = data[i];
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString().ToUpper();
        }

        public IEnumerable<ActivityBookingDataContract> GetAllActivitiesPendingForConfirmation()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivityBookingRepository activityBookingRepository
                  = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();
                IActivitiesMasterRepository activitiesMasterRepository = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();
                IEnumerable<ActivityBooking> allBookings = activityBookingRepository.GetAllActivitiesPendingForConfirmation();
                List<ActivityBookingDataContract> activityBookingList = new List<ActivityBookingDataContract>();
                foreach (var activityBooking in allBookings)
                {
                    ActivityBookingDataContract bookingContract = new ActivityBookingDataContract();
                    ActivitiesMaster activity = activitiesMasterRepository.Get(activityBooking.ActivityKey);
                    bookingContract.ActivityBookingKey = activityBooking.ActivityBookingKey;
                    bookingContract.ActivityKey = activityBooking.ActivityKey;
                    bookingContract.ActivityName = activity.Name;
                    bookingContract.BookingDate = activityBooking.BookingDate;
                    bookingContract.BookingNumber = activityBooking.BookingNumber;
                    bookingContract.ChildParticipants = activityBooking.ChildParticipants;
                    bookingContract.Participants = activityBooking.Participants;
                    bookingContract.PaymentAmount = activityBooking.PaymentAmount;
                    bookingContract.Currency = activity.Currency;
                    bookingContract.Cost = activityBooking.PaymentAmount;
                    bookingContract.Time = activityBooking.Time;
                    activityBookingList.Add(bookingContract);
                }

                return activityBookingList.OrderBy(e => e.BookingDate);
            });
        }

        public bool AcceptSelectedActivityBooking(string bookingKey, string user)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivityBookingRepository activityBookingRepository = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();
                ActivityBooking selectedBooking = activityBookingRepository.Get(bookingKey);
                selectedBooking.IsConfirmed = true;
                selectedBooking.ConfirmationDate = DateTime.Now;
                selectedBooking.IsPaymentComplete = true; 
                selectedBooking.ConfirmedBy = user;
                activityBookingRepository.Update(selectedBooking);
                return true;
            });
        }
        public bool RejectSelectedActivityBooking(string bookingKey, string user)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivityBookingRepository activityBookingRepository = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();
                ActivityBooking selectedBooking = activityBookingRepository.Get(bookingKey);
                selectedBooking.IsCancelled = true;
                selectedBooking.IsDeleted = true;
                selectedBooking.ConfirmationDate = DateTime.Now;
                selectedBooking.ConfirmedBy = user;
                activityBookingRepository.Update(selectedBooking);
                return true;
            });
        }
        public IEnumerable<CompanyMaster> GetAllRegisteredCompanies()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                ICompanyMasterRepository companyMasterRepository
                  = _DataRepositoryFactory.GetDataRepository<ICompanyMasterRepository>();
                List<CompanyMaster> results = companyMasterRepository.Get().ToList();

                CompanyMaster company = new CompanyMaster
                {
                    CompanyKey = ALL,
                    Name = ALL,
                    ContactPerson = ALL
                };
                results.Insert(0, company);
                return results;
            });
        }

        public IEnumerable<ActivityBookingDataContract> GetAllCompanyActivitiesPendingForConfirmation(string companyKey)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivityBookingRepository activityBookingRepository
                  = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();
                IActivitiesMasterRepository activitiesMasterRepository =
                    _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

                IEnumerable<ActivityBooking> allBookings = new List<ActivityBooking>();

                if (companyKey == ALL)
                {
                    allBookings = activityBookingRepository.GetAllActivitiesPendingForConfirmation();
                }
                else
                {
                    allBookings = activityBookingRepository.GetAllCompanyActivitiesPendingForConfirmation(companyKey);
                }
                List<ActivityBookingDataContract> activityBookingList = new List<ActivityBookingDataContract>();
                foreach (var activityBooking in allBookings)
                {
                    ActivityBookingDataContract bookingContract = new ActivityBookingDataContract();
                    ActivitiesMaster activity = activitiesMasterRepository.Get(activityBooking.ActivityKey);
                    bookingContract.ActivityBookingKey = activityBooking.ActivityBookingKey;
                    bookingContract.ActivityKey = activityBooking.ActivityKey;
                    bookingContract.ActivityName = activity.Name;
                    bookingContract.BookingDate = activityBooking.BookingDate;
                    bookingContract.BookingNumber = activityBooking.BookingNumber;
                    bookingContract.ChildParticipants = activityBooking.ChildParticipants;
                    bookingContract.Participants = activityBooking.Participants;
                    bookingContract.PaymentAmount = activityBooking.PaymentAmount;
                    bookingContract.Currency = activity.Currency;
                    bookingContract.Cost = activityBooking.PaymentAmount;
                    bookingContract.Time = activityBooking.Time;
                    activityBookingList.Add(bookingContract);
                }
                return activityBookingList.OrderBy(e => e.BookingDate);
            });
        }

        public IEnumerable<ActivityBookingDataContract> GetAllActivitiesCompleted()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivityBookingRepository activityBookingRepository
                  = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();
                IActivitiesMasterRepository activitiesMasterRepository = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();
                IEnumerable<ActivityBooking> allBookings = activityBookingRepository.GetAllActivitiesCompleted();
                List<ActivityBookingDataContract> activityBookingList = new List<ActivityBookingDataContract>();
                foreach (var activityBooking in allBookings)
                {
                    ActivityBookingDataContract bookingContract = new ActivityBookingDataContract();
                    ActivitiesMaster activity = activitiesMasterRepository.Get(activityBooking.ActivityKey);
                    bookingContract.ActivityBookingKey = activityBooking.ActivityBookingKey;
                    bookingContract.ActivityKey = activityBooking.ActivityKey;
                    bookingContract.ActivityName = activity.Name;
                    bookingContract.BookingDate = activityBooking.BookingDate;
                    bookingContract.BookingNumber = activityBooking.BookingNumber;
                    bookingContract.ChildParticipants = activityBooking.ChildParticipants;
                    bookingContract.Participants = activityBooking.Participants;
                    bookingContract.PaymentAmount = activityBooking.PaymentAmount;
                    bookingContract.Currency = activity.Currency;
                    bookingContract.Cost = activityBooking.PaymentAmount;
                    bookingContract.Time = activityBooking.Time;
                    activityBookingList.Add(bookingContract);
                }

                return activityBookingList.OrderBy(e => e.BookingDate);
            });
        }
        public IEnumerable<ActivityBookingDataContract> GetAllCompanyActivitiesCompleted(string companyKey)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivityBookingRepository activityBookingRepository
                  = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();
                IActivitiesMasterRepository activitiesMasterRepository =
                    _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

                IEnumerable<ActivityBooking> allBookings = new List<ActivityBooking>();

                if (companyKey == ALL)
                {
                    allBookings = activityBookingRepository.GetAllActivitiesCompleted();
                }
                else
                {
                    allBookings = activityBookingRepository.GetAllCompanyActivitiesCompleted(companyKey);
                }
                List<ActivityBookingDataContract> activityBookingList = new List<ActivityBookingDataContract>();
                foreach (var activityBooking in allBookings)
                {
                    ActivityBookingDataContract bookingContract = new ActivityBookingDataContract();
                    ActivitiesMaster activity = activitiesMasterRepository.Get(activityBooking.ActivityKey);
                    bookingContract.ActivityBookingKey = activityBooking.ActivityBookingKey;
                    bookingContract.ActivityKey = activityBooking.ActivityKey;
                    bookingContract.ActivityName = activity.Name;
                    bookingContract.BookingDate = activityBooking.BookingDate;
                    bookingContract.BookingNumber = activityBooking.BookingNumber;
                    bookingContract.ChildParticipants = activityBooking.ChildParticipants;
                    bookingContract.Participants = activityBooking.Participants;
                    bookingContract.PaymentAmount = activityBooking.PaymentAmount;
                    bookingContract.Currency = activity.Currency;
                    bookingContract.Cost = activityBooking.PaymentAmount;
                    bookingContract.Time = activityBooking.Time;
                    activityBookingList.Add(bookingContract);
                }
                return activityBookingList.OrderBy(e => e.BookingDate);
            });
        }
        public IEnumerable<ActivityBookingDataContract> GetAllUpcomingActivities()
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivityBookingRepository activityBookingRepository
                  = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();
                IActivitiesMasterRepository activitiesMasterRepository = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();
                IEnumerable<ActivityBooking> allBookings = activityBookingRepository.GetAllUpcomingActivities();
                List<ActivityBookingDataContract> activityBookingList = new List<ActivityBookingDataContract>();
                foreach (var activityBooking in allBookings)
                {
                    ActivityBookingDataContract bookingContract = new ActivityBookingDataContract();
                    ActivitiesMaster activity = activitiesMasterRepository.Get(activityBooking.ActivityKey);
                    bookingContract.ActivityBookingKey = activityBooking.ActivityBookingKey;
                    bookingContract.ActivityKey = activityBooking.ActivityKey;
                    bookingContract.ActivityName = activity.Name;
                    bookingContract.BookingDate = activityBooking.BookingDate;
                    bookingContract.BookingNumber = activityBooking.BookingNumber;
                    bookingContract.ChildParticipants = activityBooking.ChildParticipants;
                    bookingContract.Participants = activityBooking.Participants;
                    bookingContract.PaymentAmount = activityBooking.PaymentAmount;
                    bookingContract.Currency = activity.Currency;
                    bookingContract.Cost = activityBooking.PaymentAmount;
                    bookingContract.Time = activityBooking.Time;
                    activityBookingList.Add(bookingContract);
                }

                return activityBookingList.OrderBy(e => e.BookingDate);
            });
        }
        public IEnumerable<ActivityBookingDataContract> GetAllUpcomingCompanyActivities(string companyKey)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivityBookingRepository activityBookingRepository
                  = _DataRepositoryFactory.GetDataRepository<IActivityBookingRepository>();
                IActivitiesMasterRepository activitiesMasterRepository =
                    _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();

                IEnumerable<ActivityBooking> allBookings = new List<ActivityBooking>();

                if (companyKey == ALL)
                {
                    allBookings = activityBookingRepository.GetAllUpcomingActivities();
                }
                else
                {
                    allBookings = activityBookingRepository.GetAllUpcomingCompanyActivities(companyKey);
                }
                List<ActivityBookingDataContract> activityBookingList = new List<ActivityBookingDataContract>();
                foreach (var activityBooking in allBookings)
                {
                    ActivityBookingDataContract bookingContract = new ActivityBookingDataContract();
                    ActivitiesMaster activity = activitiesMasterRepository.Get(activityBooking.ActivityKey);
                    bookingContract.ActivityBookingKey = activityBooking.ActivityBookingKey;
                    bookingContract.ActivityKey = activityBooking.ActivityKey;
                    bookingContract.ActivityName = activity.Name;
                    bookingContract.BookingDate = activityBooking.BookingDate;
                    bookingContract.BookingNumber = activityBooking.BookingNumber;
                    bookingContract.ChildParticipants = activityBooking.ChildParticipants;
                    bookingContract.Participants = activityBooking.Participants;
                    bookingContract.PaymentAmount = activityBooking.PaymentAmount;
                    bookingContract.Currency = activity.Currency;
                    bookingContract.Cost = activityBooking.PaymentAmount;
                    bookingContract.Time = activityBooking.Time;
                    activityBookingList.Add(bookingContract);
                }
                return activityBookingList.OrderBy(e => e.BookingDate);
            });
        }

        public IEnumerable<ActivitySearchDataContract> GetActivitiesForSelectedSearchTag(IEnumerable<string> tags)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivityTagMappingRepository tagMappingRepository = _DataRepositoryFactory.GetDataRepository<IActivityTagMappingRepository>();
                IEnumerable<ActivitiesMaster> allActivities =  tagMappingRepository.GetActivitiesForSelectedSearchTag(tags);
                List<ActivitySearchDataContract> results = new List<ActivitySearchDataContract>();
                foreach (var item in allActivities)
                {
                    ActivitySearchDataContract search = new ActivitySearchDataContract() { ActivityKey = item.ActivitesKey, ActivityName = item.Name, LocationKey = item.LocationKey };
                    results.Add(search);
                }
                return results;
            });            
        }

        public bool SaveActivityImages(string activityKey, List<string> images)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IActivitiesMasterRepository activityRepository = _DataRepositoryFactory.GetDataRepository<IActivitiesMasterRepository>();
                string locationKey = activityRepository.Get(activityKey).LocationKey;
                IActivityImagesRepository imageRepository = _DataRepositoryFactory.GetDataRepository<IActivityImagesRepository>();
                imageRepository.RemoveImagesForActivity(activityKey);
                imageRepository.AddImagesForActivity(activityKey, images, locationKey);
                return true;
            });
        }
    }
}
