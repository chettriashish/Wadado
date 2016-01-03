(function () {
    var app = angular.module("appMain");
    var dateFilterController = function ($scope, $http, $timeout, $interval, $location, DateFilterDataService) {

        $scope.filterActivitiesByDates = function () {
            if (typeof $scope.startDate != "undefined" &&
                typeof $scope.endDate != "undefined") {
                DateFilterDataService.filterDataByDateRangeSelected($scope.startDate, $scope.endDate).then(function (response) {
                    $(".mobile-date").toggleClass("open");
                });
            }
            else if (typeof $scope.endDate == "undefined") {

            }
        }
        $scope.setStartDate = function (date) {
            $scope.startDate = date;
        }
        $scope.setEndDate = function (date) {
            $scope.endDate = date;
        }

        $scope.showDateFilters = function () {
            $(".mobile-date").toggleClass("open");
        }
    }
    app.controller("DateFilterController", dateFilterController);
}());