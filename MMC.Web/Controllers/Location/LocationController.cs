using MMC.Web.Contracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers.Location
{
    public class LocationController : Controller
    {

        private ILocationDataService _locationDataService;
        public LocationController(ILocationDataService locationDataService)
        {
            _locationDataService = locationDataService;
        }
        
        public ActionResult Location()
        {
            return View();
        }

        public ActionResult GetAllActivitiesForSelectedLocation(string locationKey)
        {
            IEnumerable<LocationModel> results = _locationDataService.GetAllActivitiesForSelectedLocation(locationKey);
            return Json(results, JsonRequestBehavior.AllowGet);
        }       
    }
}