using MMC.Business.Contracts.DataContracts;
using MMC.Business.Entities;
using MMC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Business.Contracts
{
    [ServiceContract]    
    public interface IActivitiesService
    {
        [OperationContract]
        ActivityDetailsDataContract GetAllActivities(string locationKey, string activityKey, string userAgent);

        [OperationContract]
        //[FaultContract(typeof(AuthorizationValidationException))]
        IEnumerable<ActivitiesMaster> GetAllBookedActivities(string loginName);
        [OperationContract]
        bool CheckForActivityAvailablity(string activityKey, int adults, int children, DateTime bookingDate, string time);        
       
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        ActivityBookingDataContract BookActivityForUser(ActivityBookingDataContract bookingDetails);                
        [OperationContract]
        IEnumerable<ActivityBookingDataContract> GetUsersCurrentActivityCart(string sessionKey, string userAgent);
        [OperationContract]
        IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocationAndType(string locationKey, string activityCategoryKey, string userAgent);
        [OperationContract]
        IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocationFilteredCategory(string locationKey, string activityCategoryKey, DateTime startDate, DateTime endDate, string userAgent);
        [OperationContract]
        bool RemoveSelectedActivity(string sessionKey, string activityBookingKey);
        [OperationContract]
        IEnumerable<ActivityCategoryMaster> GetAllActivityCategories();
        [OperationContract]
        IEnumerable<ActivityTypeMaster> GetAllActivitySubCategories();
        [OperationContract]
        void SaveCategories(ActivityCategoryMaster activityCategory);
        [OperationContract]
        void SaveSubCategories(ActivityTypeMaster activitySubCategory);
        [OperationContract]
        IEnumerable<ActivityTypeMaster> GetSubCategoriesForSelectedActivity(string activityCategoryKey);

        [OperationContract]
        void SaveActivityCategoryMapping(IEnumerable<string> activityTypeKeys, string activityCategoryKey);

        [OperationContract]
        IEnumerable<ActivitySummaryDataContract> GetAllActivitiesByLocation(string locationKey, string userAgent);
        [OperationContract]
        void SaveActivityDetails(ActivityDetailsDataContract activity, Dictionary<string, bool> activityDays,
            IEnumerable<string> activityTimes, string locationKey, string activityTypeKey, string user);
        [OperationContract]
        IEnumerable<ActivityBookingDataContract> GetAllActivitiesPendingForConfirmation();
        [OperationContract]
        IEnumerable<CompanyMaster> GetAllRegisteredCompanies();
        [OperationContract]
        IEnumerable<ActivityBookingDataContract> GetAllCompanyActivitiesPendingForConfirmation(string companyKey);
        [OperationContract]
        IEnumerable<ActivityBookingDataContract> GetAllCompanyActivitiesCompleted(string companyKey);
        [OperationContract]
        IEnumerable<ActivityBookingDataContract> GetAllActivitiesCompleted();
        [OperationContract]
        IEnumerable<ActivityBookingDataContract> GetAllUpcomingActivities();
        [OperationContract]
        IEnumerable<ActivityBookingDataContract> GetAllUpcomingCompanyActivities(string companyKey);
        [OperationContract]
        bool AcceptSelectedActivityBooking(string bookingKey, string user);
        [OperationContract]
        bool RejectSelectedActivityBooking(string bookingKey, string user);
        [OperationContract]
        IEnumerable<ActivitiesMaster> GetActivitiesForSelectedSearchTag(IEnumerable<string> tags);
        [OperationContract]
        IEnumerable<EmailDataContract> GetUsersBookingDetails(string sessionKey, string userAgent);
    }
}
