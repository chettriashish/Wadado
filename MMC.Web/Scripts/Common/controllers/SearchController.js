(function () {
    var app = angular.module("appMain");
    var searchController = function ($scope, $http, $timeout, $interval, $location, SearchDataService) {        
        $scope.selectionInformation = 'SELECT LOCATION TO SEARCH';
        /***************************LOADING SELECTED LOCATIONS ACTIVITY LIST *************************/
        $scope.loadSelectedLocationDetails = function (item, model) {
            $scope.locationName = item.LocationName;                               
            SearchDataService.getAllActivitiesForLocation($scope.locationName).then(function (activities) {
                $scope.activities = activities;
                $scope.selectionInformation = "VIEW \t\t" + $scope.activities.length + "\t\tACTIVITIES";  
            });
        };
        /*END LOADING SELECTED LOCATIONS ACTIVITY LIST*/

        /*LOADING THE SELECTED ACTIVITY OR SELECTED LOCATION DETAILS*/
        $scope.loadSelectedDetails = function () {
            if ($scope.locationName != null && $scope.locationName.trim() != "") {
                SearchDataService.getSelectedDetails($scope.activityName, $scope.locationName);
            }
            else {
                $scope.$broadcast("BUTTONCLICK", 'OPEN');
            }
        };
        /*LOADING THE SELECTED ACTIVITY OR SELECTED LOCATION DETAILS*/

        $scope.setSelectedActivityDetails = function (item, model) {
            $scope.activityName = item.Name;
            $scope.selectionInformation = 'VIEW SELECTED ACTIVITY DETAILS';
        };

        /*LOADING ALL LOCATIONS*/
        SearchDataService.getAllLocations().then(function (alllocations) {
            $scope.alllocation = alllocations;
        });
        /*END LOADING ALL LOCATIONS*/

        /*****************MAKE CODE COMMON******************/
        
        /*SHOWING AND HIDING SEARCH DIV*/

        $(".search-input").click(function () {
            $(".mobile-search").toggleClass("open");
            $(".mobile-search-cover").toggleClass("open");
            $scope.$broadcast("BUTTONCLICK", 'OPEN');
            $scope.alllocations = {};
            $scope.locationName = null;
            $scope.activityName = null;            
        });        
        
        $(".menusearch-input").click(function () {
            $(".mobile-search").toggleClass("open");
            $(".mobile-search-cover").toggleClass("open");
            $scope.$broadcast("BUTTONCLICK", 'OPEN');
            $scope.alllocations = {};
            $scope.locationName = null;
            $scope.activityName = null;            
        });
        /*END SHOWING AND HIDING SEARCH DIV*/
       
        /*****************END MAKE CODE COMMON******************/
        $(".close-button").click(function () {
            $(".mobile-search").toggleClass("open");
            $(".mobile-search-cover").toggleClass("open");            
            $scope.activities = [];
            $scope.selectionInformation = 'SELECT LOCATION TO SEARCH';
        });      
    }
    app.controller("SearchController", searchController);
}());