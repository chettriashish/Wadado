(function () {
    var app = angular.module("appMain");
    var dateFilterDataService = function ($http, $q) {
        var filterDataByDateRangeSelected = function (startDate, endDate) {
            var deferred = $q.defer();
            if (purl().segment().length > 2) {
                if (purl().segment(2).trim().length > 0 && purl().segment(3).trim().length) {
                    var selectedLocation = purl().segment(2);
                    var selectedActivityType = purl().segment(3);
                    $http({
                        url: '/Activities/GetSelectedActivityTypeByDate',
                        method: 'GET',
                        params: { selectedLocation: selectedLocation, selectedActivityCategory: selectedActivityType, startDate: startDate, endDate: endDate }
                    }).success(deferred.resolve).error(deferred.reject);
                }
            }
            return deferred.promise;
        }        
        return {
            filterDataByDateRangeSelected: filterDataByDateRangeSelected,
            
        };
    };
    app.factory("DateFilterDataService", dateFilterDataService);
}());