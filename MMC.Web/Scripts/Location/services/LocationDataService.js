(function () {
    var app = angular.module("appMain");
    var locationDataService = function ($http, $q, $window) {

        var getSelectedLocationDetails = function () {
            var selectedLocation = null;
            var deferred = $q.defer();
            if ($.url().segment().length > 1) {
                if ($.url().segment(2).trim().length > 0) {
                    var selectedLocation = $.url().segment(2);
                    $http({
                        url: '/Location/GetSelectedLocation',
                        method: 'GET',
                        params: { selectedLocation: selectedLocation }
                    }).success(deferred.resolve).error(deferred.reject);
                }
            }
            return deferred.promise;
        }

        var getAllAvailableLocations = function () {
            var deferred = $q.defer();
            $http.get('/Location/GetAllOtherLocations').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var getActivitiesForType = function (activityType, location) {
            $window.location.href = "/Activities/" + location + "/" + activityType;
        }

        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getSelectedLocationDetails: getSelectedLocationDetails,            
            getActivitiesForType: getActivitiesForType,
        }
    };
    app.factory("LocationDataService", locationDataService);
}());