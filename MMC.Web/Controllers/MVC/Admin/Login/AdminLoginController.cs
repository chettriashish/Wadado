using MMC.Client.Contracts;
using MMC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers.Admin.Login
{
    public class AdminLoginController : BaseViewController
    {
        IUsersService _usersService;
        public AdminLoginController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        [Authorize]
        public ActionResult CheckIfUserBelongsToCompany(string userId)
        {
            return Json(_usersService.CheckIfUserBelongsToCompany(userId), JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult CreateCompanyForSelectedUser(string userId, CompanyModel company)
        {
            try
            {
                return Json(_usersService.CreateCompanyForSelectedUser(userId, company.Name, company.Address, company.TelephoneNumber, company.Email, company.ContactPerson), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }            
        }
        [Authorize]
        public ActionResult CheckAdminUser(string userId)
        {
            try
            {
                return Json(_usersService.CheckAdminUser(userId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}