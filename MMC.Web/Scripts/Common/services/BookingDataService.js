(function () {
    var app = angular.module("appMain");
    var bookingDataService = function ($http, $q) {
        var checkForActivityAvailability = function (activityKey, numAdults, numChildren, date, time) {
            var deferred = $q.defer();
            $http({
                url: '/Booking/CheckForActivityAvailability',
                method: 'GET',
                params: { selectedActivityKey: activityKey, numAdults: numAdults, numChildren: numChildren, date: date, time: time }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        return {
            checkForActivityAvailability: checkForActivityAvailability,
        };
    };
    app.factory("BookingDataService", bookingDataService);
}());