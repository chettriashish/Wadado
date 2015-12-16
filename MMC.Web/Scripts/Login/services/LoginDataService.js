(function () {
    var app = angular.module("appMain");
    var loginDataService = function ($http, $q) {

        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
           //TBD USE METHODS FOR ASSISTING IN USER LOGIN
        }
    };
    app.factory("LoginDataService", loginDataService);
}());