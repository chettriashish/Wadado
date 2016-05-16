using Core.Common.Contracts;
using MMC.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.Contracts.RepositoryInterfaces
{
    public interface IActivityTagMappingRepository : IDataRepository<ActivityTagMapping>
    {
        IEnumerable<ActivitiesMaster> GetActivitiesForSelectedSearchTag(IEnumerable<string> tags);
        List<ActivityTagMapping> GetTagsForSelectedActivity(string activityKey);
    }
}
