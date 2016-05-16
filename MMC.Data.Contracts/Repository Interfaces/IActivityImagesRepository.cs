using Core.Common.Contracts;
using MMC.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.Contracts.RepositoryInterfaces
{
    public interface IActivityImagesRepository:IDataRepository<ActivityImages>
    {
        IEnumerable<ActivityImages> GetImagesForSelectedActivity(string activityKey);
        bool RemoveImagesForActivity(string activityKey);
        bool AddImagesForActivity(string activityKey, List<string> images, string locationKey);
    }
}
