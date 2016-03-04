using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MMC.Web.Controllers.Admin.Locations
{
    public class AdminLocationController : Controller
    {
        private ILocationService _locationService;
        public AdminLocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        //This action lets users create new locations
        public ActionResult CreateNewLocation()
        {
            return Json(new LocationDetailsDataContract(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditLocation(AdminLocationModel location)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteLocation(string locationKey)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllLocations()
        {
            return Json(_locationService.GetAllLocations(),JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSelectedLocation(string selectedLocationKey)
        {
            return Json(_locationService.GetSelectedLocationDetails(selectedLocationKey), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveLocationDetails(LocationDetailsDataContract locationDetails)
        {
            if (locationDetails.LocationKey != default(string))
            {

                _locationService.UpdateLocationDetails(locationDetails);
            }
            else
            {
                _locationService.CreateNewLocation(locationDetails);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}