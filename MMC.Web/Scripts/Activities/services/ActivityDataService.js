(function () {
    var app = angular.module("appMain");
    var activityDataService = function ($http, $q, $window) {

        var getSelectedActivityTypes = function () {
            var deferred = $q.defer();
            if (purl().segment().length > 2) {
                if (purl().segment(2).trim().length > 0 && purl().segment(3).trim().length) {
                    var selectedLocation = purl().segment(2);
                    var selectedActivityType = purl().segment(3);
                    $http({
                        url: '/Activities/GetSelectedActivityType',
                        method: 'GET',
                        params: { selectedLocation: selectedLocation, selectedActivityCategory: selectedActivityType }
                    }).success(deferred.resolve).error(deferred.reject);
                }
            }
            return deferred.promise;
        }

        var getSelectedActivitiesForSelectedLocation = function (selectedActivityType) {
            var deferred = $q.defer();
            var selectedLocation = "GANGTOK";
            if (selectedLocation.length > 1) {
                $http({
                    url: '/Activities/GetSelectedActivityType',
                    method: 'GET',
                    params: { selectedLocation: selectedLocation, selectedActivityCategory: selectedActivityType }
                }).success(deferred.resolve).error(deferred.reject);
            }
            return deferred.promise;
        }

        var getAllAvailableLocations = function () {
            var deferred = $q.defer();
            $http.get('/Location/GetAllOtherLocations').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var clearDateFilters = function () {
            var deferred = $q.defer();
            $http.get('/Activities/ClearDateFilters').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var clearActivityTypeFilters = function () {
            var deferred = $q.defer();
            $http.get('/Activities/ClearActivityTypeFilter').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var getSelectedActivity = function (activityKey) {
            if (WURFL.is_mobile) {
                if (purl().segment().length > 2) {
                    if (purl().segment(2).trim().length > 0 && purl().segment(3).trim().length) {
                        var selectedLocation = purl().segment(2);
                        $window.location.href = "/ActivityDetails/" + selectedLocation + "/" + activityKey;
                    }
                }
            }
            else {
                if (purl().segment().length >= 2) {
                    if (purl().segment(1).trim().length > 0 && purl().segment(2).trim().length) {
                        var selectedLocation = purl().segment(2);
                        $window.location.href = "/ActivityDetails/" + selectedLocation + "/" + activityKey;
                    }
                }
            }
        }
        var getAllFilters = function () {
            var deferred = $q.defer();
            $http({
                url: '/Activities/GetAllFilters',
                method: 'GET'
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var setActivityTypeFilter = function (activityTypes) {
            var deferred = $q.defer();
            var data = angular.toJson(activityTypes);
            $http({
                url: '/Activities/SetActivityTypeFilter',
                method: 'GET',
                params: { activityTypes: data }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getSelectedActivityTypes: getSelectedActivityTypes,
            getSelectedActivity: getSelectedActivity,
            clearDateFilters: clearDateFilters,
            clearActivityTypeFilters: clearActivityTypeFilters,
            getAllFilters: getAllFilters,
            setActivityTypeFilter: setActivityTypeFilter,
            getSelectedActivitiesForSelectedLocation: getSelectedActivitiesForSelectedLocation,
        }
    };
    app.factory("ActivityDataService", activityDataService);
}());
