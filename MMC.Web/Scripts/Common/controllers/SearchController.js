(function () {
    var app = angular.module("appMain");
    var searchController = function ($scope, $http, $timeout, $interval, $location, SearchDataService) {
        $scope.selectionInformation = 'SELECT LOCATION TO SEARCH';
        $scope.activityName = "Explore";

        /***************************LOADING SELECTED LOCATIONS ACTIVITY LIST *************************/
        $scope.loadSelectedLocationDetails = function (item, model) {
            if (!WURFL.is_mobile) {
                $scope.locationName = item.LocationName;
                onLocationActivityCategorySelected();
            }
            else {
                loadSelectedLocationActivities(item.LocationName);
            }
        };
        /*END LOADING SELECTED LOCATIONS ACTIVITY LIST*/

        /********************MAKING THE SECOND WORD BOLD************** */
        var makeBold = function () {
            $('.search-button a').each(function () {
                var words = $scope.selectionInformation.split('\t\t');
                $(this).empty().html(function () {
                    $scope.selectionInformation = "";
                    for (i = 0; i < words.length; i++) {
                        if (i == 1) {
                            $(this).append(' <span class="bold">' + words[i] + '\t\t</span>');
                        }
                        else {
                            $(this).append('<span>' + words[i] + '\t\t</span>');
                        }
                    }
                });
            });
        }
        if (purl().segment().length > 1) {
            if (purl().segment(2).trim().length > 0) {
                $scope.locationName = purl().segment(2);

            }
        }
        //this is for desktop
        var onLocationActivityCategorySelected = function () {
            if ($scope.locationName != null && $scope.locationName.trim() != "") {
                SearchDataService.getSelectedDetails($scope.activityCategory, $scope.locationName);
            }
        }
        //this is for mobile devices
        var onLocationActivitySelected = function () {
            if ($scope.locationName != null && $scope.locationName.trim() != "") {
                SearchDataService.getSelectedDetails($scope.activityName, $scope.locationName);
            }
            else {
                $scope.$broadcast("BUTTONCLICK", 'OPEN');
            };
        }
        /*LOADING THE SELECTED ACTIVITY OR SELECTED LOCATION DETAILS*/
        $scope.loadSelectedDetails = function () {
            onLocationActivitySelected();
        };
        /*LOADING THE SELECTED ACTIVITY OR SELECTED LOCATION DETAILS*/

        $scope.setSelectedActivityDetails = function (item, model) {
            $scope.activityName = item.Name;
            $scope.selectionInformation = 'VIEW SELECTED ACTIVITY DETAILS';
        };

        var loadSelectedLocationActivities = function (locationName) {
            $scope.locationName = locationName;
            SearchDataService.getAllActivitiesForLocation($scope.locationName).then(function (activities) {
                $scope.activities = activities;
                $scope.selectionInformation = "VIEW \t\t" + $scope.activities.length + "\t\t ACTIVITIES";
                makeBold();
                groupBy(activities, 'ActivityCategoryKey', 'Activity');
            });
        }
        /*LOADING ALL LOCATIONS*/
        SearchDataService.getAllLocations().then(function (alllocations) {
            $scope.alllocation = alllocations;
            if (purl().segment().length > 1) {
                if (purl().segment(2).trim().length > 0) {
                    var selectedLocation = purl().segment(2);
                    groupBy(alllocations, 'Country', 'location');
                    loadSelectedLocationActivities(selectedLocation);
                }
            }
        });


        //This is specifically for the desktop
        if (!WURFL.is_mobile) {
            $scope.loadSelectedActivityCategory = function (activityCategoryKey) {
                $scope.activityCategory = activityCategoryKey;
                onLocationActivityCategorySelected();
            }

            $scope.loadInLocationSelectedActivityCategory = function (activityCategoryKey) {
                $scope.$emit("ONCATEGORYCHANGED", activityCategoryKey);
            }
        }
        //end desktop specific code

        /*END LOADING ALL LOCATIONS*/

        /********************GROUPING LOCATIONS BY KEY***************************/
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
            else {
                $scope.locationResults = result;
                return $scope.locationResults;
            }
        };


        /*****************MAKE CODE COMMON******************/

        /*SHOWING AND HIDING SEARCH DIV*/

        $(".search-input").click(function () {
            preventScrolling();
            $(".mobile-search").toggleClass("open");
            $(".mobile-search-cover").toggleClass("open");
            $scope.$broadcast("BUTTONCLICK", 'OPEN');
            $scope.alllocations = {};
            $scope.locationName = null;
            $scope.activityName = null;
        });

        $(".menusearch-input").click(function () {
            preventScrolling();
            $(".mobile-search").toggleClass("open");
            $(".mobile-search-cover").toggleClass("open");
            $scope.$broadcast("BUTTONCLICK", 'OPEN');
            $scope.alllocations = {};
            $scope.locationName = null;
            $scope.activityName = null;
        });
        /*END SHOWING AND HIDING SEARCH DIV*/

        var preventScrolling = function () {
            $('.contentWrapper').bind('touchmove', function (e) { e.preventDefault() });
        }

        var allowScrolling = function () {
            $('.contentWrapper').unbind('touchmove');
        }

        /*****************END MAKE CODE COMMON******************/
        $(".close-button").click(function () {
            $(".mobile-search").toggleClass("open");
            $(".mobile-search-cover").toggleClass("open");
            $("body").toggleClass("no-scroll");
            $('.search-button a').empty();
            $scope.activities = [];
            $('.search-button a').append('<span>SELECT LOCATION TO SEARCH</span>')
            $scope.selectionInformation = 'SELECT LOCATION TO SEARCH';
            allowScrolling();
            $scope.$apply();
        });
    }
    app.controller("SearchController", searchController);
}());