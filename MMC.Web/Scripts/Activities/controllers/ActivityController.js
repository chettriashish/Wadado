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
                            $scope.allSelectedActivity[key].ImageURL = Wadado.rootPath +"/"+ $scope.allSelectedActivity[key].DefaultActivityImage.ImageURL + "_portrait.jpg";
                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:550px)")) {
                            $scope.allSelectedActivity[key].ImageURL = Wadado.rootPath + "/" + $scope.allSelectedActivity[key].DefaultActivityImage.ImageURL + "_landscape.jpg";
                        }
                    }
                    else {
                        if (window.styleMedia.matchMedium("screen and (max-width:800px)")) {
                            $scope.allSelectedActivity[key].ImageURL = Wadado.rootPath + "/" + $scope.allSelectedActivity[key].DefaultActivityImage.ImageURL + "_portrait.jpg";
                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:900px)")) {
                            $scope.allSelectedActivity[key].ImageURL = Wadado.rootPath + "/" + $scope.allSelectedActivity[key].DefaultActivityImage.ImageURL + "_landscape.jpg";
                        }
                    }
                });
            }
        };        
        /*START SELECTED ACTIVITY TYPE*/
        $scope.allSelectedActivities = {};
        ActivityDataService.getSelectedActivityTypes().then(function (selectedActivities) {
            $scope.allSelectedActivity = selectedActivities;
            $scope.ActivityType = selectedActivities[0].ActivityType;
            /*SET LOCATION FOR ACTIVITY ONLY IF THE THE ACTIVITY EXISTS AND DATA IS RETURNED FROM THE SERVER CORRECTLY*/
            if ($.url().segment().length > 2) {
                if ($.url().segment(2).trim().length > 0 && $.url().segment(3).trim().length) {
                    $scope.selectedLocation = $.url().segment(2);
                }
            }
            setImages();
        });
        /*END SELECTED ACTIVITY TYPE*/

        $scope.loadActivityDetails = function (item) {
            $scope.ActivityKey = item.Activity.ActivitesKey;
            ActivityDataService.getSelectedActivity($scope.ActivityKey);
        };
     
        $(window).resize(function () {
            setImages();
            $scope.$apply();
        });
    }
    app.controller("ActivityController", activityController);
}());
 