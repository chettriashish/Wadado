using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using MMC.Login.Contracts;
using MMC.Web.Core;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers
{
    public class AccountController : BaseViewController
    {
        ILoginService _loginService;
        IUsersService _usersService;
        public AccountController(ILoginService loginService, IUsersService usersService)
        {
            _loginService = loginService;
            _usersService = usersService;
            SessionHandler("Account");
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Index(string returnUrl)
        {
            return View(new AccountLoginModel() { ReturnUrl = returnUrl });
        }

        public ActionResult LogUserSession()
        {
            return Json(_loginService.LogUserSession(() =>
            {
                if (Session["sessionKey"] != null)
                {
                    return new UserSessionDataContract() { SessionKey = Convert.ToString(Session["sessionKey"]) };
                }
                else
                {                    
                    UserSessionDataContract result = _usersService.LogUserSession();
                    Session.Add("sessionKey", result.SessionKey);
                    return result;
                }
            }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogUserOut()
        {
            Session.Remove("guestKey");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveUserDetails(string userName, string userKey, string userMail, string loginMethod)
        {
            return Json(_loginService.AddUserInformation(() =>
            {
                UserSessionDataContract userInformation = new UserSessionDataContract() { SessionKey = Convert.ToString(Session["sessionKey"]), Name = userName, GuestKey = userKey, Email = userMail, LoginMethod = loginMethod };
                Session.Add("guestKey", userKey);
                return _usersService.AddGuestInformation(userInformation);
            }), JsonRequestBehavior.AllowGet);
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

        public ActionResult AddToFavorites(string activityKey)
        {
            if (Session["guestKey"] != null)
            {
                string guestKey = Convert.ToString(Session["guestKey"]);
                return Json(_usersService.AddToFavorites(guestKey, activityKey), JsonRequestBehavior.AllowGet);
            }
            else
            {                
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult StoreAction(string userAction, string returnURL, string activityKey)
        {
            ActionModel actionModel = new ActionModel();
            actionModel.Action = userAction;
            actionModel.ReturnURL = returnURL;
            actionModel.ActivityKey = activityKey;
            Session.Add("action", actionModel);           
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveFromFavorites(string activityKey)
        {
            if (Session["guestKey"] != null)
            {
                string guestKey = Convert.ToString(Session["guestKey"]);
                return Json(_usersService.RemoveFromFavorites(guestKey, activityKey, GetDeviceInformation()), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new ActivitySummaryDataContract(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetFavorites()
        {
            if (Session["guestKey"] != null)
            {
                string guestKey = Convert.ToString(Session["guestKey"]);
                return Json(_usersService.GetFavorites(guestKey, GetDeviceInformation()), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new ActivitySummaryDataContract(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckIfActivityInGuestFavorites(string activityKey)
        {
            if (Session["guestKey"] != null)
            {
                string guestKey = Convert.ToString(Session["guestKey"]);
                return Json(_usersService.CheckForActivityInFavorites(guestKey, activityKey), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckForAction()
        {
            if (Session["action"] != null)
            {
                ActionModel result = new ActionModel();
                result.Action = (Session["action"] as ActionModel).Action;
                result.ActivityDate = (Session["action"] as ActionModel).ActivityDate;
                result.ActivityKey = (Session["action"] as ActionModel).ActivityKey;
                result.NumberOfAdults = (Session["action"] as ActionModel).NumberOfAdults;
                result.NumberOfChildren = (Session["action"] as ActionModel).NumberOfChildren;
                result.ReturnURL = (Session["action"] as ActionModel).ReturnURL;
                result.Time = (Session["action"] as ActionModel).Time;                
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new ActionModel(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckIfUserLoggedIn()
        {
            if (Session["guestKey"] != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
