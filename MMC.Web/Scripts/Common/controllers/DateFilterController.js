(function () {
    var app = angular.module("appMain");
    var dateFilterController = function ($scope, $http, $timeout, $interval, $location, DateFilterDataService) {
        $scope.message = "select start date";
        $scope.startDate = null;
        $scope.endDate = null;
        var notify = function () {
            var filterDates = $scope.startDate + " - " + $scope.endDate;
            if (!($(".filter-button").hasClass("ready"))) {
                $scope.message = "apply filter";
                $(".filter-button").addClass("ready");
            }
            $scope.$emit("FILTERDATES", { message: filterDates });
        }
        $scope.filterActivitiesByDates = function () {
            if (($(".filter-button").hasClass("ready"))) {
                if ($scope.startDate == null) {
                    $scope.startDate = $.datepicker.formatDate("dd/mm/yy", new Date());
                }
                if ($scope.endDate == null) {
                    var newDate = new Date();
                    newDate.setDate(newDate.getDate() + 1);
                    $scope.endDate = $.datepicker.formatDate("dd/mm/yy", newDate);
                }
                DateFilterDataService.filterDataByDateRangeSelected($scope.startDate, $scope.endDate).then(function (response) {
                    $scope.$emit("LISTFILTERED", { message: response });
                    $(".mobile-date").toggleClass("open");
                    notify();
                });
            }
        }
        $scope.setStartDate = function (date) {
            $scope.startDate = date;
            if ($scope.endDate === null) {
                $scope.message = "select end date";
                $scope.$apply();
            }
        }
        $scope.setEndDate = function (date) {
            $scope.endDate = date;
            if ($scope.startDate === null) {
                $scope.message = "select start date";
                $scope.$apply();
            }
            if ($scope.startDate !== null && $scope.endDate !== null) {
                $scope.message = "apply filter";
                if (!($(".filter-button").hasClass("ready"))) {
                    $(".filter-button").addClass("ready");
                }
                $scope.$apply();
            }
        }

        $("#dateFilter").click(function () {
            $(".mobile-date").toggleClass("open");            
        });
        $("#dateRange").click(function () {
            $(".mobile-date").toggleClass("open");            
        });
        $scope.closeDateFilter = function () {
            $(".mobile-date").removeClass("open");
        }

        $scope.$on("CLEARDATEFILTER", function (event, args) {
            if (args === true) {
                $scope.startDate = null;
                $scope.endDate = null;
                $scope.message = "select start date";
                if (($(".filter-button").hasClass("ready"))) {
                    $(".filter-button").removeClass("ready");
                }
            }
        });

        var setDateFilter = function (response) {
            if ($scope.startDate == null) {
                $scope.startDate = response.StartDate;
                $scope.endDate = response.EndDate;
                var datepicker = [];
                datepicker = $('.datepicker');
                if (datepicker.length > 1) {
                    for (i = 0; i < datepicker.length; i++) {
                        var id = $(datepicker[i]).attr("date-val");
                        if (id == 'init') {
                            $(datepicker[i]).datepicker('setDate', $scope.startDate);
                        }
                        else if (id == 'end') {
                            $(datepicker[i]).datepicker('setDate', $scope.endDate);
                        }
                    }
                }
            }            
            if ($scope.startDate != null) {
                notify();
            }         
        }

        $scope.$on("DATEFILTERS", function (event, args) {
            setDateFilter(args);
        });
    }
    app.controller("DateFilterController", dateFilterController);
}());