(function () {
    var app = angular.module("appMain");

    var activitiesCartController = function ($scope, $http, $timeout, $interval, $location, $routeParams, ActivitiesCartDataService) {
        $scope.selectedLocation = "Select location";
        $scope.userInfo = {
            firstName:"",
            lastName: "",
            phoneNumber: "",
            email:""
        };
        var setLayout = function () {
            if (window.styleMedia.matchMedium("screen and (max-width:900px)")) {
                $scope.largeScreen = false;
            }
            else {
                $scope.largeScreen = true;
            }
        }
        setLayout();

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
                    $scope.activities[key].f_pax = $scope.activities[key].Participants + (parseInt($scope.activities[key].Participants) > 1 ? "\PAX\t," : "\PAX\t,");
                    $scope.Total = $scope.Total + parseFloat($scope.activities[key].Cost);
                    $scope.activities[key].ImageURL = Wadado.rootPath + "/" + $scope.activities[key].ThumbnailImage + "_portrait.jpg";
                });
            }
            else {
                $.each($scope.activities, function (key, value) {
                    /*SETTING LOCATION INFORMATION*/
                    $scope.activities[key].ActivityWithLocation = $scope.activities[key].ActivityName;
                    //Setting Dates Correctly
                    var regex = /[0-9]+/;
                    var date = $scope.activities[key].BookingDate;
                    var result = Number(date.match(regex)[0]);
                    $scope.activities[key].f_BookingDate = new Date(result).toDateString();
                    $scope.activities[key].f_BookingDate = new Date(result).toDateString();
                    $scope.activities[key].f_Cost = $scope.activities[key].Currency + "\t" + $scope.activities[key].Cost + "";
                    $scope.activities[key].f_pax = $scope.activities[key].Participants + (parseInt($scope.activities[key].Participants) > 1 ? "\tPersons\t," : "\tPerson\t,");
                    $scope.activities[key].ImageURL = Wadado.rootPath + "/" + $scope.activities[key].ThumbnailImage + "_portrait.jpg";
                    $scope.Total = $scope.Total + parseFloat($scope.activities[key].Cost);
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

        $scope.removeActivityFromUserCart = function (item) {
            //TBD NEED TO ADD USER CONFIRMATION
            ActivitiesCartDataService.removeActivityFromUserCart(item.ActivityBookingKey).then(function (response) {
                getUserActivityCart();
                $scope.$broadcast('ACTIVITYUPDATEBR', { message: "ACTIVITYUPDATEBR" });
            });
        }

        $scope.proceedToCart = function () {
            if ($scope.userInfo.firstName && $scope.userInfo.lastName && $scope.userInfo.phoneNumber && $scope.userInfo.email) {
                ActivitiesCartDataService.proceedToPayment($scope.userInfo.firstName, $scope.userInfo.lastName, $scope.userInfo.phoneNumber, $scope.userInfo.email).then(function (response) {
                    if (response == false) {
                        //error
                    }
                    else {
                        ActivitiesCartDataService.userValidatedSendToPayment();
                    }
                });
            }
        }

        var getUserActivityCart = function () {
            ActivitiesCartDataService.getUserActivityCart().then(function (response) {
                $scope.activities = response;
                groupBy($scope.activities, 'Location', 'Activity');

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
        }
        $(window).resize(function () {
            setLayout();
            $scope.$apply();
        });
        getUserActivityCart();
        /********************GROUPING BOOKINGS BY LOCATION***************************/
        var groupBy = function (data, key, type) {
            $scope.results = {};
            if (!(data && key)) return;
            if (!this.$id) {
                var result = {};
            } else {
                var scopeId = this.$id;
                if (!$scope.results[scopeId]) {
                    $scope.results[scopeId] = {};
                    this.$on("$destroy", function () {
                        delete $scope.results[scopeId];
                    });
                }
                result = $scope.results[scopeId];
            }

            for (var groupKey in $scope.result)
                result[groupKey].splice(0, result[groupKey].length);

            for (var i = 0; i < data.length; i++) {
                if (!result[data[i][key]])
                    result[data[i][key]] = [];
                result[data[i][key]].push(data[i]);
            }

            var keys = Object.keys(result);
            for (var k = 0; k < keys.length; k++) {
                if (result[keys[k]].length === 0)
                    delete result[keys[k]];
            }
            if (type === 'Activity') {
                $scope.activityResults = result;
                return $scope.activityResults;
            }           
        };
    }
    app.controller("ActivitiesCartController", activitiesCartController);
}());
