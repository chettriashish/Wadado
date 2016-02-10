(function () {
    var app = angular.module("appMain");

    var activitiesCartController = function ($scope, $http, $timeout, $interval, $location, $routeParams, ActivitiesCartDataService) {
        $scope.selectedLocation = "Select location";
        var setImages = function () {
            if (WURFL.is_mobile) {
                $.each($scope.activities, function (key, value) {
                    /*SETTING LOCATION INFORMATION*/
                    $scope.activities[key].ActivityWithLocation = $scope.activities[key].ActivityName + "\t-\t" + $scope.activities[key].Location + "";
                    //Setting Dates Correctly
                    var regex = /[0-9]+/;
                    var date = $scope.activities[key].BookingDate;
                    var result = Number(date.match(regex)[0]);
                    $scope.activities[key].f_BookingDate = new Date(result).toDateString();
                    $scope.activities[key].f_Cost = $scope.activities[key].Currency + "\t" + $scope.activities[key].Cost + "";
                    $scope.activities[key].f_pax = $scope.activities[key].Participants + "\tPAX\t,";
                    if (WURFL.form_factor == "Smartphone") {
                        if (window.styleMedia.matchMedium("screen and (max-width:500px)")) {
                            $scope.activities[key].ImageURL = Wadado.rootPath + "/" + $scope.activities[key].ThumbnailImage + "_portrait.jpg";
                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:550px)")) {
                            $scope.activities[key].ImageURL = Wadado.rootPath + "/" + $scope.activities[key].ThumbnailImage + "_landscape.jpg";
                        }
                    }
                    else {
                        if (window.styleMedia.matchMedium("screen and (max-width:800px)")) {
                            $scope.activities[key].ImageURL = Wadado.rootPath + "/" + $scope.activities[key].ThumbnailImage + "_portrait.jpg";
                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:900px)")) {
                            $scope.activities[key].ImageURL = Wadado.rootPath + "/" + $scope.activities[key].ThumbnailImage + "_landscape.jpg";
                        }
                    }
                });
            }
            else {
                $.each($scope.activities, function (key, value) {
                    /*SETTING LOCATION INFORMATION*/
                    $scope.activities[key].ActivityWithLocation = $scope.activities[key].ActivityName + "\t-\t" + $scope.activities[key].Location + "";
                    //Setting Dates Correctly
                    var regex = /[0-9]+/;
                    var date = $scope.activities[key].BookingDate;
                    var result = Number(date.match(regex)[0]);
                    $scope.activities[key].f_BookingDate = new Date(result).toDateString();
                    $scope.activities[key].f_BookingDate = new Date(result).toDateString();
                    $scope.activities[key].f_Cost = $scope.activities[key].Currency + "\t" + $scope.activities[key].Cost + "";
                    $scope.activities[key].f_pax = $scope.activities[key].Participants + "\tPAX\t,";
                    $scope.activities[key].ImageURL = Wadado.rootPath + "/" + $scope.activities[key].ThumbnailImage + "_portrait.jpg";
                });
            }
        };

        $scope.showActivityCart = function () {
            if ($scope.activities != null && $scope.activities.length > 0
                && $scope.activities[0].ActivityKey != null) {
                return true;
            }
            else {
                return false;
            }
        }

        ActivitiesCartDataService.getUserActivityCart().then(function (response) {
            $scope.activities = response;
            //$scope.Total = 0;
            //for (i = 0; i < $scope.activities.length;i++ ){
            //    $scope.Total = $scope.Total + $scope.activities[i].PaymentAmount;
            //}
            $scope.Count = $scope.activities.length;
            
            $scope.showChildInfo = function () {
                if (response.ChildParticipants > 0) {
                    return true;
                }
                else {
                    return false;
                }
            }

            $scope.isINR = function () {
                if (response[0].Currency == "INR") {
                    return true;
                }
                else {
                    return false;
                }
            }
            setImages();
        });
        $(window).resize(function () {
            setImages();
            $scope.$apply();
        });
    }
    app.controller("ActivitiesCartController", activitiesCartController);
}());
