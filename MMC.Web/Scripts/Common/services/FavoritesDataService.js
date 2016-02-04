(function () {
    var app = angular.module("appMain");
    var favoritesDataService = function ($http, $q, $window) {
        var addFavorites = function (activityKey) {
            var deferred = $q.defer();
            $http({
                url: '/Account/AddToFavorites',
                method: 'GET',
                params: { activityKey: activityKey }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        var removeFavorites = function (activityKey) {
            var deferred = $q.defer();
            $http({
                url: '/Account/RemoveFromFavorites',
                method: 'GET',
                params: { activityKey: activityKey }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        var removeFavorites = function (activityKey) {
            var deferred = $q.defer();
            $http({
                url: '/Account/RemoveFromFavorites',
                method: 'GET',
                params: { activityKey: activityKey }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        var checkIfActivityInGuestFavorites = function (activityKey) {
            var deferred = $q.defer();
            $http({
                url: '/Account/CheckIfActivityInGuestFavorites',
                method: 'GET',
                params: { activityKey: activityKey }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var checkIfUserLoggedIn = function () {
            var deferred = $q.defer();
            $http.get('/Account/CheckIfUserLoggedIn').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;            
        }

        var storeAction = function (activityKey) {
            var deferred = $q.defer();
            var userAction = "a_f";
            var returnURL = '';
            for (i = 1; i <= purl().segment().length ; i++) {
                returnURL = returnURL + purl().segment(i) + "/";
            }
            $http({
                url: '/Account/StoreAction',
                method: 'GET',
                params: { userAction: userAction, returnURL: returnURL, activityKey: activityKey }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        var loginUser = function () {
            $window.location.href = "/login";
        }
        return {
            addFavorites: addFavorites,
            removeFavorites: removeFavorites,
            checkIfActivityInGuestFavorites: checkIfActivityInGuestFavorites,
            storeAction: storeAction,
            loginUser: loginUser,
            checkIfUserLoggedIn: checkIfUserLoggedIn,
        };
    };
    app.factory("FavoritesDataService", favoritesDataService);
}());