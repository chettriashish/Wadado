using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers
{
    public class BaseViewController : Controller
    {
        public string GetDeviceInformation()
        {
            var deviceInfo = WURFL.WURFLManagerBuilder.Instance.GetDeviceForRequest(Request.UserAgent);            
            string device = default(string);
            if (Convert.ToBoolean(deviceInfo.GetVirtualCapability("is_mobile")))
            {
                if (Convert.ToBoolean(deviceInfo.GetVirtualCapability("is_smartphone")))
                {
                    device = "smartphone";
                }
                else
                {
                    device = "tablet";
                }
            }
            else
            {
                device = "desktop";
            }
            return device;
        }

        public void KeepAlive()
        {
            //KEEP SESSION ALIVE AND CHECK IF ITEMS IN CART ARE STILL AVAILABLE
            //IF NOT REMOVE THE ITEMS FROM CART
        }
        //HANDLES SESSION CLEARANCE BASED ON THE RULES DEFINED FOR EACH CONTROLLER
        public void SessionHandler(string controller)
        {
            if (Session != null)
            {
                switch (controller)
                {
                    case "Home": Session.Remove("action"); break;
                    case "ActivityDetails": Session.Remove("action"); break;
                    case "Location": Session.Remove("action"); break;
                    case "Activities": Session.Remove("action"); break;
                } 
            }            
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }
    }
}