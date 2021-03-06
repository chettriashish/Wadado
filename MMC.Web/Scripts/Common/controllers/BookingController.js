﻿(function () {
    var app = angular.module("appMain");
    var bookingController = function ($scope, $http, $timeout, $interval, $location, BookingDataService) {
        $scope.ErrorMessage = null;
        $scope.NumAdults = 1;
        $scope.NumChildren = 0;
        $scope.Error = false;
        $scope.selectedDate = '';
        $scope.time = {
            val: ""
        };
        $scope.Total = 0;
        $scope.AdultCost = 0;
        $scope.ChildCost = 0;
        var checkForAvailability = function () {
            var time = $scope.time.val;
            BookingDataService.checkForActivityAvailability($scope.selectedActivityDetails.ActivityKey,
                $scope.NumAdults, $scope.NumChildren, $scope.selectedDate, time).then(function (result) {
                    if (!result.Status) {
                        $scope.ErrorMessage = result.Message;
                        $scope.Error = true;
                    }
                    else {
                        $scope.ErrorMessage = null;
                        $scope.Error = false;
                        if ($scope.selectedActivityDetails.NumChildren == 0) {
                            $scope.Total = $scope.selectedActivityDetails.Currency + " " + $scope.NumAdults * $scope.selectedPriceOption.PriceForAdults;
                        }
                        else {
                            $scope.Total = $scope.selectedActivityDetails.Currency + " " + ($scope.NumAdults * $scope.selectedPriceOption.PriceForAdults +
                                $scope.NumChildren * $scope.selectedPriceOption.PriceForChildren);
                        }
                    }
                });
        }

        $scope.selectTime = function () {
            checkForAvailability();
        }

        $scope.addGuests = function (obj) {
            if (obj == 'A') {
                if (!$scope.Error) {
                    $scope.NumAdults = $scope.NumAdults + 1;
                }
            }
            else if (obj == 'C') {
                if (!$scope.Error) {
                    $scope.NumChildren = $scope.NumChildren + 1;
                }
            }
            checkForAvailability();
        }

        $scope.removeGuests = function (obj) {
            if (obj == 'A') {
                if ($scope.NumAdults > 1) {
                    $scope.NumAdults = $scope.NumAdults - 1;
                }
            }
            else if (obj == 'C') {
                if ($scope.NumChildren > 1) {
                    $scope.NumChildren = $scope.NumChildren - 1;
                }
            }
            checkForAvailability();
        }

        $scope.onDateSet = function (selectedDate) {            
            $scope.selectedDate = selectedDate;
            if ($scope.selectedDate != '') {
                spinner();
                if (!$(".booking-wrapper").hasClass("open")) {
                    $(".desktop-book").addClass("hide");
                    $(".booking-wrapper").addClass("open");
                    $scope.Total = $scope.selectedActivityDetails.Currency + " " + $scope.NumAdults * $scope.selectedPriceOption.PriceForAdults;
                    $scope.ShowChildren = function () {
                        if ($scope.selectedActivityDetails.NumChildren > 0) {
                            $scope.Total = $scope.selectedActivityDetails.Currency + " " + ($scope.NumAdults * $scope.selectedPriceOption.PriceForAdults +
                                    $scope.NumChildren * $scope.selectedPriceOption.PriceForChildren);
                            $scope.AdultCost = $scope.selectedActivityDetails.Currency + " " + $scope.selectedPriceOption.PriceForAdults + "/" + "adult";
                            $scope.ChildCost = $scope.selectedActivityDetails.Currency + " " + $scope.selectedPriceOption.PriceForChildren + "/" + "child";
                            return true;
                        }
                        else {
                            $scope.AdultCost = $scope.selectedActivityDetails.Currency + " " + $scope.selectedPriceOption.PriceForAdults + "/" + "person";
                            return false;
                        }
                        if ($scope.selectedActivityDetails.AllActivityTimes.length > 4) {
                            $scope.radio = false;
                            $scope.selectedTime = {};
                        }
                    }
                    //$scope.$apply();
                }
                if ($scope.NumAdults >= 1 || $scope.NumChildren >= 1) {
                    checkForAvailability();
                }
                spinner();
            }            
        }
        $scope.$on("OPTIONCHANGED", function (event, args)
        {
            $scope.currentPriceOption = {};
            $scope.currentPriceOption = args;
            checkForAvailability();
        });
        $scope.addActivityToCart = function () {          
            if ($scope.selectedDate != '') {
                spinner();
                BookingDataService.checkForActivityAvailability($scope.selectedActivityDetails.ActivityKey,
                $scope.NumAdults, $scope.NumChildren, $scope.selectedDate, $scope.time.val).then(function (result) {
                    if (!result.Status) {
                        $scope.ErrorMessage = result.Message;
                        $scope.Error = true;
                        spinner();
                    }
                    else {
                        var total = 0;
                        if ($scope.selectedActivityDetails.NumChildren == 0) {
                            total = $scope.NumAdults * $scope.selectedPriceOption.PriceForAdults; //$scope.selectedActivityDetails.Cost;
                        }
                        else {
                            total = ($scope.NumAdults * $scope.selectedPriceOption.PriceForAdults +
                                $scope.NumChildren * $scope.selectedPriceOption.PriceForChildren);
                        }
                        BookingDataService.addSelectedActivityToUsersCart($scope.selectedActivityDetails.ActivityKey, $scope.currentPriceOption.ActivityPricingKey,
                           $scope.NumAdults, $scope.NumChildren, $scope.selectedDate, $scope.time.val, total).then(function (response) {
                               console.log("activity added to cart");
                               $scope.$emit("ACTIVITYUPDATED", { message: "ACTIVITYUPDATED" })
                               spinner();
                           });
                    }                    
                });
            }
            else {
                $(".error-init").addClass("show");
                $scope.ErrorMessage ="Please Select a Date";
                $scope.Error = true;
            }

        }       
        /***************SHOW/HIDE THE LOADING SPINNER*************************/
        var spinner = function () {
            if ($('.spinner').hasClass('show')) {
                $('.spinner').removeClass('show');
            }
            else {
                $('.spinner').addClass('show');
            }
        }
    }
    app.controller("BookingController", bookingController);
}());