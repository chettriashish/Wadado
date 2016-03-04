using MMC.Client.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers.Admin.Bookings
{
    public class AdminBookingsController : Controller
    {
        IActivitiesService _activitiesService;
        public AdminBookingsController(IActivitiesService activitiesService)
        {
            _activitiesService = activitiesService;
        }

        public ActionResult GetAllActivitiesPendingForConfirmation()
        {
            return Json(_activitiesService.GetAllActivitiesPendingForConfirmation(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllRegisteredCompanies()
        {
            return Json(_activitiesService.GetAllRegisteredCompanies(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllActivitiesPendingForSelectedCompany(string companyKey)
        {
            return Json(_activitiesService.GetAllCompanyActivitiesPendingForConfirmation(companyKey), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCompletedActivities()
        {
            return Json(_activitiesService.GetAllActivitiesCompleted(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllActivitiesCompletedForSelectedCompany(string companyKey)
        {
            return Json(_activitiesService.GetAllCompanyActivitiesCompleted(companyKey), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllUpcomingActivities()
        {
            return Json(_activitiesService.GetAllUpcomingActivities(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllUpcomingCompanyActivities(string companyKey)
        {
            return Json(_activitiesService.GetAllUpcomingCompanyActivities(companyKey), JsonRequestBehavior.AllowGet);
        }
    }
}