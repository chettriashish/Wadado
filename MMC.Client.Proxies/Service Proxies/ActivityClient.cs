using Core.Common.ServiceModel;
using MMC.Client.Contracts;
using MMC.Client.Contracts.DataContracts;
using MMC.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Proxies
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public class ActivityClient : UserClientBase<IActivitiesService>, IActivitiesService
    {
        public ActivityDetailsDataContract GetAllActivities(string locationKey, string activityKey, string userAgent)
        {
            return Channel.GetAllActivities(locationKey, activityKey, userAgent);
        }

        public IEnumerable<ActivitiesMaster> GetAllBookedActivities(string loginName)
        {
            return Channel.GetAllBookedActivities(loginName);
        }

        public bool CheckForActivityAvailablity(string activityKey, int adults,
            int children, DateTime bookingDate, string time)
        {
            return Channel.CheckForActivityAvailablity(activityKey, adults, children, bookingDate, time);
        }

        public ActivityBookingDataContract AddUserActivityToCart(string activityKey, string selectedActivityPriceOptionsKey, int adults,
            int children, DateTime bookingDate, string time, decimal total, string sessionKey)
        {
            ActivityBookingDataContract bookingDetails = new ActivityBookingDataContract();
            bookingDetails.ActivityBookingKey = Guid.NewGuid().ToString();
            bookingDetails.ActivityKey = activityKey;
            bookingDetails.ActivityPricingKey = selectedActivityPriceOptionsKey;
            bookingDetails.BookingDate = bookingDate;
            bookingDetails.CreatedDate = DateTime.Now;
            bookingDetails.ChildParticipants = children;
            bookingDetails.Participants = adults;
            bookingDetails.SessionKey = sessionKey;
            bookingDetails.ConfirmationDate = new DateTime(1753, 1, 1);
            bookingDetails.Time = time;
            bookingDetails.IsConfirmed = false;
            bookingDetails.IsDeleted = false;
            bookingDetails.IsCancelled = false;
            bookingDetails.PaymentAmount = total;
            bookingDetails.RefundAmount = 0;
            bookingDetails.IsPaymentComplete = false;
            //Need to add user information as well. This after login is complete
            return BookActivityForUser(bookingDetails);
        }

        public ActivityBookingDataContract AddUserActivityToCart(string activityKey, string selectedActivityPriceOptionsKey, int adults,
            int children, DateTime bookingDate, string time, decimal total, string sessionKey, string guestKey)
        {
            ActivityBookingDataContract bookingDetails = new ActivityBookingDataContract();
            bookingDetails.ActivityBookingKey = Guid.NewGuid().ToString();
            bookingDetails.ActivityKey = activityKey;
            bookingDetails.ActivityPricingKey = selectedActivityPriceOptionsKey;
            bookingDetails.BookingDate = bookingDate;
            bookingDetails.CreatedDate = DateTime.Now;
            bookingDetails.GuestKey = guestKey;
            bookingDetails.ChildParticipants = children;
            bookingDetails.Participants = adults;
            bookingDetails.SessionKey = sessionKey;
            bookingDetails.ConfirmationDate = new DateTime(1753, 1, 1);
            bookingDetails.Time = time;
            bookingDetails.IsConfirmed = false;
            bookingDetails.IsDeleted = false;
            bookingDetails.IsCancelled = false;
            bookingDetails.PaymentAmount = total;
            bookingDetails.RefundAmount = 0;
            bookingDetails.IsPaymentComplete = false;
            //Need to add user information as well. This after login is complete
            return BookActivityForUser(bookingDetails);
        }

        public ActivityBookingDataContract BookActivityForUser(ActivityBookingDataContract bookingDetails)
        {
            return Channel.BookActivityForUser(bookingDetails);
        }

        public Task<ActivityDetailsDataContract> GetAllActivitiesAsync(string locationKey,
            string activityKey, string userAgent)
        {
            return Channel.GetAllActivitiesAsync(locationKey, activityKey, userAgent);
        }

        public Task<IEnumerable<ActivitiesMaster>> GetAllBookedActivitiesAsync(string loginName)
        {
            return Channel.GetAllBookedActivitiesAsync(loginName);
        }

        public Task<bool> CheckForActivityAvailablityAsync(string activityKey, int adults,
            int children, DateTime bookingDate, string time)
        {
            return Channel.CheckForActivityAvailablityAsync(activityKey, adults, children, bookingDate, time);
        }

        public IEnumerable<ActivityBookingDataContract> GetUsersCurrentActivityCart(string sessionKey, string userAgent)
        {
            return Channel.GetUsersCurrentActivityCart(sessionKey, userAgent);
        }

        public Task<ActivityBookingDataContract> BookActivityForUserAsync(ActivityBookingDataContract bookingDetails)
        {
            return Channel.BookActivityForUserAsync(bookingDetails);
        }

        public Task<ActivityBookingDataContract> AddUserActivityToCartAsync(string activityKey, int adults, int children, DateTime bookingDate,
            string time, decimal total, string sessionKey)
        {
            ActivityBookingDataContract bookingDetails = new ActivityBookingDataContract();
            bookingDetails.ActivityBookingKey = Guid.NewGuid().ToString();
            bookingDetails.ActivityKey = activityKey;
            bookingDetails.BookingDate = bookingDate;
            bookingDetails.CreatedDate = DateTime.Now;
            bookingDetails.ChildParticipants = children;
            bookingDetails.Participants = adults;
            bookingDetails.SessionKey = sessionKey;
            bookingDetails.ConfirmationDate = new DateTime(1753, 1, 1);
            bookingDetails.Time = time;
            bookingDetails.IsConfirmed = false;
            bookingDetails.IsDeleted = false;
            bookingDetails.IsCancelled = false;
            bookingDetails.PaymentAmount = total;
            bookingDetails.RefundAmount = 0;
            bookingDetails.IsPaymentComplete = false;
            //Need to add user information as well. This after login is complete
            return BookActivityForUserAsync(bookingDetails);
        }

        public IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocationAndType(string locationKey, string activityCategoryKey, string userAgent)
        {
            return Channel.GetAllActivitiesByLocationAndType(locationKey, activityCategoryKey, userAgent);
        }

        public IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocationFilteredCategory(string locationKey, string activityCategoryKey, DateTime startDate, DateTime endDate, string userAgent)
        {
            return Channel.GetAllActivitiesByLocationFilteredCategory(locationKey, activityCategoryKey, startDate, endDate, userAgent);
        }

        public bool RemoveSelectedActivity(string sessionKey, string activityBookingKey)
        {
            return Channel.RemoveSelectedActivity(sessionKey, activityBookingKey);
        }
        public Task<bool> RemoveSelectedActivityAsync(string sessionKey, string activityBookingKey)
        {
            return Channel.RemoveSelectedActivityAsync(sessionKey, activityBookingKey);
        }
        public IEnumerable<ActivityCategoryMaster> GetAllActivityCategories()
        {
            return Channel.GetAllActivityCategories();
        }
        public IEnumerable<ActivityTypeMaster> GetAllActivitySubCategories()
        {
            return Channel.GetAllActivitySubCategories();
        }

        public void SaveCategory(string activityCategoryKey, string activityCategory)
        {
            ActivityCategoryMaster newActivityCategory = new ActivityCategoryMaster() { ActivityCategoryKey = activityCategoryKey, ActivityCategory = activityCategory };
            SaveCategories(newActivityCategory);
        }
        public void SaveSubCategory(string activityTypeKey, string activityType)
        {
            ActivityTypeMaster newActivityType = new ActivityTypeMaster() { ActivityTypeKey = activityTypeKey, ActivityType = activityType };
            SaveSubCategories(newActivityType);
        }
        public void SaveCategories(ActivityCategoryMaster activityCategory)
        {
            Channel.SaveCategories(activityCategory);
        }
        public void SaveSubCategories(ActivityTypeMaster activitySubCategory)
        {
            Channel.SaveSubCategories(activitySubCategory);
        }

        public IEnumerable<ActivityTypeMaster> GetSubCategoriesForSelectedActivity(string activityCategoryKey)
        {
            return Channel.GetSubCategoriesForSelectedActivity(activityCategoryKey);
        }

        public void SaveActivityCategoryMapping(IEnumerable<string> activityTypeKeys, string activityCategoryKey)
        {
            Channel.SaveActivityCategoryMapping(activityTypeKeys, activityCategoryKey);
        }

        public IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocation(string locationKey, string userAgent)
        {
            return Channel.GetAllActivitiesByLocation(locationKey, userAgent);
        }
        public Task<IEnumerable<ActivityTypeMaster>> GetSubCategoriesForSelectedActivityAsync(string activityCategoryKey)
        {
            throw new NotImplementedException();
        }

        public void SaveActivityDetails(ActivityDetailsDataContract activity, Dictionary<string, bool> activityDays,
            IEnumerable<string> activityTimes, string locationKey, string activityTypeKey, string user)
        {
            try
            {
                foreach (var item in activity.ActivityPriceOption)
                {
                    item.CreatedDate = DateTime.Now;
                }                
                Channel.SaveActivityDetails(activity, activityDays, activityTimes, locationKey, activityTypeKey, user);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            
        }

        public IEnumerable<ActivityBookingDataContract> GetAllActivitiesPendingForConfirmation()
        {
            return Channel.GetAllActivitiesPendingForConfirmation();
        }

        public IEnumerable<CompanyMaster> GetAllRegisteredCompanies()
        {
            return Channel.GetAllRegisteredCompanies();
        }
        public IEnumerable<ActivityBookingDataContract> GetAllCompanyActivitiesPendingForConfirmation(string companyKey)
        {
            return Channel.GetAllCompanyActivitiesPendingForConfirmation(companyKey);
        }

        public IEnumerable<ActivityBookingDataContract> GetAllCompanyActivitiesCompleted(string companyKey)
        {
            return Channel.GetAllCompanyActivitiesCompleted(companyKey);
        }

        public IEnumerable<ActivityBookingDataContract> GetAllActivitiesCompleted()
        {
            return Channel.GetAllActivitiesCompleted();
        }
        public IEnumerable<ActivityBookingDataContract> GetAllUpcomingActivities()
        {
            return Channel.GetAllUpcomingActivities();
        }

        public IEnumerable<ActivityBookingDataContract> GetAllUpcomingCompanyActivities(string companyKey)
        {
            return Channel.GetAllUpcomingCompanyActivities(companyKey);
        }

        public ActivityDetailsDataContract CreateNewActivityDetails()
        {
            ActivityDetailsDataContract result = new ActivityDetailsDataContract();
            result.AllActivityUniqueDates = new List<ActivityDates>();
            result.AllPriceOptions = new List<ActivityPriceMapping>();
            result.IsEvent = false;
            result.IsActivity = true;
            return result;
        }

        public bool AcceptSelectedActivityBooking(string bookingKey, string user)
        {
            return Channel.AcceptSelectedActivityBooking(bookingKey, user);
        }

        public bool RejectSelectedActivityBooking(string bookingKey, string user)
        {
            return Channel.RejectSelectedActivityBooking(bookingKey, user);
        }
        public IEnumerable<ActivitySearchDataContract> GetActivitiesForSelectedSearchTag(IEnumerable<string> tags)
        {
            IEnumerable<ActivitySearchDataContract> results = Channel.GetActivitiesForSelectedSearchTag(tags.ToList());
            return results;
        }

        public IEnumerable<EmailDataContract> GetUsersBookingDetails(string sessionKey, string userAgent)
        {
            return Channel.GetUsersBookingDetails(sessionKey, userAgent);
        }
        public bool SaveActivityImages(string activityKey, List<string> images)
        {
            return Channel.SaveActivityImages(activityKey, images);
        }
    }
}
