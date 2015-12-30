using MMC.Client.Entities;
using MMC.Web.Contracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers.Location
{
    public class LocationController : BaseViewController
    {

        private ILocationDataService _locationDataService;
        public LocationController(ILocationDataService locationDataService)
        {
            _locationDataService = locationDataService;
        }
        
        public ActionResult Index(string locationName)
        {
            SessionHandler("Location");
            return View();
        }
                
        [HttpGet]
        public ActionResult GetSelectedLocation(string selectedLocation)
        {                    
            LocationModel results = _locationDataService.GetAllActivitiesForSelectedLocation(selectedLocation, Request.UserAgent);         
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllOtherLocations()
        {
            IEnumerable<LocationsMaster> results = _locationDataService.GetAllLocations();
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}