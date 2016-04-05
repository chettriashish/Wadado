using MMC.Client.Contracts;
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

namespace MMC.Web.Controllers.Location
{
    public class LocationController : BaseViewController
    {
        private ILocationService _locationService;
        private ILocationDataService _locationDataService;
        public LocationController(ILocationDataService locationDataService, ILocationService locationService)
        {
            _locationDataService = locationDataService;
            _locationService = locationService;
        }

        public ActionResult Index(string locationName)
        {
            string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
            if (locationName == null)
            {                
                Response.Redirect(baseUrl + "/Location/Gangtok");
            }
            SessionHandler("Location");
            return View();
        }

        [OutputCache(CacheProfile = "global", Location = OutputCacheLocation.Server)]
        [HttpGet]
        public ActionResult GetSelectedLocation(string selectedLocation)
        {
            IEnumerable<LocationDetailsDataContract> results = _locationService.GetSelectedLocationDetailsForClientApplication(selectedLocation, GetDeviceInformation());
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        [OutputCache(CacheProfile = "global", Location = OutputCacheLocation.Server)]
        [HttpGet]
        public ActionResult GetAllOtherLocations()
        {
            IEnumerable<LocationsMaster> results = _locationService.GetAllLocations();
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}