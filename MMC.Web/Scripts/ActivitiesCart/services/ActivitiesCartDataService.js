(function () {
    var app = angular.module("appMain");
    var activitiesCartDataService = function ($http, $q, $window) {       

        var getUserActivityCart = function () {
            var deferred = $q.defer();
            $http.get('/ActivitiesCart/GetUsersActivityCart').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var removeActivityFromUserCart = function (activityBookingKey) {
            var deferred = $q.defer();            
            $http({
                url: '/ActivitiesCart/RemoveSelectedActivityFromUsersCart',
                method: 'GET',
                params: { activityBookingKey: activityBookingKey }
            }).success(deferred.resolve).error(deferred.reject);          
            return deferred.promise;
        }

        var proceedToPayment = function (firstName, lastName, phoneNumber, email) {
            var deferred = $q.defer();
            $http({
                url: '/ActivitiesCart/ProceedToPayment',
                method: 'GET',
                params: { firstName: firstName, lastName:lastName, phoneNumber:phoneNumber, email:email }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        var userValidatedSendToPayment = function () {
            $window.location.href = "/Confirmation";
        }
        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getUserActivityCart: getUserActivityCart,
            removeActivityFromUserCart: removeActivityFromUserCart,
            proceedToPayment: proceedToPayment,
            userValidatedSendToPayment: userValidatedSendToPayment,
        }
    };
    app.factory("ActivitiesCartDataService", activitiesCartDataService);
}());