using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
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
          public BookingController(IActivitiesService activitiesService)
          {
              _activitiesService = activitiesService;
          }

          public JsonResult CheckForActivityAvailability(string selectedActivityKey, int numAdults, int numChildren, DateTime date, string time)
          {
              bool availabilityStatus =  _activitiesService.CheckForActivityAvailablity(selectedActivityKey,numAdults,numChildren, date,time);
              ActivityAvailabilityDetails result = new ActivityAvailabilityDetails() { Status = availabilityStatus };
              if (!availabilityStatus)
              {
                  result.Message = string.Format("Sorry.The selected date cannot accomodate the tickets you have specified. Please select a new date or reduce the number of tickets");
              }
              return Json(result, JsonRequestBehavior.AllowGet);
          }

          public JsonResult AddSelectedActivityToUsersCart(ActivityDetailsDataContract selectedActivity, int numAdults,
             int numChildren, DateTime bookingDate, string bookingTime, decimal total)
          {
              return Json(_activitiesService.AddUserActivityToCart(selectedActivity, numAdults,
                  numChildren, bookingDate, bookingTime, total),JsonRequestBehavior.AllowGet);
          }
    }
}