(function () {
    var app = angular.module("appMain");
    var activityDataService = function ($http, $q, $window) {

        var getSelectedActivityTypes = function () {
            var deferred = $q.defer();
            if ($.url().segment().length > 2) {
                if ($.url().segment(2).trim().length > 0 && $.url().segment(3).trim().length) {
                    var selectedLocation = $.url().segment(2);
                    var selectedActivityType = $.url().segment(3);
                    $http({
                        url: '/Activities/GetSelectedActivityType',
                        method: 'GET',
                        params: { selectedLocation: selectedLocation, selectedActivityCategory: selectedActivityType }
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
            if ($.url().segment().length > 2) {
                if ($.url().segment(2).trim().length > 0 && $.url().segment(3).trim().length) {
                    var selectedLocation = $.url().segment(2);
                    $window.location.href = "/ActivityDetails/" + selectedLocation + "/" + activityKey;
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
        }
    };
    app.factory("ActivityDataService", activityDataService);
}());
