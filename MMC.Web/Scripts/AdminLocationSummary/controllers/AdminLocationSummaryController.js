(function () {
    var app = angular.module("appMain");
    var adminLocationSummaryController = function ($scope, $http, $timeout, $interval, $location, $state, allAvailableLocations, AdminLocationSummaryDataService) {
        $scope.locations = allAvailableLocations;

        $scope.editSelectedLocation = function (item) {
            $state.go("adminLocationEdit", { id: item.LocationKey });
        }

        $scope.deleteSelectedLocation = function (item) {

        }

        $scope.createNewLocation = function () {
            $state.go("adminLocationCreate");
        }
    }
    app.controller("AdminLocationSummaryController", adminLocationSummaryController);
}());
