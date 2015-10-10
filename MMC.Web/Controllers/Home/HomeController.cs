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

namespace MMC.Web.Controllers.Home
{
    public class HomeController : Controller
    {
        IHomeDataService _homeDataService;

        public HomeController(IHomeDataService homeDataService)
        {
            _homeDataService = homeDataService;
        }
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult GetAllLocations()
        {                    
            IEnumerable<LocationsMaster> results =_homeDataService.GetAllLocations();            
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTopOffers()
        {
            IEnumerable<TopOffersModel> results = _homeDataService.GetTopOffers(Request.UserAgent);  
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTopTrendingActivities()
        {   
            IEnumerable<TopTrendingActivitiesModel> results =  _homeDataService.GetTrendingActivities(Request.UserAgent);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLatestNews()
        {
            IEnumerable<NewsModel> results = _homeDataService.GetLastestNews();
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}
