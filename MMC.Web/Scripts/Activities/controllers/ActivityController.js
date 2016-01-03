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
    var activityController = function ($scope, $http, $timeout, $interval, $location, $routeParams, ActivityDataService) {
        var setImages = function () {
            if (WURFL.is_mobile) {
                $.each($scope.allSelectedActivity, function (key, value) {

                    if (WURFL.form_factor == "Smartphone") {
                        if (window.styleMedia.matchMedium("screen and (max-width:500px)")) {
                            $scope.allSelectedActivity[key].DefaultImageURL = Wadado.rootPath + "/" + $scope.allSelectedActivity[key].ImageURL + "_portrait.jpg";
                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:550px)")) {
                            $scope.allSelectedActivity[key].DefaultImageURL = Wadado.rootPath + "/" + $scope.allSelectedActivity[key].ImageURL + "_landscape.jpg";
                        }
                    }
                    else {
                        if (window.styleMedia.matchMedium("screen and (max-width:800px)")) {
                            $scope.allSelectedActivity[key].DefaultImageURL = Wadado.rootPath + "/" + $scope.allSelectedActivity[key].ImageURL + "_portrait.jpg";
                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:900px)")) {
                            $scope.allSelectedActivity[key].DefaultImageURL = Wadado.rootPath + "/" + $scope.allSelectedActivity[key].ImageURL + "_landscape.jpg";
                        }
                    }
                });
            }
        };

        var setRating = function () {
            if (WURFL.is_mobile) {
                $.each($scope.allSelectedActivity, function (key, value) {
                    var count = 0;
                    $scope.allSelectedActivity[key].ratingURL = [];
                    var result = Math.round($scope.allSelectedActivity[key].Rating);
                    var half = false;
                    if (result > $scope.allSelectedActivity[key].Rating) {
                        result = result - 1;
                        half = true;
                    }
                    for (i = 0; i < result; i++) {
                        $scope.allSelectedActivity[key].ratingURL[count] = Wadado.rootPath + "/Images/Icons/full_star.png";
                        count++;
                    }
                    if (half) {
                        $scope.allSelectedActivity[key].ratingURL[count] = Wadado.rootPath + "/Images/Icons/half_star.png";
                        count++;
                    }
                    for (i = count; i < 5 ; i++) {
                        $scope.allSelectedActivity[key].ratingURL[count] = Wadado.rootPath + "/Images/Icons/line_star.png";
                        count++;
                    }
                });
            }
        };

        /*START SELECTED ACTIVITY TYPE*/
        $scope.allSelectedActivities = {};
        ActivityDataService.getSelectedActivityTypes().then(function (selectedActivities) {
            $scope.allSelectedActivity = selectedActivities;
            $scope.ActivityType = selectedActivities[0].ActivityCategory;
            /*SET LOCATION FOR ACTIVITY ONLY IF THE THE ACTIVITY EXISTS AND DATA IS RETURNED FROM THE SERVER CORRECTLY*/
            if ($.url().segment().length > 2) {
                if ($.url().segment(2).trim().length > 0 && $.url().segment(3).trim().length) {
                    $scope.selectedLocation = $.url().segment(2);

                }
            }
            setRating();
            setImages();
        });
        /*END SELECTED ACTIVITY TYPE*/

        $scope.loadActivityDetails = function (item) {
            $scope.ActivityKey = item.ActivityKey;
            ActivityDataService.getSelectedActivity($scope.ActivityKey);
        };

        $(window).resize(function () {
            setImages();
            $scope.$apply();
        });

        $scope.$on("LISTFILTERED", function (event, args) {
            $scope.allSelectedActivity = args.message;
            setRating();
            setImages();
        });

        $scope.$on("FILTERDATES", function (event, args) {
            $scope.dateRange = args.message;           
        });
    }
    app.controller("ActivityController", activityController);
}());
