﻿(function () {
    var app = angular.module("appMain");

    var loginController = function ($scope, $http, $timeout, $interval, $location, LoginDataService) {
        $scope.accountModel = new Wadado.AccountLoginModel();
        $scope.returnUrl = '';
        $scope.UserName = null;
        $scope.LoginStatus = "Login";
        $scope.AllowGuest = false;
        var statusChangeCallback = function (response) {
            // The response object is returned with a status field that lets the
            // app know the current login status of the person.
            // Full docs on the response object can be found in the documentation
            // for FB.getLoginStatus().
            if (response.status === 'connected') {
                return true;
            }
            else {
                return false;
            }
        }

        var logUserInformation = function (userName, userInfo, mail) {
            LoginDataService.logUserSession().then(function (response) {
                LoginDataService.saveUserDetails(userName, userInfo, mail, "f_b").then(function (response) {
                    if ($scope.Action == "a_f") {
                        LoginDataService.addFavorites($scope.ActivityKey).then(function (response) {
                            LoginDataService.returnUser($scope.returnUrl);
                        });
                    }
                    else {
                        LoginDataService.returnUser($scope.returnUrl);
                    }
                });
            });
        }

        var checkLoginState = function () {
            FB.getLoginStatus(function (response) {
                statusChangeCallback(response);
                if (response.status != 'connected') {
                    FB.login(function (response) {
                        if (response.authResponse) {
                            FB.api('/me', function (response) {
                                console.log(response);
                                logUserInformation(response.name, response.id, response.mail);
                            });
                        }
                    });
                }
                else {
                    FB.api('/me', function (response) {
                        logUserInformation(response.name, response.id, response.mail);
                    });
                }
            });
        }

        $scope.FBLogin = function () {
            checkLoginState();
        };

        $scope.FBLogout = function () {
            function logout() {
                FB.logout(function (response) {
                    console.log(response);
                    LoginDataService.logUserOut().then(function (response) {
                        if (response == true) {
                            LoginDataService.returnUser($scope.returnUrl);
                        }
                    });
                });
            }
        }

        var getAllActions = function () {
            LoginDataService.checkForAction().then(function (response) {
                if (response.Action == "a_f") {
                    $scope.AllowGuest = false;
                    $scope.returnUrl = response.ReturnURL;
                    $scope.Action = response.Action;
                    $scope.ActivityKey = response.ActivityKey;
                }
                else {
                    $scope.AllowGuest
                }
            });
        }
        getAllActions();
        $scope.continueAsGuest = function () {
            LoginDataService.returnUser($scope.returnUrl);
        };
    }
    app.controller("LoginController", loginController);
}());