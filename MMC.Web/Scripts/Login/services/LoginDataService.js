(function () {
    var app = angular.module("appMain");
    var loginDataService = function ($http, $q, $window) {
        window.fbAsyncInit = function () {
            FB.init({
                appId: '963933207007150',
                cookie: true,  // enable cookies to allow the server to access 
                // the session
                xfbml: true,  // parse social plugins on this page
                version: 'v2.2' // use version 2.2
            });
        }
        var logUserSession = function () {
            var deferred = $q.defer();
            $http.get('/Account/LogUserSession').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        };

        var saveUserDetails = function (userName, userKey, userMail, loginMethod) {
            var deferred = $q.defer();
            $http({
                url: '/Account/SaveUserDetails',
                method: 'GET',
                params: { userName: userName, userKey: userKey, userMail: userMail, loginMethod: loginMethod }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var checkForAction = function () {
            var deferred = $q.defer();
            $http.get('/Account/CheckForAction').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        };

        var getGuestInformation = function () {
            var deferred = $q.defer();
            $http.get('/Account/GetGuestInformation').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        };

        var logUserOut = function () {
            var deferred = $q.defer();
            $http.get('/Account/LogUserOut').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        };
        var addFavorites = function (activityKey) {
            var deferred = $q.defer();
            $http({
                url: '/Account/AddToFavorites',
                method: 'GET',
                params: { activityKey: activityKey }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        var returnUser = function (returnURL) {
            if (returnURL.length < 4) {
                /************FOR LOCAL TESTING*************/
                //returnURL = "http://localhost:4197/";
                /************FOR TESTING*************/
                returnURL = "http://www.thefigtree.in";
            }
            $window.location.href = returnURL.replace(/\/$/, "");
        }
        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            logUserSession: logUserSession,
            saveUserDetails: saveUserDetails,
            getGuestInformation: getGuestInformation,
            logUserOut: logUserOut,
            returnUser: returnUser,
            checkForAction: checkForAction,
            addFavorites: addFavorites,
        }
    };
    app.factory("LoginDataService", loginDataService);
}());