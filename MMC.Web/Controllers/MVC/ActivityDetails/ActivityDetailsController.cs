using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MMC.Web.Controllers.ActivityDetails
{
    public class ActivityDetailsController : BaseViewController
    {
        private IActivitiesService _activitiesService;
        public ActivityDetailsController(IActivitiesService activitiesService)
        {
            _activitiesService = activitiesService;           
        }
        [OutputCache(CacheProfile = "global", Location = OutputCacheLocation.Server)]
        public JsonResult GetSelectedActivityDetails(string selectedLocation, string activityKey)
        {
            string device = GetDeviceInformation();
            ActivityDetailsDataContract selectedActivityDetails = _activitiesService.GetAllActivities(locationKey: selectedLocation.ToUpper(), activityKey: activityKey, userAgent: device);
            return Json(selectedActivityDetails, JsonRequestBehavior.AllowGet);
        }
        // GET: ActivityDetails
        public ActionResult Index()
        {
            SessionHandler("ActivityDetails");
            return View();
        }
    }
}