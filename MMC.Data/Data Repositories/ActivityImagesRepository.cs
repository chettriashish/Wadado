using MMC.Business.Entities;
using MMC.Data.Contracts.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.DataRepositories
{
    public class ActivityImagesRepository : DataRepositoryBase<ActivityImages>, IActivityImagesRepository
    {
        protected override ActivityImages AddEntity(MyMonkeyCapContext entityContext, ActivityImages entity)
        {
            return entityContext.ActivityImagesSet.Add(entity);
        }

        protected override ActivityImages UpdateEntity(MyMonkeyCapContext entityContext, ActivityImages entity)
        {
            return (from e in entityContext.ActivityImagesSet
                    where e.ActivityImageKey == entity.ActivityImageKey
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<ActivityImages> GetEntities(MyMonkeyCapContext entityContext)
        {
            return (from e in entityContext.ActivityImagesSet
                    select e);
        }

        protected override ActivityImages GetEntity(MyMonkeyCapContext entityContext, string key)
        {
            var query = (from e in entityContext.ActivityImagesSet
                         where e.ActivityImageKey == key
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<ActivityImages> GetImagesForSelectedActivity(string activityKey)
        {
            List<ActivityImages> result = new List<ActivityImages>();
            using (MyMonkeyCapContext entityContext = new MyMonkeyCapContext())
            {
                result = (from e in entityContext.ActivityImagesSet
                          where e.ActivityKey == activityKey
                          select e).ToList();
            }
            return result;
        }


        public bool RemoveImagesForActivity(string activityKey)
        {
            IEnumerable<ActivityImages> allActivityImages = GetImagesForSelectedActivity(activityKey);
            foreach (var item in allActivityImages)
            {
                Remove(item);
            }
            return true;
        }

        public bool AddImagesForActivity(string activityKey, List<string> images, string locationKey)
        {
            int count = 0;
            foreach (var image in images)
            {
                ActivityImages item = new ActivityImages
                {
                    ActivityImageKey = Guid.NewGuid().ToString(),
                    ActivityKey = activityKey,                    
                    ImageURL = image,
                    LocationKey = locationKey,
                    IsDefault = count == 0 ? true : false
                };
                Add(item);
                count++;
            }

            return true;
        }
    }
}
