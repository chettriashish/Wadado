using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using MMC.Web.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MMC.Web.Controllers.MVC.Communication
{
    public class CommunicationController : Controller
    {
        IActivitiesService _activitiesService;

        public CommunicationController(IActivitiesService activitiesService)
        {
            _activitiesService = activitiesService;
        }

        // GET: Communcation
        public ActionResult BookingConfirmation()
        {
            ViewBag["Name"] = "Ashish Chettri";
            return View();
        }
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
        [HttpPost]
        public ActionResult SendEmail()
        {
            try
            {
                IEnumerable<EmailDataContract> result = new List<EmailDataContract>();
                ///WHEN THE USER IS LOGGED IN
                if (Session["sessionKey"] != null && Session["guestKey"] != null)
                {
                    string sessionKey = Convert.ToString(Session["sessionKey"]);
                    string guestKey = Convert.ToString(Session["guestKey"]);
                    result = _activitiesService.GetUsersBookingDetails(sessionKey, GetDeviceInformation());
                }
                ///WHEN USER IS NOT LOGGED IN
                else if (Session["sessionKey"] != null)
                {
                    string sessionKey = Convert.ToString(Session["sessionKey"]);
                    result = _activitiesService.GetUsersBookingDetails(sessionKey, GetDeviceInformation());
                }
                dynamic email = new Email("BookingConfirmation");
                if (TempData["userData"] != null)
                {
                    UserModel userDetails = (TempData["userData"] as UserModel);
                    email.Date = DateTime.UtcNow.Date.ToString("MMM dd yyyy");
                    email.Name = string.Format("{0} {1}", userDetails.FirstName, userDetails.LastName);
                    email.PhoneNumber = userDetails.PhoneNumber;
                    email.Email = userDetails.Email;                    
                    foreach (var booking in result)
                    {
                        if (booking.ChildParticipants > 0 && booking.Participants > 0)
                        {
                            email.NumPeople = string.Format(" {0} Adult(s), {1} Children", booking.Participants, booking.ChildParticipants);
                        }
                        else if (booking.ChildParticipants > 0 && booking.Participants == 0)
                        {
                            email.NumPeople = string.Format(" {0} Children", booking.ChildParticipants);
                        }
                        else
                        {
                            email.NumPeople = string.Format(" {0} Adult(s)", booking.Participants);
                        }
                        //for testing
                        email.To = userDetails.Email;                        
                        email.BookingTime = booking.Time;
                        email.BookingDateTime = string.Format("{0} - {1}", booking.BookingDate.ToString("MMM dd yyyy"), booking.Time);

                        email.BookingNumber = booking.BookingNumber;

                        email.Total = booking.PaymentAmount;
                        email.Currency =  booking.Currency;


                        email.ActivityName = booking.ActivityName;
                        //email.PriceOption = get price option for booking.PricingKey
                        //TBD
                        //1. Activity Name - done
                        //2. Activity Option - done
                        //3. Activity Date/Time - done 
                        //4. Activity Address - fetch from db
                        //5. Activity Owner Contact Number  - fetch from db
                        //6. Activity Duration  - fetch from db
                        //7. Cancellation Policy - fetch from db
                        //8. Restrictions  - fetch from db
                        //9. Things to bring  - fetch from db
                        //10. mymonkeycap footer
                        email.PriceOption = booking.PriceOption; //string.Format("Paragliding without lunch");
                        email.CancellationPolicy = booking.CancellationPolicy;// "There is no cancellation policy";
                        email.ContactNumber = booking.ContactNumber;// "+91 7585991297";
                        email.Address = booking.Address; //"Near Kanchendzonga Sports Complex, Ranka, East Sikkim";
                        email.Restrictions = booking.Restrictions;// "People over 90 kilograms are not allowed to participate";
                        email.ThingsToCarry = booking.ThingsToCarry;// "Please wear comfortable clothes and training shoes";
                        email.Duration = booking.Duration;// "2 hrs";
                        email.Send();
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            //return new EmailViewResult(email);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}