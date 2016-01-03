﻿using Core.Common.Contracts;
using MMC.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Data.Contracts.RepositoryInterfaces
{
    public interface IActivityTypeCategoryRepository : IDataRepository<ActivityTypeCategory>
    {
        IEnumerable<ActivityTypeCategory> GetActivityTypeCategoryMapping(string activityTypeKey);
        IEnumerable<ActivityTypeCategory> GetActivityCategoryTypeMapping(string activityCategoryKey);
    }
}
