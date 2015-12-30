using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMC.Client.Entities;
using MMC.Web.Model;
using MMC.Common.Extensions;
using MMC.Web.Contracts;
using Core.Common.Core;
using Microsoft.Practices.Unity;
using MMC.Login.Contracts;
using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;

namespace MMC.Web.Controllers.Home
{
    public class HomeController : BaseViewController
    {
        IHomeDataService _homeDataService;
        ILoginService _loginService;
        IUsersService _usersService;

        public HomeController(IHomeDataService homeDataService, ILoginService loginService, IUsersService usersService)
        {
            _homeDataService = homeDataService;
            _loginService = loginService;
            _usersService = usersService;           
        }
        public ActionResult Index()
        {
            SessionHandler("Home");
            return View();
        }

        public ActionResult GetTopOffers()
        {
            IEnumerable<TopOffersModel> results = _homeDataService.GetTopOffers(Request.UserAgent);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTopTrendingActivities()
        {
            IEnumerable<ActivitiesModel> results = _homeDataService.GetTrendingActivities(Request.UserAgent);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLatestNews()
        {
            IEnumerable<NewsModel> results = _homeDataService.GetLastestNews();
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogUserSession()
        {
            return Json(_loginService.LogUserSession(() =>
            {
                UserSessionDataContract result = _usersService.LogUserSession();
                return result.SessionKey;

            }), JsonRequestBehavior.AllowGet);
        }
    }
}
