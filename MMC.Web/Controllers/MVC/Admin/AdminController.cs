using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Clients;
using MMC.Web.Model;
using System.Threading.Tasks;

namespace MMC.Web.Controllers.Admin
{
    public class AdminController : Controller
    {

#if DEBUG
        public const string tokenEndPointURL = "http://localhost/connect/token"; 
        //public const string tokenEndPointURL = "http://182.50.130.34/connect/token";
#else        
        public const string tokenEndPointURL = "http://www.wadado.in/wadado/connect/token";
        //public const string tokenEndPointURL = "http://182.50.130.34/connect/token";
        //public const string tokenEndPointURL = "http://localhost:9850/connect/token"; 
#endif
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            UserCredentialsModel user = new UserCredentialsModel
            {
                Email = string.Empty,
                IsLoggedIn = false
            };
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Login(string userName, string password)
        {
            UserCredentialsModel user = null;
            try
            {
                var client = new OAuth2Client(new Uri(tokenEndPointURL), "mymonkeycap", "Nexusdata#1");

                await Task.Run(() =>
                {
                    var requestResponse = client.RequestAccessTokenUserName(userName, password, "openid profile offline_access");
                    var claims = new[]
                    {
                        new Claim("access_token",requestResponse.AccessToken),
                        new Claim("refresh_token", requestResponse.RefreshToken)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(claimsIdentity);
                });

                user = new UserCredentialsModel
                {
                    Email = userName,
                    IsLoggedIn = true
                };
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet); 
            }

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}