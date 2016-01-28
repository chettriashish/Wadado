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

    var locationController = function ($scope, $http, $timeout, $interval, $location, LocationDataService) {
        var setLayout = function () {
            if (window.styleMedia.matchMedium("screen and (max-width:900px)")) {
                $scope.largeScreen = false;
            }
            else {
                $scope.largeScreen = true;
            }
        }
        if (!WURFL.is_mobile) {
            setLayout();
        }
        var setImages = function () {
            if (WURFL.is_mobile) {
                if (WURFL.form_factor == "Smartphone") {
                    if (window.styleMedia.matchMedium("screen and (max-width:479px)")) {
                        $scope.location.SelectedLocation.LocationImage = Wadado.rootPath + "/" + $scope.location.DefaultLocationImageURL + "_portrait.jpg";
                    }
                    else if (window.styleMedia.matchMedium("screen and (min-width:480px)")) {
                        $scope.location.SelectedLocation.LocationImage = Wadado.rootPath + "/" + $scope.location.DefaultLocationImageURL + "_landscape.jpg";
                    }
                }
                else {
                    if (window.styleMedia.matchMedium("screen and (max-width:800px)")) {
                        $scope.location.SelectedLocation.LocationImage = Wadado.rootPath + "/" + $scope.location.DefaultLocationImageURL + "_portrait.jpg";
                    }
                    else if (window.styleMedia.matchMedium("screen and (min-width:900px)")) {
                        $scope.location.SelectedLocation.LocationImage = Wadado.rootPath + "/" + $scope.location.DefaultLocationImageURL + "_landscape.jpg";
                    }
                }
            }
            else {
                $scope.location.SelectedLocation.LocationImage = Wadado.rootPath + "/" + $scope.location.DefaultLocationImageURL + ".jpg";
            }
        };

        $scope.locations = {};
        LocationDataService.getSelectedLocationDetails().then(function (selectedLocation) {
            $scope.location = selectedLocation;
             /*SET LOCATION FOR ACTIVITY ONLY IF THE THE ACTIVITY EXISTS AND DATA IS RETURNED FROM THE SERVER CORRECTLY*/
            if ($.url().segment().length >= 2) {
                if ($.url().segment(2).trim().length > 0 ) {
                    $scope.selectedLocation = $.url().segment(2);
                }
            }
            $scope.Root = Wadado.rootPath;
            setImages();
        });        

        $(window).resize(function () {
            setImages();            
            if (!WURFL.is_mobile) {
                setLayout();
            }
            $scope.$apply();
        });

        /***************************LOADING SELECTED LOCATIONS ACTIVITY TYPE *************************/
        $scope.getAllSelectedActivities = function (item) {
            $scope.locationName = $scope.location.SelectedLocation.LocationName;
            $scope.ActivityType = item.ActivityName;
            LocationDataService.getActivitiesForType($scope.ActivityType, $scope.locationName);
        };
        /*END LOADING SELECTED LOCATION ACTIVITY TYPE*/
    }
    app.controller("LocationController", locationController);
}());