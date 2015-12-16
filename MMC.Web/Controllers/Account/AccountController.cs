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
        public AccountController(ISecurityAdapter securityAdapter)
        {
            _securityAdapter = securityAdapter;
        }

        [HttpGet]        
        [Route("login")]
        public ActionResult Index(string returnUrl)
        {
            _securityAdapter.Initialize();
            return View(new AccountLoginModel() {ReturnUrl = returnUrl });
        }
    }
}
