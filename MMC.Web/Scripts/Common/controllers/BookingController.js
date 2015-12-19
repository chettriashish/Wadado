(function () {
    var app = angular.module("appMain");
    var bookingController = function ($scope, $http, $timeout, $interval, $location, BookingDataService) {
        $scope.ErrorMessage = null;
        $scope.NumAdults = 1;
        $scope.NumChildren = 0;
        $scope.Error = false;
        $scope.time;
        $scope.Total = 0;
        $scope.AdultCost = 0;
        $scope.ChildCost = 0;
        var checkForAvailability = function () {
            BookingDataService.checkForActivityAvailability($scope.selectedActivityDetails.ActivityKey,
                $scope.NumAdults, $scope.NumChildren, $scope.date, $scope.time).then(function (result) {
                    if (!result.Status) {
                        $scope.ErrorMessage = result.Message;
                        $scope.Error = true;
                    }
                    else {
                        $scope.ErrorMessage = null;
                        $scope.Error = false;
                        if ($scope.selectedActivityDetails.NumChildren == 0) {
                            $scope.Total = $scope.selectedActivityDetails.Currency + " " + $scope.NumAdults * $scope.selectedActivityDetails.Cost;
                        }
                        else {
                            $scope.Total = $scope.selectedActivityDetails.Currency + " " + ($scope.NumAdults * $scope.selectedActivityDetails.Cost +
                                $scope.NumChildren * $scope.selectedActivityDetails.CostForChild);
                        }
                    }
                });
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

        $scope.dateSelected = function (date) {
            $scope.date = date;
            if (!$(".booking-wrapper").hasClass("open")) {
                $(".booking-wrapper").addClass("open");
                $scope.Total = $scope.selectedActivityDetails.Currency + " " + $scope.NumAdults * $scope.selectedActivityDetails.Cost;
                $scope.ShowChildren = function () {
                    if ($scope.selectedActivityDetails.NumChildren > 0) {
                        $scope.Total = $scope.selectedActivityDetails.Currency + " " + ($scope.NumAdults * $scope.selectedActivityDetails.Cost +
                                $scope.NumChildren * $scope.selectedActivityDetails.CostForChild);
                        $scope.AdultCost = $scope.selectedActivityDetails.Currency + " " + $scope.selectedActivityDetails.Cost + "/" + "adult";
                        $scope.ChildCost = $scope.selectedActivityDetails.Currency + " " + $scope.selectedActivityDetails.CostForChild + "/" + "child";
                        return true;
                    }
                    else {
                        $scope.AdultCost = $scope.selectedActivityDetails.Currency + " " + $scope.selectedActivityDetails.Cost + "/" + "person";
                        return false;
                    }
                }
                $scope.$apply();
            }
            if ($scope.NumAdults > 1 || $scope.NumChildren > 1) {
                checkForAvailability();
            }
        }

        $scope.addActivityToCart = function () {
            var total = 0;
            if ($scope.selectedActivityDetails.NumChildren == 0) {
                total = $scope.NumAdults * $scope.selectedActivityDetails.Cost;
            }
            else {
                total = ($scope.NumAdults * $scope.selectedActivityDetails.Cost +
                    $scope.NumChildren * $scope.selectedActivityDetails.CostForChild);
            }
           
            BookingDataService.addSelectedActivityToUsersCart($scope.selectedActivityDetails.ActivityKey,
               $scope.NumAdults, $scope.NumChildren, $scope.date, $scope.time, total).then(function (response) {
                   console.log("activity added to cart");
               });
        }
    }
    app.controller("BookingController", bookingController);
}());