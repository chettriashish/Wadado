(function () {
    var app = angular.module("appMain");
    var activityDataService = function ($http, $q) {        

        var getSelectedActivityTypes = function () {
            var deferred = $q.defer();
            if ($.url().segment().length > 2) {
                if ($.url().segment(2).trim().length > 0 && $.url().segment(3).trim().length) {
                    var selectedLocation = $.url().segment(2);
                    var selectedActivityType = $.url().segment(3);
                    $http({
                        url: '/Activities/GetSelectedActivityType',
                        method: 'GET',
                        params: { selectedLocation: selectedLocation, selectedActivityType: selectedActivityType }
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
        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getSelectedActivityTypes: getSelectedActivityTypes,            
        }
    };
    app.factory("ActivityDataService", activityDataService);
}());
