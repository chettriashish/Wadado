using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using MMC.Login.Contracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers.Booking
{
    public class BookingController : BaseViewController
    {
        private IActivitiesService _activitiesService;
        private ILoginService _loginService;
        IUsersService _usersService;
        public BookingController(IActivitiesService activitiesService, ILoginService loginService, IUsersService usersService)
        {
            _activitiesService = activitiesService;
            _loginService = loginService;
            _usersService = usersService;
        }
        public JsonResult CheckForActivityAvailability(string selectedActivityKey, int numAdults, int numChildren, string date, string time)
        {
            DateTime convertedDate = Convert.ToDateTime(date);
            bool availabilityStatus = _activitiesService.CheckForActivityAvailablity(selectedActivityKey, numAdults, numChildren, convertedDate, time);
            ActivityAvailabilityDetails result = new ActivityAvailabilityDetails() { Status = availabilityStatus };
            if (!availabilityStatus)
            {
                result.Message = string.Format("Sorry.The selected date cannot accomodate the tickets you have specified. Please select a new date or reduce the number of tickets");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult AddSelectedActivityToUsersCart(string selectedActivityKey, int numAdults,
           int numChildren, string bookingDate, string bookingTime, decimal total)
        {            
            if (Session["sessionKey"] != null)
            {
                string sessionKey = Convert.ToString(Session["sessionKey"]);
                return Json(_activitiesService.AddUserActivityToCart(selectedActivityKey, numAdults,
                numChildren, Convert.ToDateTime(bookingDate), bookingTime, total, sessionKey), JsonRequestBehavior.AllowGet);
            }
            else
            {
                LogUserSession();
                string sessionKey = Convert.ToString(Session["sessionKey"]);
                return Json(_activitiesService.AddUserActivityToCart(selectedActivityKey, numAdults,
                numChildren, Convert.ToDateTime(bookingDate), bookingTime, total, sessionKey), JsonRequestBehavior.AllowGet);
            }
        }

        public void LogUserSession()
        {
            if (Session["sessionKey"] == null)
            {
                UserSessionDataContract result = _usersService.LogUserSession();
                Session.Add("sessionKey", result.SessionKey);
            }
        }
    }
}