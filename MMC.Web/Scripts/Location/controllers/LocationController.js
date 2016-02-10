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
        var sliderInit = false;
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
                        $scope.location.ImageURL = Wadado.rootPath + "/" + $scope.location.ImageURL + "_portrait.jpg";
                    }
                    else if (window.styleMedia.matchMedium("screen and (min-width:480px)")) {
                        $scope.location.ImageURL = Wadado.rootPath + "/" + $scope.location.ImageURL + "_landscape.jpg";
                    }
                }
                else {
                    if (window.styleMedia.matchMedium("screen and (max-width:800px)")) {
                        $scope.location.ImageURL = Wadado.rootPath + "/" + $scope.location.ImageURL + "_portrait.jpg";
                    }
                    else if (window.styleMedia.matchMedium("screen and (min-width:900px)")) {
                        $scope.location.ImageURL = Wadado.rootPath + "/" + $scope.location.ImageURL + "_landscape.jpg";
                    }
                }
            }
            else {
                $scope.location.ImageURL = Wadado.rootPath + "/" + $scope.location.ImageURL + ".jpg";
            }
        };

        /*****************************TOP OFFERS****************************************/
        /*******************ALWAYS USE SINGLE SIZE IMAGE FOR THE SLIDER*****************/
        $scope.topOffers = {};
        var setCarasoulImages = function () {
            if (!WURFL.is_mobile) {
                $.each($scope.location.TopOffersForLocation, function (key, value) {
                    $scope.location.TopOffersForLocation[key].ImageURL = Wadado.rootPath + "/" + $scope.location.TopOffersForLocation[key].ImageURL + ".jpg";
                });
            }
            if (sliderInit == true) {
                $scope.$apply();
            }
            else if (sliderInit == false) {
                setTimeout(function () {
                    if (!WURFL.is_mobile) {
                        sliderInit = true;
                        LocationDataService.setSlider();
                    }
                }, 100);
            }
        };

        $scope.locations = {};
        LocationDataService.getSelectedLocationDetails().then(function (selectedLocation) {
            $scope.location = selectedLocation;
            /*SET LOCATION FOR ACTIVITY ONLY IF THE THE ACTIVITY EXISTS AND DATA IS RETURNED FROM THE SERVER CORRECTLY*/
            if (purl().segment().length >= 2) {
                if (purl().segment(2).trim().length > 0) {
                    $scope.selectedLocation = $scope.location.LocationName;
                }
            }
            $scope.Root = Wadado.rootPath;
            setImages();
            if (!WURFL.is_mobile) {
                setCarasoulImages();
                setRatings($scope.location.TopOffersForLocation);
            }
            $scope.$broadcast("LOCATIONSET", $scope.location.DefaultActivityCategoryKey);
            $scope.aboutLocation = "About " + $scope.location.LocationName;
            /********************UNDERLINING THE FIRST WORD */
            var underline = $('.header-left').each(function () {
                var words = $scope.aboutLocation.split(' ');
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
        });

        var setRatings = function (allActivities) {
            {
                $.each(allActivities, function (key, value) {
                    var count = 0;
                    allActivities[key].ratingURL = [];
                    var result = Math.round(allActivities[key].Rating);
                    var half = false;
                    if (result > allActivities[key].Rating) {
                        result = result - 1;
                        half = true;
                    }
                    for (i = 0; i < result; i++) {
                        allActivities[key].ratingURL[count] = Wadado.rootPath + "/Images/Icons/full_star_white.png";
                        count++;
                    }
                    if (half) {
                        allActivities[key].ratingURL[count] = Wadado.rootPath + "/Images/Icons/half_star_white.png";
                        count++;
                    }
                    for (i = count; i < 5 ; i++) {
                        allActivities[key].ratingURL[count] = Wadado.rootPath + "/Images/Icons/line_star_white.png";
                        count++;
                    }
                });
            }
        };

        $(window).resize(function () {
            if (!WURFL.is_mobile) {
                setLayout();
                $scope.$apply();
            }
        });

        /***************************LOADING SELECTED LOCATIONS ACTIVITY TYPE *************************/
        $scope.getAllSelectedActivities = function (item) {
            $scope.locationName = $scope.location.LocationName;
            $scope.ActivityType = item.ActivityName;
            LocationDataService.getActivitiesForType($scope.ActivityType, $scope.locationName);
        };
        /*END LOADING SELECTED LOCATION ACTIVITY TYPE*/        
    }
    app.controller("LocationController", locationController);
}());