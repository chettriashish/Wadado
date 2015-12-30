(function () {
    var app = angular.module("appMain");
    var menuDataService = function ($http, $q, $window) {

        var getGuestInformation = function () {
            var deferred = $q.defer();
            $http.get('/Menu/GetGuestInformation').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        var getUsersActivityCartCount = function () {
            var deferred = $q.defer();
            $http.get('/Menu/GetUsersActivityCartCount').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var loginUser = function (returnURL) {            
            $window.location.href = "/login?returnUrl=" + returnURL;
        }

        var showUserActivityCart = function () {
            $window.location.href = "/ActivitiesCart";
        }

        return {
            getGuestInformation: getGuestInformation,
            getUsersActivityCartCount: getUsersActivityCartCount,
            loginUser: loginUser,
            showUserActivityCart: showUserActivityCart,
        };
    };
    app.factory("MenuDataService", menuDataService);
}());