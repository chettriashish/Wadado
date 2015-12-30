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
                //fetching all details for selected activity
                ActivitiesMaster activity = Get().Where(entity => entity.ActivitesKey == activityKey
                    && entity.LocationKey == locationKey)
                    .ToList().FirstOrDefault();

                ActivityDayScheduler activityDayScheduler = entityContext.ActivityDaySchedulerSet.Where(entity =>
                                                            entity.ActivityKey == activityKey).FirstOrDefault();

                IEnumerable<ActivityTimeScheduler> activityTimeScheduler = entityContext.ActivityTimeSchedulerSet.
                    Where(entity => entity.ActivityKey == activityKey);
            

                ActivityCategoryMaster activityCategory = (from entity in entityContext.ActivityTypeMasterSet
                                                           join entity1 in entityContext.ActivityCategoryMasterSet
                                                           on entity.ActivityCategoryKey equals entity1.ActivityCategoryKey
                                                           where entity.ActivityTypeKey == activity.ActivityTypeKey
                                                           select entity1).FirstOrDefault();

                IEnumerable<ActivityImages> activityImages = entityContext.ActivityImagesSet.
                    Where(entity => entity.ActivityKey == activityKey)
                                                              .ToList();

                result.ActivityCategory = activityCategory.ActivityCategory;
                result.CancellationPolicy = activity.CancellationPolicy;
                result.Cost = activity.Cost;
                result.Currency = activity.Currency;
                result.Description = activity.Description;
                result.DifficultyRating = activity.DifficultyRating;
                result.Location = activity.Address;
                result.ActivityLocation = activity.ActivityLocation;
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

                result.ActivityImages = activityImages.Where(entity => entity.IsThumbnail == false).Select(entity => entity.ImageURL).ToList();
                result.ActivityImagesURL = activityImages.Where(entity => entity.IsThumbnail == false).Select(entity => entity.ImageURL).ToList();
                result.ImageURL = result.DefaultImageURL;
                result.DifficultyLevel = GetDifficultyLevel(result.DifficultyRating);
                result.AllActivityDates = ConvertDays(activityDayScheduler);
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
    }
}
