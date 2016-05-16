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
using System.Web.Script.Serialization;
using System.Web.UI;

namespace MMC.Web.Controllers.Activities
{
    public class ActivitiesController : BaseViewController
    {
        private IActivitiesService _activitiesService;
        //this is something that will be removed once the actual data is set
        private ILocationDataService _locationDataService;
        public ActivitiesController(IActivitiesService activitiesService, ILocationDataService locationDataService)
        {
            _activitiesService = activitiesService;
            _locationDataService = locationDataService;
        }

        // GET: Activities
        public ActionResult Index()
        {
            SessionHandler("Activities");
            return View();
        }
        
        [OutputCache(CacheProfile = "global", Location = OutputCacheLocation.Server,VaryByParam="selectedActivityCategory")]
        public ActionResult GetSelectedActivityType(string selectedLocation, string selectedActivityCategory)
        {
            //DUMMY IMPLEMENTATION FOR TILL ACTUAL DATA IS AVALABLE - THIS SECTION WILL BE REMOVED ONCE DATA IS AVAILABLE
            if (selectedActivityCategory == "TOPTRENDING")
            {
                IEnumerable<ActivitySummaryDataContract> result = _locationDataService.GetTopTrendingActivities(selectedLocation);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            //END DUMMY IMPLEMENTATION FOR TILL ACTUAL DATA IS AVALABLE - THIS SECTION WILL BE REMOVED ONCE DATA IS AVAILABLE
            else
            {
                //ACTUAL IMPLEMENTATION
                if (Session["StartDate"] != null && Session["EndDate"] != null)
                {
                    return GetSelectedActivityTypeByDate(selectedLocation, selectedActivityCategory, Session["StartDate"].ToString(), Session["EndDate"].ToString());
                }
                else
                {
                    IEnumerable<ActivitySummaryDataContract> result = _activitiesService.GetAllActivitiesByLocationAndType(selectedLocation, selectedActivityCategory, GetDeviceInformation());
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpGet]
        public ActionResult GetAllOtherLocations()
        {
            IEnumerable<LocationsMaster> results = new List<LocationsMaster>();
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetSelectedActivityTypeByDate(string selectedLocation, string selectedActivityCategory, string startDate, string endDate)
        {
            IEnumerable<ActivitySummaryDataContract> result = _activitiesService.GetAllActivitiesByLocationFilteredCategory(selectedLocation, selectedActivityCategory
                , DateTime.ParseExact(startDate, "d/MM/yyyy", CultureInfo.InvariantCulture)
                , DateTime.ParseExact(endDate, "d/MM/yyyy", CultureInfo.InvariantCulture), GetDeviceInformation());
            ActivityFilterModel filterModel = new ActivityFilterModel();
            if (Session["ActivityFilter"] != null)
            {
                (Session["ActivityFilter"] as ActivityFilterModel).StartDate = startDate;
                (Session["ActivityFilter"] as ActivityFilterModel).EndDate = endDate;
            }
            else
            {
                filterModel.StartDate = startDate;
                filterModel.EndDate = endDate;
                Session["ActivityFilter"] = filterModel;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetActivityTypeFilter(string activityTypes)
        {
            ActivityFilterModel filterModel = new ActivityFilterModel();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
                
            if (Session["ActivityFilter"] != null)
            {
                filterModel = Session["ActivityFilter"] as ActivityFilterModel;
                filterModel.ActivityTypes = serializer.Deserialize<List<ActivityTypeFilter>>(activityTypes);
            }
            else
            {
                filterModel.ActivityTypes = serializer.Deserialize<List<ActivityTypeFilter>>(activityTypes);
                Session["ActivityFilter"] = filterModel;
            }
            return Json(filterModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ClearDateFilters()
        {
            if (Session["ActivityFilter"] != null)
            {
                (Session["ActivityFilter"] as ActivityFilterModel).StartDate = default(string);
                (Session["ActivityFilter"] as ActivityFilterModel).EndDate = default(string);
                if ((Session["ActivityFilter"] as ActivityFilterModel).ActivityTypes != null &&
                    (Session["ActivityFilter"] as ActivityFilterModel).ActivityTypes.Count() == 0)
                {
                    Session.Remove("ActivityFilter");
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ClearActivityTypeFilter()
        {
            if (Session["ActivityFilter"] != null)
            {
                (Session["ActivityFilter"] as ActivityFilterModel).ActivityTypes = new List<ActivityTypeFilter>();
                if ((Session["ActivityFilter"] as ActivityFilterModel).StartDate == default(string) ||
                    (Session["ActivityFilter"] as ActivityFilterModel).StartDate == string.Empty)
                {
                    Session.Remove("ActivityFilter");
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetAllFilters()
        {
            ActivityFilterModel filterModel = new ActivityFilterModel();
            if (Session["ActivityFilter"] != null)
            {
                filterModel = Session["ActivityFilter"] as ActivityFilterModel;
            }
            return Json(filterModel, JsonRequestBehavior.AllowGet);
        }
    }
}