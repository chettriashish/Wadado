using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers.ActivitiesCart
{
    public class ActivitiesCartController : Controller
    {
        IActivitiesService _activitiesService;
        public ActivitiesCartController(IActivitiesService activitiesService)
        {
            _activitiesService = activitiesService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult RemoveSelectedActivityFromUsersCart(string activityKey, string sessionKey)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsersActivityCart(string sessionKey)
        {
            return Json(_activitiesService.GetUsersCurrentActivityCart(sessionKey),JsonRequestBehavior.AllowGet);
        }
    }
}