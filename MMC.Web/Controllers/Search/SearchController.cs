using MMC.Client.Contracts.DataContracts;
using MMC.Client.Entities;
using MMC.Web.Contracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers.Search
{
    public class SearchController : Controller
    {
        private ISearchDataService _searchDataService;
        public SearchController(ISearchDataService searchDataService)
        {
            _searchDataService = searchDataService;
        }
        public ActionResult GetAllActivitiesForLocation(string locationKey)
        {
            IEnumerable<ActivitySummaryDataContract> result = _searchDataService.GetAllActivitiesForLocation(Request.UserAgent, locationKey);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllLocations()
        {
            IEnumerable<LocationsMaster> results = _searchDataService.GetAllLocations();
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}