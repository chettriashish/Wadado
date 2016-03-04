(function () {
    var app = angular.module("appMain");
    var adminActivityDataService = function ($http, $q) {

        var getAllAvailableLocations = function () {
            return $http.get('/AdminLocation/GetAllLocations').then(
                function (response) {
                    return response.data;
                })
            .catch(function (response) {
                console.log(response);
            });
        }

        var getAllAvailableLocationsAsync = function () {
            var deferred = $q.defer();
            $http.get('/AdminLocation/GetAllLocations').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var getAllActivitiesForSelectedLocation = function (item) {
            var deferred = $q.defer();
            $http({
                url: 'AdminActivity/GetAllActivitiesByLocation',
                method: 'GET',
                params: { locationKey: item.selected.LocationKey }
            }).success(deferred.resolve).error(deferred.reject)

            return deferred.promise;
        }

        var createNewActivity = function () {
            return $http.get('/AdminActivity/CreateNewActivity').then(function (response) {
                return response.data;
            }).catch(function (response) {
                console.log(response);
            });
        }

        var getSelectedActivityDetails = function (activityKey) {
            return $http({
                method: 'GET',
                url: 'AdminActivity/GetSelectedActivityDetails',
                params: { activityKey: activityKey }
            })
                .then(function (response) {
                    return response.data
                })
                .catch(function (response) {
                    console.log(response);
                });
        }

        var saveActivityDetails = function (activityDetails, activityDays, activityTimes, activityCategoryKey, activityLocationKey) {
            var deferred = $q.defer();
            var time = [];
            $.each(activityTimes, function (key, value) {
                time.push(activityTimes[key].time);
            });
            $http({
                method: 'POST',
                url: 'AdminActivity/SaveActivityDetails',
                data: { activityDetails: activityDetails, activityDays: activityDays, activityTimes: time, activityCategoryKey: activityCategoryKey, activityLocationKey: activityLocationKey }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getAllAvailableLocations: getAllAvailableLocations,
            getAllActivitiesForSelectedLocation: getAllActivitiesForSelectedLocation,
            getSelectedActivityDetails: getSelectedActivityDetails,
            saveActivityDetails: saveActivityDetails,
            getAllAvailableLocationsAsync: getAllAvailableLocationsAsync,
            createNewActivity: createNewActivity,
        }
    };
    app.factory("AdminActivityDataService", adminActivityDataService);
}());