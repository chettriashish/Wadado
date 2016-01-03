using MMC.Client.Contracts;
using MMC.Client.Entities;
using MMC.Web.Contracts;
using MMC.Web.Model;
using System;
using MMC.Client.Contracts.DataContracts;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace MMC.Web.Controllers.Activities
{
    public class ActivitiesController : BaseViewController
    {
        private IActivitiesService _activitiesService;

        public ActivitiesController(IActivitiesService activitiesService)
        {
            _activitiesService = activitiesService;            
        }

        // GET: Activities
        public ActionResult Index()
        {
            SessionHandler("Activities");
            return View();
        }

        public ActionResult GetSelectedActivityType(string selectedLocation, string selectedActivityCategory)
        {
            IEnumerable<ActivitySummaryDataContract> result = _activitiesService.GetAllActivitiesByLocationAndType(selectedLocation,selectedActivityCategory,GetDeviceInformation());
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllOtherLocations()
        {
            IEnumerable<LocationsMaster> results = new List<LocationsMaster>();
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSelectedActivityTypeByDate(string selectedLocation, string selectedActivityCategory, string startDate, string endDate)
        {
            IEnumerable<ActivitySummaryDataContract> result = _activitiesService.GetAllActivitiesByLocationFilteredCategory(selectedLocation, selectedActivityCategory
                , DateTime.ParseExact(startDate, "d/MM/yyyy", CultureInfo.InvariantCulture)
                , DateTime.ParseExact(endDate, "d/MM/yyyy", CultureInfo.InvariantCulture), GetDeviceInformation());
            return Json(result, JsonRequestBehavior.AllowGet);
        }      
    }
}