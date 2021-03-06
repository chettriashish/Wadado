﻿using MMC.Client.Entities;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Web.Contracts
{
    public interface IActivitiesDataService
    {
        IEnumerable<ActivitiesModel> GetSelectedActivityType(string userAgent, string activityType);
        IEnumerable<LocationsMaster> GetAllLocations();
    }
}
 