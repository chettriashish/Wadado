using MMC.Business.Contracts.DataContracts;
using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivitiesMasterRepository : DataRepositoryBase<ActivitiesMaster>, IActivitiesMasterRepository
    {
        const string MOBILE = "_mob";
        const string TABLET = "_tab";
        const string THUMBNAIL = "_thumb";
        /// <summary>
        /// overriden methods from datarepositorybase class
        /// </summary>
        /// <param name="entityContext"></param>
        /// <param name="entity"></param>
        /// <returns></returns>

        protected override ActivitiesMaster AddEntity(MyMonkeyCapContext entityContext, ActivitiesMaster entity)
        {
            return entityContext.ActivitiesMasterSet.Add(entity);
        }

        protected override ActivitiesMaster UpdateEntity(MyMonkeyCapContext entityContext, ActivitiesMaster entity)
        {
            return (from e in entityContext.ActivitiesMasterSet
                    where e.ActivitesKey == entity.ActivitesKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivitiesMaster> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivitiesMasterSet
                    select e);
        }

        protected override ActivitiesMaster GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivitiesMasterSet
                         where e.ActivitesKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public ActivitiesMaster GetActivityByCompany(string companyId)
        {
            throw new NotImplementedException();
        }

        public ActivityDetailsDataContract GetActivityByLocation(string locationKey, string activityKey, string userAgent)
        {
            ActivityDetailsDataContract result = new ActivityDetailsDataContract();
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                ActivitiesMaster activity = new ActivitiesMaster();
                if (locationKey != default(string))
                {
                    //fetching all details for selected activity
                    activity = Get().Where(entity => entity.ActivitesKey == activityKey && entity.LocationKey == locationKey).ToList().FirstOrDefault();
                }
                else
                {
                    activity = Get().Where(entity => entity.ActivitesKey == activityKey).ToList().FirstOrDefault();
                }

                ActivityDayScheduler activityDayScheduler = entityContext.ActivityDaySchedulerSet.Where(entity =>
                                                            entity.ActivityKey == activityKey).FirstOrDefault();

                IEnumerable<ActivityTimeScheduler> activityTimeScheduler = entityContext.ActivityTimeSchedulerSet.
                    Where(entity => entity.ActivityKey == activityKey);


                ActivityCategoryMaster activityCategory = (from entity in entityContext.ActivityTypeCategorySet
                                                           join entity1 in entityContext.ActivityCategoryMasterSet
                                                           on entity.ActivityCategoryKey equals entity1.ActivityCategoryKey
                                                           where entity.ActivityTypeKey == activity.ActivityTypeKey
                                                           select entity1).FirstOrDefault();

                ActivityTypeMaster activityType = (from entity in entityContext.ActivityTypeMasterSet
                                                   where entity.ActivityTypeKey == activity.ActivityTypeKey
                                                   select entity).ToList().FirstOrDefault();

                IEnumerable<ActivityImages> activityImages = entityContext.ActivityImagesSet
                    .Where(entity => entity.ActivityKey == activityKey).ToList();

                result.ActivitySubCategory = activityType.ActivityType;
                result.ActivityCategory = activityCategory != null ? activityCategory.ActivityCategory : default(string);
                result.CancellationPolicy = activity.CancellationPolicy;
                result.Cost = activity.Cost;
                result.Currency = activity.Currency;
                result.Description = activity.Description;
                result.DifficultyRating = activity.DifficultyRating;
                result.Location = activity.Address;
                result.LocationLatLong = entityContext.LocationMasterSet.Where(e1 => e1.LocationKey == entityContext.ActivityLocationSet.Where(e => e.LocationKey == locationKey).FirstOrDefault().LocationKey).FirstOrDefault().LatLng;
                result.LatLong = activity.ActivityLocation;
                result.DistanceFromNearestCity = activity.DistanceFromNearestCity;
                result.NumAdults = activity.NumAdults;
                result.CostForChild = activity.CostForChild;
                result.NumChildren = activity.NumChildren;
                result.PermitRequired = activity.IsPermitRequired;
                result.MinPeople = activity.MinAdults;
                result.MaxPeople = activity.MaxAdults;
                result.Name = activity.Name;
                result.Duration = activity.Duration;
                result.UserRating = activity.AverageUserRating;
                result.AllActivityTimes = activityTimeScheduler.Select(entity => entity.ActivityTime).ToList();
                result.ActivityKey = activity.ActivitesKey;
                foreach (var item in activityImages)
                {
                    if (userAgent == RepositoryResource.SMARTPHONE)
                    {
                        if (!item.IsThumbnail)
                        {
                            item.ImageURL = string.Format("Images/{0}{1}", item.ImageURL, MOBILE);
                        }
                        else
                        {
                            item.ImageURL = string.Format("Images/{0}{1}{2}", item.ImageURL, MOBILE, THUMBNAIL);
                        }

                    }
                    else if (userAgent == RepositoryResource.TABLET)
                    {
                        if (!item.IsThumbnail)
                        {
                            item.ImageURL = string.Format("Images/{0}{1}", item.ImageURL, TABLET);
                        }
                        else
                        {
                            item.ImageURL = string.Format("Images/{0}{1}{2}", item.ImageURL, TABLET, THUMBNAIL);
                        }
                    }
                    else
                    {
                        if (!item.IsThumbnail)
                        {
                            item.ImageURL = string.Format("Images/{0}", item.ImageURL);
                        }
                        else
                        {
                            item.ImageURL = string.Format("Images/{0}{1}", item.ImageURL, THUMBNAIL);
                        }
                    }
                    if (item.IsDefault)
                    {
                        result.DefaultImageURL = item.ImageURL;
                    }
                }

                ///Getting list of similar activities

                result.SimilarActivities = (from entity in entityContext.ActivitiesMasterSet
                                            join entity1 in entityContext.ActivityTypeCategorySet
                                            on entity.ActivityTypeKey equals entity1.ActivityTypeKey
                                            where entity1.ActivityTypeKey == activity.ActivityTypeKey
                                            && entity.ActivitesKey != activity.ActivitesKey
                                            && entity.LocationKey == locationKey
                                            select new ActivitySummaryDataContract()
                                            {
                                                ActivityKey = entity.ActivitesKey,
                                                ActivityName = entity.Name,
                                                ImageURL = entityContext.ActivityImagesSet.Where(e => e.ActivityKey == entity.ActivitesKey && e.IsDefault == true).FirstOrDefault().ImageURL,
                                                Location = entityContext.LocationMasterSet.Where(e1 => e1.LocationKey == entityContext.ActivityLocationSet.Where(e => e.LocationKey == entity.LocationKey).FirstOrDefault().LocationKey).FirstOrDefault().LocationKey,
                                                Rating = entity.AverageUserRating,
                                                IsSpecialOffer = ((from e1 in entityContext.TopOffersSet
                                                                   join e2 in entityContext.TopOfferMappingSet
                                                                   on e1.TopOffersKey equals e2.TopOfferKey
                                                                   where e1.LocationKey == locationKey
                                                                   && e2.MappingType == RepositoryResource.SINGLE
                                                                   && (e1.OfferStartDate <= DateTime.Now && e1.OfferEndDate > DateTime.Now)
                                                                   select e2).Count() > 0 ? true : false),
                                                Cost = entity.Cost,
                                                Discount = ((from e1 in entityContext.TopOffersSet
                                                             join e2 in entityContext.TopOfferMappingSet
                                                             on e1.TopOffersKey equals e2.TopOfferKey
                                                             where e1.LocationKey == locationKey
                                                             && e2.MappingType == RepositoryResource.SINGLE
                                                             && (e1.OfferStartDate <= DateTime.Now && e1.OfferEndDate > DateTime.Now)
                                                             select e1).Count()) > 0 ?
                                                (from e1 in entityContext.TopOffersSet
                                                 join e2 in entityContext.TopOfferMappingSet
                                                 on e1.TopOffersKey equals e2.TopOfferKey
                                                 where e1.LocationKey == locationKey
                                                 && e2.MappingType == RepositoryResource.SINGLE
                                                 && (e1.OfferStartDate <= DateTime.Now && e1.OfferEndDate > DateTime.Now)
                                                 select e2).FirstOrDefault().Discount : 0
                                            }).Take(6).ToList();

                if (result.SimilarActivities.Count() < 6 && activityCategory != null)
                {
                    int activitiesToBeRetrieved = 6 - result.SimilarActivities.Count();
                    result.SimilarActivities = (from entity in entityContext.ActivitiesMasterSet
                                                join entity1 in entityContext.ActivityTypeCategorySet
                                                on entity.ActivityTypeKey equals entity1.ActivityTypeKey
                                                where entity1.ActivityCategoryKey == activityCategory.ActivityCategoryKey
                                                && entity.ActivitesKey != activity.ActivitesKey
                                                && entity.LocationKey == locationKey
                                                select new ActivitySummaryDataContract()
                                                {
                                                    ActivityKey = entity.ActivitesKey,
                                                    ActivityName = entity.Name,
                                                    ImageURL = entityContext.ActivityImagesSet.Where(e => e.ActivityKey == entity.ActivitesKey && e.IsDefault == true).FirstOrDefault().ImageURL,
                                                    Location = entityContext.LocationMasterSet.Where(e1 => e1.LocationKey == entityContext.ActivityLocationSet.Where(e => e.LocationKey == entity.LocationKey).FirstOrDefault().LocationKey).FirstOrDefault().LocationKey,
                                                    Rating = entity.AverageUserRating,
                                                    IsSpecialOffer = ((from e1 in entityContext.TopOffersSet
                                                                       join e2 in entityContext.TopOfferMappingSet
                                                                       on e1.TopOffersKey equals e2.TopOfferKey
                                                                       where e1.LocationKey == locationKey
                                                                       && e2.MappingType == RepositoryResource.SINGLE
                                                                       && (e1.OfferStartDate <= DateTime.Now && e1.OfferEndDate > DateTime.Now)
                                                                       select e2).Count() > 0 ? true : false),
                                                    Cost = entity.Cost,
                                                    Discount = ((from e1 in entityContext.TopOffersSet
                                                                 join e2 in entityContext.TopOfferMappingSet
                                                                 on e1.TopOffersKey equals e2.TopOfferKey
                                                                 where e1.LocationKey == locationKey
                                                                 && e2.MappingType == RepositoryResource.SINGLE
                                                                 && (e1.OfferStartDate <= DateTime.Now && e1.OfferEndDate > DateTime.Now)
                                                                 select e1).Count()) > 0 ?
                                                (from e1 in entityContext.TopOffersSet
                                                 join e2 in entityContext.TopOfferMappingSet
                                                 on e1.TopOffersKey equals e2.TopOfferKey
                                                 where e1.LocationKey == locationKey
                                                 && e2.MappingType == RepositoryResource.SINGLE
                                                 && (e1.OfferStartDate <= DateTime.Now && e1.OfferEndDate > DateTime.Now)
                                                 select e2).FirstOrDefault().Discount : 0
                                                }).Take(activitiesToBeRetrieved).ToList();
                }


                result.ActivityImages = activityImages.
                    Where(entity => entity.IsThumbnail == false).Select(entity => entity.ImageURL).ToList();
                result.ActivityImagesURL = activityImages.
                    Where(entity => entity.IsThumbnail == false).Select(entity => entity.ImageURL).ToList();
                result.ImageURL = result.DefaultImageURL;
                result.DifficultyLevel = GetDifficultyLevel(result.DifficultyRating);
                result.AllActivityDates = ConvertDays(activityDayScheduler);


                if (result.AllActivityDates.Count() == 0)
                {
                    result.NextAvaiableDate = (from entity in entityContext.ActivityDatesSet
                                               where entity.Date > DateTime.Now
                                               && entity.IsDeleted == false
                                               select entity).ToList().FirstOrDefault().Date;
                }
                else if (result.SimilarActivities != null)
                {
                    foreach (var item in result.SimilarActivities)
                    {
                        if (userAgent == RepositoryResource.SMARTPHONE)
                        {
                            item.ImageURL = string.Format("Images/{0}{1}", item.ImageURL, MOBILE);

                        }
                        else if (userAgent == RepositoryResource.TABLET)
                        {
                            item.ImageURL = string.Format("Images/{0}{1}", item.ImageURL, TABLET);
                        }
                        else
                        {
                            item.ImageURL = string.Format("Images/{0}", item.ImageURL);
                        }
                    }
                }
                return result;
            }
        }

        public List<int> ConvertDays(ActivityDayScheduler dayScheduler)
        {
            List<int> result = new List<int>();
            if (dayScheduler.IsMonday)
            {
                result.Add(1);
            }
            if (dayScheduler.IsTuesday)
            {
                result.Add(2);
            }
            if (dayScheduler.IsWednesday)
            {
                result.Add(3);
            }
            if (dayScheduler.IsThursday)
            {
                result.Add(4);
            }
            if (dayScheduler.IsFriday)
            {
                result.Add(5);
            }
            if (dayScheduler.IsSaturday)
            {
                result.Add(6);
            }
            if (dayScheduler.IsSunday)
            {
                result.Add(0);
            }
            return result;
        }

        private string GetDifficultyLevel(decimal difficultyRating)
        {
            switch (Convert.ToInt32(difficultyRating))
            {
                case 1:
                case 2: return RepositoryResource.EASY;
                case 3: return RepositoryResource.MEDIUM;
                case 4: return RepositoryResource.HARD;
                case 5: return RepositoryResource.EXTREME;
                default: return RepositoryResource.MEDIUM;
            }
        }

        public void AddActivityToUserItenerary(string activityKey, string activityDate, int numberOfPeople, string activityTime)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fetch user information based on user account details
        /// </summary>
        /// <param name="userAccountKey"></param>
        /// <returns></returns>
        public IEnumerable<ActivitiesMaster> GetAllActivitiesBooked(string userAccountKey)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocationCategory(string locationKey, string activityCategoryKey, string userAgent)
        {
            IEnumerable<ActivitySummaryDataContract> result = new List<ActivitySummaryDataContract>();
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                result = (from entity in entityContext.ActivitiesMasterSet
                          join entity1 in entityContext.ActivityTypeCategorySet
                          on entity.ActivityTypeKey equals entity1.ActivityTypeKey
                          where entity.LocationKey == locationKey
                          && entity1.ActivityCategoryKey == activityCategoryKey
                          select new ActivitySummaryDataContract()
                          {
                              ActivityKey = entity.ActivitesKey,
                              ActivityName = entity.Name,
                              ActivityType = entityContext.ActivityTypeMasterSet.Where(e => e.ActivityTypeKey == entity.ActivityTypeKey).FirstOrDefault().ActivityType,
                              ActivityCategory = entityContext.ActivityCategoryMasterSet.Where(e => e.ActivityCategoryKey == activityCategoryKey).FirstOrDefault().ActivityCategory,
                              ImageURL = entityContext.ActivityImagesSet.Where(e => e.ActivityKey == entity.ActivitesKey && e.IsDefault == true).FirstOrDefault().ImageURL,
                              Location = entityContext.LocationMasterSet.Where(e1 => e1.LocationKey == entityContext.ActivityLocationSet.Where(e => e.LocationKey == entity.LocationKey).FirstOrDefault().LocationKey).FirstOrDefault().LocationName,
                              Rating = entity.AverageUserRating,
                              ThumbNailURL = entityContext.ActivityImagesSet.Where(e => e.ActivityKey == entity.ActivitesKey && e.IsDefault == true).FirstOrDefault().ImageURL,
                              IsSpecialOffer = ((from e1 in entityContext.TopOffersSet
                                                 join e2 in entityContext.TopOfferMappingSet
                                                 on e1.TopOffersKey equals e2.TopOfferKey
                                                 where e1.LocationKey == locationKey
                                                 && e2.MappingType == RepositoryResource.SINGLE
                                                 && (e1.OfferStartDate <= DateTime.Now && e1.OfferEndDate > DateTime.Now)
                                                 select e2).Count() > 0 ? true : false),
                              LatLong = entity.ActivityLocation,
                              Cost = entity.Cost,
                              Currency = entity.Currency,
                              Discount = ((from e1 in entityContext.TopOffersSet
                                           join e2 in entityContext.TopOfferMappingSet
                                           on e1.TopOffersKey equals e2.TopOfferKey
                                           where e1.LocationKey == locationKey
                                           && e2.MappingType == RepositoryResource.SINGLE
                                           && (e1.OfferStartDate <= DateTime.Now && e1.OfferEndDate > DateTime.Now)
                                           select e1).Count()) > 0 ?
                                               (from e1 in entityContext.TopOffersSet
                                                join e2 in entityContext.TopOfferMappingSet
                                                on e1.TopOffersKey equals e2.TopOfferKey
                                                where e1.LocationKey == locationKey
                                                && e2.MappingType == RepositoryResource.SINGLE
                                                && (e1.OfferStartDate <= DateTime.Now && e1.OfferEndDate > DateTime.Now)
                                                select e2).FirstOrDefault().Discount : 0
                          }).ToList();

                foreach (var item in result)
                {
                    if (userAgent == RepositoryResource.SMARTPHONE)
                    {
                        item.ImageURL = string.Format("Images/{0}{1}", item.ImageURL, MOBILE);
                    }
                    else if (userAgent == RepositoryResource.TABLET)
                    {
                        item.ImageURL = string.Format("Images/{0}{1}", item.ImageURL, TABLET);
                    }
                    else
                    {
                        item.ImageURL = string.Format("Images/{0}", item.ImageURL);
                    }
                }
            }
            return result;
        }
        public IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocation(string locationKey, string userAgent)
        {
            IEnumerable<ActivitySummaryDataContract> result = new List<ActivitySummaryDataContract>();
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                result = (from entity in entityContext.ActivitiesMasterSet
                          where entity.LocationKey == locationKey
                          select new ActivitySummaryDataContract()
                          {
                              ActivityKey = entity.ActivitesKey,
                              ActivityName = entity.Name,
                              ActivityType = entityContext.ActivityTypeMasterSet.Where(e => e.ActivityTypeKey == entity.ActivityTypeKey).FirstOrDefault().ActivityType,
                              ImageURL = entityContext.ActivityImagesSet.Where(e => e.ActivityKey == entity.ActivitesKey && e.IsDefault == true).FirstOrDefault().ImageURL,
                              Location = entityContext.LocationMasterSet.Where(e1 => e1.LocationKey == entityContext.ActivityLocationSet.Where(e => e.LocationKey == entity.LocationKey).FirstOrDefault().LocationKey).FirstOrDefault().LocationName,
                              Rating = entity.AverageUserRating,
                              ThumbNailURL = entityContext.ActivityImagesSet.Where(e => e.ActivityKey == entity.ActivitesKey && e.IsDefault == true).FirstOrDefault().ImageURL,
                              IsSpecialOffer = ((from e1 in entityContext.TopOffersSet
                                                 join e2 in entityContext.TopOfferMappingSet
                                                 on e1.TopOffersKey equals e2.TopOfferKey
                                                 where e1.LocationKey == locationKey
                                                 && e2.MappingType == RepositoryResource.SINGLE
                                                 && (e1.OfferStartDate <= DateTime.Now && e1.OfferEndDate > DateTime.Now)
                                                 select e2).Count() > 0 ? true : false),
                              LatLong = entity.ActivityLocation,
                              Cost = entity.Cost,
                              Currency = entity.Currency,
                              Discount = ((from e1 in entityContext.TopOffersSet
                                           join e2 in entityContext.TopOfferMappingSet
                                           on e1.TopOffersKey equals e2.TopOfferKey
                                           where e1.LocationKey == locationKey
                                           && e2.MappingType == RepositoryResource.SINGLE
                                           && (e1.OfferStartDate <= DateTime.Now && e1.OfferEndDate > DateTime.Now)
                                           select e1).Count()) > 0 ?
                                               (from e1 in entityContext.TopOffersSet
                                                join e2 in entityContext.TopOfferMappingSet
                                                on e1.TopOffersKey equals e2.TopOfferKey
                                                where e1.LocationKey == locationKey
                                                && e2.MappingType == RepositoryResource.SINGLE
                                                && (e1.OfferStartDate <= DateTime.Now && e1.OfferEndDate > DateTime.Now)
                                                select e2).FirstOrDefault().Discount : 0
                          }).ToList();

                foreach (var item in result)
                {
                    if (userAgent == RepositoryResource.SMARTPHONE)
                    {
                        item.ImageURL = string.Format("Images/{0}{1}", item.ImageURL, MOBILE);
                    }
                    else if (userAgent == RepositoryResource.TABLET)
                    {
                        item.ImageURL = string.Format("Images/{0}{1}", item.ImageURL, TABLET);
                    }
                    else
                    {
                        item.ImageURL = string.Format("Images/{0}", item.ImageURL);
                    }
                }
            }
            return result;
        }
        public IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocationFilteredCategory(string locationKey, string activityCategoryKey, DateTime startDate, DateTime endDate, string userAgent)
        {
            IEnumerable<ActivitySummaryDataContract> initResult = GetAllActivitiesByLocationCategory(locationKey, activityCategoryKey, userAgent);
            List<ActivitySummaryDataContract> finalResult = new List<ActivitySummaryDataContract>();
            DayOfWeek startDay = startDate.DayOfWeek;
            DayOfWeek endDay = endDate.DayOfWeek;
            if ((endDate.Day - startDate.Day) > 7)
            {
                using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
                {
                    foreach (var item in initResult)
                    {
                        ActivityDayScheduler activitySchedule = (from entity in entityContext.ActivityDaySchedulerSet
                                                                 where entity.ActivityKey == item.ActivityKey
                                                                 select entity).ToList().FirstOrDefault();

                        if (activitySchedule != null && activitySchedule.ActivityKey != default(string))
                        {
                            finalResult.Add(item);
                        }
                        else
                        {
                            ActivityDates activityDates = (from entity in entityContext.ActivityDatesSet
                                                           where entity.Date > DateTime.Now
                                                           && entity.IsDeleted == false
                                                           select entity).ToList().FirstOrDefault();

                            if (activityDates != null &&
                                (activityDates.Date >= startDate && activityDates.Date < endDate))
                            {
                                finalResult.Add(item);
                            }
                        }
                    }
                }
            }
            else
            {
                Dictionary<int, bool> dates = new Dictionary<int, bool>();
                int daysInBetween = (endDate.Day - startDate.Day);
                for (int i = 0; i < daysInBetween; i++)
                {
                    switch (startDate.DayOfWeek)
                    {
                        case DayOfWeek.Sunday: dates[0] = true; break;
                        case DayOfWeek.Monday: dates[1] = true; break;
                        case DayOfWeek.Tuesday: dates[2] = true; break;
                        case DayOfWeek.Wednesday: dates[3] = true; break;
                        case DayOfWeek.Thursday: dates[4] = true; break;
                        case DayOfWeek.Friday: dates[5] = true; break;
                        case DayOfWeek.Saturday: dates[6] = true; break;
                    }
                    startDate = startDate.AddDays(1);
                }
                using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
                {
                    foreach (var item in initResult)
                    {
                        ActivityDayScheduler activitySchedule = (from entity in entityContext.ActivityDaySchedulerSet
                                                                 where entity.ActivityKey == item.ActivityKey
                                                                 select entity).ToList().FirstOrDefault();
                        bool datePresent = false;
                        for (int i = 0; i < 7; i++)
                        {
                            if (dates.ContainsKey(i))
                            {
                                switch (i)
                                {
                                    case 0: if (activitySchedule.IsSunday == true) { datePresent = true; }; break;
                                    case 1: if (activitySchedule.IsMonday == true) { datePresent = true; }; break;
                                    case 2: if (activitySchedule.IsTuesday == true) { datePresent = true; }; break;
                                    case 3: if (activitySchedule.IsWednesday == true) { datePresent = true; }; break;
                                    case 4: if (activitySchedule.IsThursday == true) { datePresent = true; }; break;
                                    case 5: if (activitySchedule.IsFriday == true) { datePresent = true; }; break;
                                    case 6: if (activitySchedule.IsSaturday == true) { datePresent = true; }; break;
                                }
                                if (datePresent)
                                {
                                    finalResult.Add(item);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return finalResult;
        }
    }
}
