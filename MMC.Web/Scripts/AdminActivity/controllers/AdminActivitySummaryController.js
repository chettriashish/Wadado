(function () {
    var app = angular.module("appMain");
    var adminActivitySummaryController = function ($scope, $http, $timeout, $interval, $location, $state, allAvailableLocations, AdminActivityDataService) {
        $scope.locations = allAvailableLocations;

        $scope.editSelectedActivity = function (item) {
            $state.go("adminActivityEdit", { id: item.ActivityKey, location: item.Location });
        }

        $scope.getAllActivitiesForSelectedLocation = function (item) {
            $scope.allActivities = [];
            AdminActivityDataService.getAllActivitiesForSelectedLocation(item).then(function (response) {
                $scope.allActivities = response;
            });
        }

        $scope.deleteSelectedActivity = function (item) {

        }

        $scope.createNewLocation = function () {
            $state.go("adminLocationCreate");
        }
    }
    app.controller("AdminActivitySummaryController", adminActivitySummaryController);
}());
