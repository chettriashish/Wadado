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

    app.filter("activityFilter", function () {
        return function (allActivities) {
            var result = [];
            if (typeof allActivities !== "undefined") {
                var result = $.grep(allActivities, function (e) { return e.selected == true });

            }
            else {
                allActivities;
            }
            return result;
        }
    });

    var activityController = function ($scope, $http, $timeout, $interval, $location, $routeParams, ActivityDataService) {
        $scope.datesSelected = false;
        $scope.categoryFilterSelected = false;
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
        $scope.activityTypes = [];
        $scope.all = {
            type: "",
            selected: true,
        };
        /****************************SETTING ACTIVITY FILTER WHEN NO FILTER IS SET********************************************/
        var setAllActivityTypes = function () {
            $.each($scope.allSelectedActivity, function (key, value) {
                if ($scope.activityTypes.length === 0 ||
                    $.grep($scope.activityTypes, function (e) { return e.type == $scope.allSelectedActivity[key].ActivityType }).length === 0) {
                    activity = {
                        type: "",
                        selected: false,
                    };
                    activity.type = $scope.allSelectedActivity[key].ActivityType;
                    activity.selected = true;                   
                    $scope.all.selected = true;
                    $scope.activityTypes.push(activity);
                }
                $scope.allSelectedActivity[key].selected = true;
            });
        }
        /****************************END SETTING ACTIVITY FILTER WHEN NO FILTER IS SET********************************************/

        /****************************CLEARING ACTIVITY FILTER********************************************/
        var clearAllActivityTypeFilters = function () {
            $.each($scope.allSelectedActivity, function (key, value) {
                if ($scope.activityTypes.length === 0 ||
                    $.grep($scope.activityTypes, function (e) { return e.type == $scope.allSelectedActivity[key].ActivityType }).length === 0) {
                    activity = {
                        type: "",
                        selected: false,
                    };
                    activity.type = $scope.allSelectedActivity[key].ActivityType;
                    activity.selected = true;
                    $scope.all.selected = true;
                    $scope.activityTypes.push(activity);
                }
                $scope.allSelectedActivity[key].selected = true;
                $scope.categoryFilterSelected = false;
            });
        }
        /****************************END CLEARING ACTIVITY FILTER********************************************/

        /****************************SETTING USER SELECTED ACTIVITY FILTER*********************************/
        $scope.dateRange = "search by date";
        var setUserSelectedFilter = function () {
            var checked = true;
            $.each($scope.activityTypes, function (key, value) {
                $.each($scope.allSelectedActivity, function (innerKey, innerValue) {
                    if ($scope.activityTypes[key].type == $scope.allSelectedActivity[innerKey].ActivityType) {
                        $scope.allSelectedActivity[innerKey].selected = $scope.activityTypes[key].selected;
                        if ($scope.activityTypes[key].selected === false) {
                            checked = false
                        }
                    }
                });
            });
            $scope.categoryFilterSelected = !checked;
            $scope.all.selected = checked;
            ActivityDataService.setActivityTypeFilter($scope.activityTypes);
        }
        /****************************END SETTING USER SELECTED ACTIVITY FILTER*********************************/

        /****************************GETTING USER SELECTED ACTIVITY FILTER *********************************/
        var getFilters = function () {
            setUserSelectedFilter();
            ActivityDataService.getAllFilters().then(function (response) {
                if (response.StartDate != null) {
                    $scope.$broadcast("DATEFILTERS", response);
                }                
                $scope.activityTypes = response.ActivityTypes;
                if ($scope.activityTypes === null || $scope.activityTypes.length === 0) {
                    $scope.activityTypes = [];
                    setAllActivityTypes();
                }
                else {
                    setUserSelectedFilter();
                }
            });
        }
        /****************************END GETTING USER SELECTED ACTIVITY FILTER*********************************/

        /*START SELECTED ACTIVITY TYPE*/
        $scope.allSelectedActivities = {};
        var getSelectedActivites = function () {           
            ActivityDataService.getSelectedActivityTypes().then(function (selectedActivities) {
                $scope.allSelectedActivity = selectedActivities;
                $scope.ActivityType = selectedActivities[0].ActivityCategory;
                /*SET LOCATION FOR ACTIVITY ONLY IF THE THE ACTIVITY EXISTS AND DATA IS RETURNED FROM THE SERVER CORRECTLY*/
                if ($.url().segment().length > 2) {
                    if ($.url().segment(2).trim().length > 0 && $.url().segment(3).trim().length) {
                        $scope.selectedLocation = $.url().segment(2);

                    }
                }
                getFilters();
                setRating();
                setImages();
            });
            /*END SELECTED ACTIVITY TYPE*/
        }

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
            getFilters();
            setRating();
            setImages();
        });

        $scope.$on("FILTERDATES", function (event, args) {
            $scope.dateRange = args.message;
            $scope.datesSelected = true;
        });
        /****************CLEAR DATE FILTER*******************/
        $scope.clearFilter = function () {           
            $scope.dateRange = "search by date";
            $scope.datesSelected = false;
            $scope.$broadcast("CLEARDATEFILTER", true);
            ActivityDataService.clearDateFilters().then(function (response) {
                getSelectedActivites();
            });            
        }
        /****************END CLEAR DATE FILTER*******************/
        /****************CLEAR ACTIVITY FILTER*******************/
        $scope.clearActivityTypeFilter = function () {
            ActivityDataService.clearActivityTypeFilters();
            $scope.activityTypes = [];
            clearAllActivityTypeFilters();
        }
        /****************END CLEAR ACTIVITY FILTER*******************/
        $scope.showActivityType = function () {
            if (!$(".mobile-activityFilter").hasClass("open")) {
                $(".mobile-activityFilter").addClass("open");
            }
        }

        $scope.toggleCheck = function () {
            $.each($scope.activityTypes, function (key, value) {
                $scope.activityTypes[key].selected = $scope.all.selected;
            });
        }
        $scope.toggleItem = function () {
            var checked = false;
            $.each($scope.activityTypes, function (key, value) {
                if ($scope.activityTypes[key].selected == true) {
                    checked = true;
                }
                else {
                    checked = false;
                }
            });
            $scope.all.selected = checked;
        }
        getSelectedActivites();
        /****************************SETTING USER SELECTED ACTIVITY FILTER *********************************/
        $scope.applyActivityFilter = function () {
            setUserSelectedFilter();
            $(".mobile-activityFilter").toggleClass("open");
        }
        /****************************END SETTING USER SELECTED ACTIVITY FILTER *********************************/

        $scope.closeFilter = function () {
            $(".mobile-activityFilter").removeClass("open");
        }
    }
    app.controller("ActivityController", activityController);
}());
