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

        var getAllActivitiesForSelectedLocation = function (item) {
            var deferred = $q.defer();
            $http({
                url: 'AdminActivity/GetAllActivitiesByLocation',
                method: 'GET',
                params: { locationKey: item.selected.LocationKey }
            }).success(deferred.resolve).error(deferred.reject)

            return deferred.promise;
        }

        var getSelectedActivityDetails = function (activityKey) {
            $http({
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

        var saveActivityDetails = function (item) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '',
                data: { activityDetails: item }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getAllAvailableLocations: getAllAvailableLocations,
            getAllActivitiesForSelectedLocation: getAllActivitiesForSelectedLocation,
            getSelectedActivityDetails: getSelectedActivityDetails,
            saveActivityDetails: saveActivityDetails,
        }
    };
    app.factory("AdminActivityDataService", adminActivityDataService);
}());