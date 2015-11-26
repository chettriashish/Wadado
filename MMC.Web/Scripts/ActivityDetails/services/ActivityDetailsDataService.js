(function () {
    var app = angular.module("appMain");
    var activityDetailsDataService = function ($http, $q) {

        /****************FETCH SELECTED ACTIVITY DETAILS******************/
        var getSelectedActivityDetails = function () {
            var deferred = $q.defer();
            if ($.url().segment().length > 2) {
                if ($.url().segment(2).trim().length > 0 && $.url().segment(3).trim().length) {                    
                    var selectedActivityKey = $.url().segment(3);
                    $http({
                        url: '/ActivityDetails/GetSelectedActivityDetails',
                        method: 'GET',
                        params: { activityKey: activityKey }
                    }).success(deferred.resolve).error(deferred.reject);
                }
            }
            return deferred.promise;
        }
        /****************END FETCH SELECTED ACTIVITY DETAILS******************/

        /****************CHECK FOR ACTIVITY AVAILABILITY******************/
        var checkForSlotAvailability = function (date,time,numAdults,numChildren) {
            var deferred = $q.defer();            
            var selectedActivityKey = $.url().segment(3);
            $http({
                url: '/ActivityDetails/CheckForSlotAvailability',
                method: 'GET',
                params: { activityKey: activityKey, selectedDate: date,selectedTime:time,numberOfAdults:numAdults,numberOfChildren:numChildren }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        /****************END CHECK FOR ACTIVITY AVAILABILITY******************/

        /****************GET SIMILAR NEARBY ACTIVITIES******************/
        var getTwoSimilarActivitiesNearby = function (activityType, locationCode) {
            var deferred = $q.defer();            
            $http({
                url: '/ActivityDetails/GetTwoSimilarActivitiesNearby',
                method: 'GET',
                params: { activityType: activityType, locationCode:locationCode }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        /****************END GET SIMILAR NEARBY ACTIVITIES******************/


        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getSelectedActivityDetails: getSelectedActivityDetails,
            checkForSlotAvailability:checkForSlotAvailability,
            getTwoSimilarActivitiesNearby: getTwoSimilarActivitiesNearby,
        }
    };
    app.factory("ActivityDetailsDataService", activityDetailsDataService);
}());
