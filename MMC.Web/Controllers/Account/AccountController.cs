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
    public class AccountController : ViewControllerBase
    {
        ISecurityAdapter _securityAdapter;
        ILoginService _loginService;
        IUsersService _usersService;
        public AccountController(ISecurityAdapter securityAdapter, ILoginService loginService, IUsersService usersService)
        {
            _securityAdapter = securityAdapter;
            _loginService = loginService;
            _usersService = usersService;
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Index(string returnUrl)
        {
            _securityAdapter.Initialize();
            return View(new AccountLoginModel() { ReturnUrl = returnUrl });
        }

        public ActionResult LogUserSession()
        {
            return Json(_loginService.LogUserSession(() =>
            {
                UserSessionDataContract result = _usersService.LogUserSession();
                Session.Add("sessionKey", result.SessionKey);
                return result;

            }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveUserDetails(string userName, string userKey, string userMail)
        {
            return Json(_loginService.AddUserInformation(() =>
            {
                UserSessionDataContract userInformation = new UserSessionDataContract() { SessionKey = Convert.ToString(Session["sessionKey"]), Name = userName, GuestKey = userKey, Email = userMail };
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
                return Json(new UserSessionDataContract() {Name = default(string), SessionKey = default(string) }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
