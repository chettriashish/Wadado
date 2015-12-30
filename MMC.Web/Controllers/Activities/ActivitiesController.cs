using MMC.Client.Entities;
using MMC.Web.Contracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers.Activities
{
    public class ActivitiesController : BaseViewController
    {
        private IActivitiesDataService _activitiesDataService;

        public ActivitiesController(IActivitiesDataService activitiesDataService)
        {
            _activitiesDataService = activitiesDataService;            
        }

        // GET: Activities
        public ActionResult Index()
        {
            SessionHandler("Activities");
            return View();
        }

        public ActionResult GetSelectedActivityType(string selectedLocation, string selectedActivityType)
        {
            IEnumerable<ActivitiesModel> result = _activitiesDataService.GetSelectedActivityType(Request.UserAgent, selectedActivityType);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllOtherLocations()
        {
            IEnumerable<LocationsMaster> results = _activitiesDataService.GetAllLocations();
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}