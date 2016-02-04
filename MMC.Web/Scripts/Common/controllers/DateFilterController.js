(function () {
    var app = angular.module("appMain");
    var dateFilterController = function ($scope, $http, $timeout, $interval, $location, DateFilterDataService) {
        $scope.selectedStartDate = '';
        $scope.selectedEndDate = '';
        $scope.onStartDate = true;
        $scope.onEndDate = false;
        var notify = function () {
            var filterDates = $scope.selectedStartDate + " - " + $scope.selectedEndDate;
            resetDefaults();
            $scope.$emit("FILTERDATES", { message: filterDates });
        }
        var resetDefaults = function () {
            $(".mobile-date").removeClass("open");
            $scope.onStartDate = true;
            $scope.onEndDate = false;
            if ($("#to").hasClass("open")) {
                $("#to").removeClass("open");
                $("#from").addClass("open");
            }
            allowScrolling();
        }
        $scope.filterActivitiesByDates = function () {
            if ($scope.selectedStartDate == '') {
                $scope.selectedStartDate = $.datepicker.formatDate("dd/mm/yy", new Date());
            }
            if ($scope.selectedEndDate == '') {
                var newDate = new Date();
                newDate.setDate(newDate.getDate() + 1);
                $scope.selectedEndDate = $.datepicker.formatDate("dd/mm/yy", newDate);
            }
            DateFilterDataService.filterDataByDateRangeSelected($scope.selectedStartDate, $scope.selectedEndDate).then(function (response) {
                $scope.$emit("LISTFILTERED", { message: response });
                $(".mobile-date").toggleClass("open");
                notify();
            });
        }
        $scope.onStartDateSet = function (date) {
            $scope.selectedStartDate = date;
            $scope.onEndDate = true;
        }
        $scope.onEndDateSet = function (date) {
            $scope.selectedEndDate = date;
        }
        $scope.showStartDate = function () {
            $scope.onStartDate = true;
            $("#to").removeClass("open");
            $("#from").addClass("open");
        }
        $scope.hideStartDate = function () {
            $scope.onStartDate = false;
            $("#from").removeClass("open");
            $("#to").addClass("open");
        }
        $("#dateFilter").click(function () {
            $(".mobile-date").toggleClass("open");
            preventScrolling()
        });
        $("#dateRange").click(function () {
            $(".mobile-date").toggleClass("open");
            preventScrolling()
        });
        $scope.closeDateFilter = function () {
            resetDefaults();
        }

        var preventScrolling = function () {
            $('body').bind('touchmove', function (e) { e.preventDefault() });
        }

        var allowScrolling = function () {
            $('body').unbind('touchmove');
        }

        $scope.$on("CLEARDATEFILTER", function (event, args) {
            if (args === true) {
                $scope.selectedStartDate = '';
                $scope.selectedEndDate = '';
                resetDefaults();
            }
        });       
    }
    app.controller("DateFilterController", dateFilterController);
}());