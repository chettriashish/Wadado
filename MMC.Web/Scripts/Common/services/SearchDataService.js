(function () {
    var app = angular.module("appMain");
    var searchDataService = function ($http, $q, $window) {

        var getAllLocations = function () {
            var deferred = $q.defer();
            $http.get('/Location/GetAllOtherLocations').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        var getAllActivitiesForLocation = function (locationKey) {
            var deferred = $q.defer();
            $http.get('/Search/GetAllActivitiesForLocation', locationKey).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var getSelectedDetails = function (activity, location) {
            if ((activity == null || activity.trim() == "Explore") && location != null && location.length > 0) {
                $window.location.href = "/Location/" + location;
            }
            else if (location != null && activity != null
                && location.trim() != "" && activity.trim() != "") {
                $window.location.href = "/Activities/" + location + "/" + activity;
            }
        }

        return {
            getAllLocations: getAllLocations,
            getAllActivitiesForLocation: getAllActivitiesForLocation,
            getSelectedDetails: getSelectedDetails,           
        };
    };
    app.factory("SearchDataService", searchDataService);
}());