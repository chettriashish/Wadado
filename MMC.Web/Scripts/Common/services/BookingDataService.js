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

        var addSelectedActivityToUsersCart = function (activityKey, activityPricingKey, numAdults, numChildren, date, time, total) {
            var deferred = $q.defer();
            $http({
                url: '/Booking/AddSelectedActivityToUsersCart',
                method: 'GET',
                params: { selectedActivityKey: activityKey, selectedActivityPriceOptionsKey: activityPricingKey, numAdults: numAdults, numChildren: numChildren, bookingDate: date, bookingTime: time, total: total }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        return {
            checkForActivityAvailability: checkForActivityAvailability,
            addSelectedActivityToUsersCart: addSelectedActivityToUsersCart,
        };
    };
    app.factory("BookingDataService", bookingDataService);
}());