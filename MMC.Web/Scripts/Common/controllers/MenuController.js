(function () {
    var app = angular.module("appMain");
    var menuController = function ($scope, $http, $timeout, $interval, $location, MenuDataService) {
        $scope.isDesktop = !WURFL.is_mobile;
        $scope.removeFilter = false;
        $scope.LoginStatus = "Login";
        /***************************LOADING SELECTED GUEST INFORMATION *************************/
        MenuDataService.getGuestInformation().then(function (response) {
            if (response.GuestKey != null) {
                $scope.userDetails = response;
                $scope.LoginStatus = "Logout";
                $scope.UserName = "Welcome - " + $scope.userDetails.Name;
            }
            else {
                $scope.LoginStatus = "Login";
                $scope.UserName = "";
            }
        });
        /*ENDLOADING SELECTED GUEST INFORMATION*/

        var checkForActivities = function () {
            MenuDataService.getUsersActivityCartCount().then(function (response) {
                if (response > 0) {
                    $scope.ActivityCount = response;
                    if (WURFL.is_mobile) {
                        if (!$(".nav-button").hasClass("info")) {
                            $(".nav-button").toggleClass("info");
                        }
                    }
                    else {

                    }                    
                }
                else {
                    $scope.ActivityCount = null;
                    if (WURFL.is_mobile) {
                        if ($(".nav-button").hasClass("info")) {
                            $(".nav-button").toggleClass("info");
                        }
                    }
                    else {

                    }
                }
                $scope.ActivityInCart = function () {
                    if ($scope.ActivityCount != null && $scope.ActivityCount > 0) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            });           
        }
        $scope.userLogIn = function () {
            if ($scope.userDetails != null && $scope.userDetails.GuestKey != null) {
                return true;
            }
            else {
                return false;
            }
        }
        
        /*UPDATING THE NUMBER OF ACTIVITIES BOOKED IN MENU CONTROLLER*/
        $scope.$on("ACTIVITYUPDATEBR", function (event, args) {
            checkForActivities();
        });

        $scope.Login = function () {
            var returnURL = '';
            for (i = 1; i <= purl().segment().length ; i++) {
                returnURL = returnURL + purl().segment(i) + "/";
            }
            if ($scope.userDetails == null) {
                MenuDataService.loginUser(returnURL);
            }
            else if ($scope.userDetails.GuestKey == null) {
                MenuDataService.loginUser(returnURL);
            }
            else {
                //logout
            }

        }

        $scope.showCart = function () {
            MenuDataService.showUserActivityCart();
        }
        /*END UPDATING THE NUMBER OF ACTIVITIES BOOKED IN MENU CONTROLLER*/

        /*FIRST TIME CHECK IN THE MENU IF THERE ARE ANY ACTIVITIES ADDED */
        checkForActivities();
        $(window).resize(function () {
            if (window.styleMedia.matchMedium("screen and (max-width:600px)")) {
                $scope.removeFilter = true;
            }
            else {
                $scope.removeFilter = false;
            }
        })
    }
    app.controller("MenuController", menuController);
}());