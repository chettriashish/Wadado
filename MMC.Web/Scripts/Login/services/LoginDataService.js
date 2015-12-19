(function () {
    var app = angular.module("appMain");
    var loginDataService = function ($http, $q) {
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

        var saveUserDetails = function (userName, userKey, userMail) {
            var deferred = $q.defer();
            $http({
                url: '/Account/SaveUserDetails',
                method: 'GET',
                params: { userName: userName, userKey: userKey, userMail: userMail }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;            
        }
        var getGuestInformation = function () {
            var deferred = $q.defer();
            $http.get('/Account/GetGuestInformation').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        };
        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            logUserSession: logUserSession,
            saveUserDetails: saveUserDetails,
            getGuestInformation: getGuestInformation,
        }
    };
    app.factory("LoginDataService", loginDataService);
}());