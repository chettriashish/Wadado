(function () {
    var app = angular.module("appMain");
    var adminLocationSummaryDataService = function ($http, $q) {

        var getAllAvailableLocations = function () {
            //var deferred = $q.defer();
            return $http.get('/AdminLocation/GetAllLocations').then(
                function (response) {
                    return response.data;
                })
            .catch(function (response) {
                console.log(response);
            });
            //return deferred.promise;
        }

        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getAllAvailableLocations: getAllAvailableLocations,
        }
    };
    app.factory("AdminLocationSummaryDataService", adminLocationSummaryDataService);
}());