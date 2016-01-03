(function () {
    var app = angular.module("appMain");
    var dateFilterController = function ($scope, $http, $timeout, $interval, $location, DateFilterDataService) {

        $scope.filterActivitiesByDates = function () {
            if (typeof $scope.startDate == "undefined") {
                $scope.startDate = $.datepicker.formatDate("dd/mm/yy", new Date());
            }
            if (typeof $scope.endDate == "undefined") {
                var newDate = new Date();
                newDate.setDate(newDate.getDate() + 1);
                $scope.endDate = $.datepicker.formatDate("dd/mm/yy", newDate);
            }
            DateFilterDataService.filterDataByDateRangeSelected($scope.startDate, $scope.endDate).then(function (response) {               
                $scope.$emit("LISTFILTERED", { message: response });
                $(".mobile-date").toggleClass("open");
                var filterDates = $scope.startDate + " - " + $scope.endDate;
                $scope.$emit("FILTERDATES", { message: filterDates });
            });
        }
        $scope.setStartDate = function (date) {
            $scope.startDate = date;
        }
        $scope.setEndDate = function (date) {
            $scope.endDate = date;
        }

        $("#dateFilter").click(function () {
            $(".mobile-date").toggleClass("open");
        });

        $scope.closeDateFilter = function () {
            $(".mobile-date").toggleClass("open");
        }
    }
    app.controller("DateFilterController", dateFilterController);
}());