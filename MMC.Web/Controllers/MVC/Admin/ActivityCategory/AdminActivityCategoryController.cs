using MMC.Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers.Admin.ActivityCategory
{
    public class AdminActivityCategoryController : Controller
    {
        private IActivitiesService _activitiesService;
        // GET: AdminActivityCategory
        public AdminActivityCategoryController(IActivitiesService activitiesService)
        {
            _activitiesService = activitiesService;
        }
        public ActionResult GetAllCategories()
        {
            return Json(_activitiesService.GetAllActivityCategories(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllActivitySubCategories()
        {
            return Json(_activitiesService.GetAllActivitySubCategories(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveActivityCategory(string activityCategoryKey, string activityCategory)
        {
            _activitiesService.SaveCategory(activityCategoryKey, activityCategory);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveActivitySubCategory(string activitySubCategoryKey, string activitySubCategory)
        {
            _activitiesService.SaveSubCategory(activitySubCategoryKey, activitySubCategory);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubCategoriesForSelectedActivity(string activityCategoryKey)
        {
            return Json(_activitiesService.GetSubCategoriesForSelectedActivity(activityCategoryKey), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveActivityCategoryMapping(IEnumerable<string> activityTypes, string activityCategory)
        {
            _activitiesService.SaveActivityCategoryMapping(activityTypes, activityCategory);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}