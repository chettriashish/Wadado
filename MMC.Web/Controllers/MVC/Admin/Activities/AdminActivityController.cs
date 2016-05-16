using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using MMC.Web.Helpers;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers.Admin.Activities
{
    public class AdminActivityController : BaseViewController
    {
        private IActivitiesService _activitiesService;
        private ILocationService _locationService;
        public AdminActivityController(IActivitiesService activitiesService, ILocationService locationService)
        {
            _activitiesService = activitiesService;
            _locationService = locationService;
        }
        public ActionResult GetAllActivitiesByLocation(string locationKey)
        {
            IEnumerable<ActivitySummaryDataContract> results = _activitiesService.GetAllActivitiesByLocation(locationKey, GetDeviceInformation());
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSelectedActivityDetails(string activityKey)
        {
            string device = GetDeviceInformation();
            ActivityDetailsDataContract selectedActivityDetails = _activitiesService.GetAllActivities(locationKey: default(string), activityKey: activityKey, userAgent: device);
            return Json(selectedActivityDetails, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveActivityDetails(ActivityDetailsDataContract activityDetails, Dictionary<string, bool> activityDays, IEnumerable<string> activityTimes, string activityCategoryKey, string activityLocationKey)
        {
            //Make call to service                      
            _activitiesService.SaveActivityDetails(activityDetails, activityDays, activityTimes, activityLocationKey, activityCategoryKey, "TBD");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadImages(string activityKey, List<AdminImageModel> activityImages)
        {
            ImagesHelper imageHelper = new ImagesHelper();
            imageHelper.ResizeAndUpload(activityImages.Where(e => e.Result != null).ToList(), false);
            List<string> images = new List<string>();
            foreach (var item in activityImages)
            {
                images.Add(item.Name);
            }
            _activitiesService.SaveActivityImages(activityKey, images);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveEventDetails(ActivityDetailsDataContract activityDetails, string activityCategoryKey, string activityLocationKey)
        {
            //Make call to service
            _activitiesService.SaveActivityDetails(activityDetails, null, null, activityLocationKey, activityCategoryKey, "TBD");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateNewActivity()
        {
            return Json(_activitiesService.CreateNewActivityDetails(), JsonRequestBehavior.AllowGet);
        }
    }
}