using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers.Menu
{
    public class MenuController : BaseViewController
    {
        IUsersService _usersService;
        IActivitiesService _activitiesService;
        public MenuController(IUsersService usersService, IActivitiesService activitiesService)
        {
            _usersService = usersService;
            _activitiesService = activitiesService;
        }
        public ActionResult GetGuestInformation()
        {
            if (Session["sessionKey"] != null && Session["guestKey"] != null)
            {
                string userKey = Session["guestKey"].ToString();
                return Json(_usersService.GetGuestInformation(userKey), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new UserSessionDataContract() { Name = default(string), SessionKey = default(string) }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetUsersActivityCartCount()
        {
            int result = 0;
            ///WHEN THE USER IS LOGGED IN
            if (Session["sessionKey"] != null && Session["guestKey"] != null)
            {
                string sessionKey = Convert.ToString(Session["sessionKey"]);
                string guestKey = Convert.ToString(Session["guestKey"]);
                return Json((result = _activitiesService.GetUsersCurrentActivityCart(sessionKey, GetDeviceInformation()).Count()), JsonRequestBehavior.AllowGet);
            }
            ///WHEN USER IS NOT LOGGED IN
            else if (Session["sessionKey"] != null)
            {
                string sessionKey = Convert.ToString(Session["sessionKey"]);
                return Json((_activitiesService.GetUsersCurrentActivityCart(sessionKey, GetDeviceInformation()).Count()), JsonRequestBehavior.AllowGet);
            }
            ///THIS CONDITION IS A FAIL SAFE. SHOULD NEVER HAPPEN AS SESSION SHOULD BE ALIVE AS LONG AS USER 
            ///IS ADDING ITEMS TO CART
            else
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}