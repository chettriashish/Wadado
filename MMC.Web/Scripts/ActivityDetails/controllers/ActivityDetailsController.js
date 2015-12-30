(function () {
    var app = angular.module("appMain");
    app.filter('propsFilter', function () {
        return function (items, props) {
            var out = [];

            if (angular.isArray(items)) {
                var keys = Object.keys(props);

                items.forEach(function (item) {
                    var itemMatches = false;

                    for (var i = 0; i < keys.length; i++) {
                        var prop = keys[i];
                        var text = props[prop].toLowerCase();
                        if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                            itemMatches = true;
                            break;
                        }
                    }

                    if (itemMatches) {
                        out.push(item);
                    }
                });
            } else {
                // Let the output be the input untouched
                out = items;
            }

            return out;
        };
    });
    var activityDetailsController = function ($scope, $http, $timeout, $interval, $location, $routeParams, ActivityDetailsDataService) {
        var sliderInit = false;
        var setActivityImages = function () {
            $scope.ImageURL = [];

            if (WURFL.is_mobile) {
                $scope.selectedActivityDetails.ActivityImagesURL = {};
                $.each($scope.selectedActivityDetails.ActivityImages, function (key, value) {
                    if (WURFL.form_factor == "Smartphone") {
                        if (window.styleMedia.matchMedium("screen and (max-width:500px)")) {
                            $scope.selectedActivityDetails.ActivityImagesURL[key] = Wadado.rootPath + "/" + $scope.selectedActivityDetails.ActivityImages[key] + "_portrait.jpg";
                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:550px)")) {
                            $scope.selectedActivityDetails.ActivityImagesURL[key] = Wadado.rootPath + "/" + $scope.selectedActivityDetails.ActivityImages[key] + "_landscape.jpg";
                        }
                    }
                    else {
                        if (window.styleMedia.matchMedium("screen and (max-width:800px)")) {
                            $scope.selectedActivityDetails.ActivityImagesURL[key] = Wadado.rootPath + "/" + $scope.selectedActivityDetails.ActivityImages[key] + "_portrait.jpg";
                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:900px)")) {
                            $scope.selectedActivityDetails.ActivityImagesURL[key] = Wadado.rootPath + "/" + $scope.selectedActivityDetails.ActivityImages[key] + "_landscape.jpg";
                        }
                    }
                });
            }
            if (sliderInit == true) {
                $scope.$apply();
            }
            else if (sliderInit == false) {
                setTimeout(function () {
                    ActivityDetailsDataService.setActivityImageSlider();
                }, 100);
            }
        };
        /*START SELECTED ACTIVITY TYPE*/
        $scope.selectedActivities = {};
        $scope.bookingViews = [];

        $scope.hideActivityDetails = function () {
            return !sliderInit;
        }
        /*UPDATING THE ACTIVITIES COUNT IN THE MENU CONTROLLER ONLY AFTER AN ACTIVITY HAS BEEN ADDED TO THE LIST*/
        $scope.$on("ACTIVITYUPDATED", function (event, args) {
            $scope.$broadcast('ACTIVITYUPDATEBR', { message: "ACTIVITYUPDATEBR" });
        });
        /*END UPDATING THE ACTIVITIES COUNT IN THE MENU CONTROLLER ONLY AFTER AN ACTIVITY HAS BEEN ADDED TO THE LIST*/

        ActivityDetailsDataService.getSelectedActivityDetails().then(function (selectedActivityDetails) {
            $scope.selectedActivityDetails = selectedActivityDetails;
            /*SET LOCATION FOR ACTIVITY ONLY IF THE THE ACTIVITY EXISTS AND DATA IS RETURNED FROM THE SERVER CORRECTLY*/
            if ($.url().segment().length > 2) {
                if ($.url().segment(2).trim().length > 0 && $.url().segment(3).trim().length) {
                    setActivityImages();
                    $scope.NumberOfPeople = $scope.selectedActivityDetails.MinPeople + "-" + $scope.selectedActivityDetails.MaxPeople;
                    $scope.selectedLocation = $.url().segment(2);
                }
            }
            $scope.showBookingDetails = function () {
                return true;
            }

            $scope.Cost = $scope.selectedActivityDetails.Currency + " " + $scope.selectedActivityDetails.Cost + "/" + "person";
            if (WURFL.form_factor == "Smartphone") {
                $scope.limit = $scope.selectedActivityDetails.Description.length / 1.5;
            }
            else {
                $scope.limit = $scope.selectedActivityDetails.Description.length / 1.2;
            }
            $scope.descriptionAction = "more..";
            /********************UNDERLINING THE FIRST WORD */
            var underline = $('.header-left').each(function () {
                var words = $scope.selectedActivityDetails.Name.split(' ');
                $(this).empty().html(function () {
                    for (i = 0; i < words.length; i++) {
                        if (i == 0) {
                            $(this).append('<span>' + words[i] + '</span>');
                        }
                        else {
                            $(this).append(' <span>' + words[i] + '</span>');
                        }
                    }
                });
            });

            var rating = function () {
                var result = Math.round($scope.selectedActivityDetails.UserRating);
                var half = false;
                if (result > $scope.selectedActivityDetails.UserRating) {
                    result = result - 1;
                    half = true;
                }
                if (!($("#userRating div:has(img)").length > 0)) {
                    for (i = 0; i < result; i++) {
                        $("#userRating").append('<div style="float:left;"><img src="../../Images/Icons/full_star.png"></div>')
                    }
                    if (half) {
                        $("#userRating").append('<div style="float:left;"><img src="../../Images/Icons/half_star.png"></div>')
                    }
                    if (half == true) {
                        for (i = 0; i < (5 - (result + 1)) ; i++) {
                            $("#userRating").append('<div style="float:left;"><img src="../../Images/Icons/line_star.png"></div>')
                        }
                    }
                    else {
                        for (i = 0; i < (5 - result) ; i++) {
                            $("#userRating").append('<div style="float:left;"><img src="../../Images/Icons/line_star.png"></div>')
                        }
                    }
                    //result = Math.round($scope.selectedActivityDetails.DifficultyRating);
                    //half = false;
                    //if (result > $scope.selectedActivityDetails.DifficultyRating) {
                    //    result = result - 1;
                    //    half = true;
                    //}
                    //for (i = 0; i < result; i++) {
                    //    $("#diffRating").append('<div><img src="../../Images/Icons/full_star.png"></div>')
                    //}
                    //if (half) {
                    //    $("#diffRating").append('<div><img src="../../Images/Icons/half_star.png"></div>')
                    //}
                    //if (half == true) {
                    //    for (i = 0; i < (5 - (result + 1)) ; i++) {
                    //        $("#diffRating").append('<div><img src="../../Images/Icons/line_star.png"></div>')
                    //    }
                    //}
                    //else {
                    //    for (i = 0; i < (5 - result) ; i++) {
                    //        $("#diffRating").append('<div><img src="../../Images/Icons/line_star.png"></div>')
                    //    }
                    //}
                }
            };
            /*********SHOW BOOKING DETAILS ONLY AFTER DATA HAS BEEN FETCHED FROM SERVER*************/
            $scope.showActivityDetails = function () {
                rating();
                sliderInit = true;
                return sliderInit;
            }
            /*************SHOW BOOKING DETAILS ONLY AFTER DATA HAS BEEN FETCHED FROM SERVER********/
        });

        $scope.showMore = function () {
            if ($scope.descriptionAction.indexOf("more") != -1) {
                $scope.descriptionAction = "less";
                $scope.limit = $scope.selectedActivityDetails.Description.length / 1;
            }
            else {
                $scope.descriptionAction = "more..";
                if (WURFL.form_factor == "Smartphone") {
                    $scope.limit = $scope.selectedActivityDetails.Description.length / 1.5;
                }
                else {
                    $scope.limit = $scope.selectedActivityDetails.Description.length / 1.2;
                }
            }
        }
        /*END SELECTED ACTIVITY TYPE*/
        $(window).resize(function () {
            setActivityImages();
        });
    }
    app.controller("ActivityDetailsController", activityDetailsController);
}());
