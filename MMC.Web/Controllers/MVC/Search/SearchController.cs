﻿using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using MMC.Client.Entities;
using MMC.Web.Contracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MMC.Web.Controllers.Search
{
    public class SearchController : Controller
    {
        private ISearchDataService _searchDataService;
        private ILocationService _locationService;
        public SearchController(ISearchDataService searchDataService, ILocationService locationService)
        {
            _searchDataService = searchDataService;
            _locationService = locationService;
        }
        public ActionResult GetAllActivitiesForLocation(string locationKey)
        {
            IEnumerable<ActivitySummaryDataContract> result = _searchDataService.GetAllActivitiesForLocation(Request.UserAgent, locationKey);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(CacheProfile = "global", Location = OutputCacheLocation.Server)]
        public ActionResult GetAllLocations()
        {
            IEnumerable<LocationsMaster> results = _locationService.GetAllLocations();
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}