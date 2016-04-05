using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers.ActivitiesCart
{
    public class ActivitiesCartController : BaseViewController
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

        public JsonResult RemoveSelectedActivityFromUsersCart(string activityBookingKey)
        {
            if (Session["sessionKey"] != null)
            {
                string sessionKey = Convert.ToString(Session["sessionKey"]);
                return Json(_activitiesService.RemoveSelectedActivity(sessionKey, activityBookingKey), JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsersActivityCart()
        {
            ///WHEN THE USER IS LOGGED IN
            if (Session["sessionKey"] != null && Session["guestKey"] != null)
            {
                string sessionKey = Convert.ToString(Session["sessionKey"]);
                string guestKey = Convert.ToString(Session["guestKey"]);
                return Json(_activitiesService.GetUsersCurrentActivityCart(sessionKey,GetDeviceInformation()), JsonRequestBehavior.AllowGet);
            }
            ///WHEN USER IS NOT LOGGED IN
            else if (Session["sessionKey"] != null)
            {
                string sessionKey = Convert.ToString(Session["sessionKey"]);
                return Json(_activitiesService.GetUsersCurrentActivityCart(sessionKey,GetDeviceInformation()), JsonRequestBehavior.AllowGet);
            }
            ///THIS CONDITION IS A FAIL SAFE. SHOULD NEVER HAPPEN AS SESSION SHOULD BE ALIVE AS LONG AS USER 
            ///IS ADDING ITEMS TO CART
            else
            {
                return Json(new ActivityBookingDataContract(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ProceedToPayment(string firstName, string lastName, string phoneNumber, string email)
        {
            bool isValid = false;
            string emailRegex = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            if(!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName)
                && !string.IsNullOrWhiteSpace(phoneNumber) && !string.IsNullOrWhiteSpace(email))
            {
                if(Regex.IsMatch(email,emailRegex))
                {
                    isValid = true;
                }
                UserModel userDetails = new UserModel() { FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phoneNumber };
                TempData.Add("userData", userDetails);
            }             

            //send out emails 


            return Json(isValid, JsonRequestBehavior.AllowGet);            
        }       
    }
}