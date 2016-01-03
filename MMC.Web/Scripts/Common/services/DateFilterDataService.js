(function () {
    var app = angular.module("appMain");
    var dateFilterDataService = function ($http, $q) {
        var filterDataByDateRangeSelected = function (startDate, endDate) {
            var deferred = $q.defer();
            if ($.url().segment().length > 2) {
                if ($.url().segment(2).trim().length > 0 && $.url().segment(3).trim().length) {
                    var selectedLocation = $.url().segment(2);
                    var selectedActivityType = $.url().segment(3);
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