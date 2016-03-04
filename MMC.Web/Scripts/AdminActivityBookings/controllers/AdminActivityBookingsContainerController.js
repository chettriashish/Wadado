(function () {
    var app = angular.module("appMain");
    var adminActivityBookingsContainerController = function ($state, $scope) {
        $scope.loadContent = function (content) {
            if (content == 'pending') {
                $scope.isCompleted = false;
                $scope.isUpcoming = false;
                $scope.isPending = true;
                $state.go('adminbookings_parent.adminActivityBookingsPending');
            }
            else if (content == 'upcoming') {
                $scope.isPending = false;
                $scope.isCompleted = false;
                $scope.isUpcoming = true;
                $state.go('adminbookings_parent.adminUpcomingActivityBooking');
            }
            else if (content == 'completed') {
                $scope.isPending = false;
                $scope.isUpcoming = false;
                $scope.isCompleted = true;
                $state.go('adminbookings_parent.adminActivityBookingCompleted');
            }
            if ($('.active a')[1].id != content) {
                var current = $('.active a')[1].id;
                $('#' + current).parent().removeClass('active');
                $('#' + content).parent().addClass('active');
            }
        }

        var setDefaults = function () {
            $scope.isCompleted = false;
            $scope.isPending = true;
            $state.go('adminbookings_parent.adminActivityBookingsPending');
        }

        setDefaults();
    }
    app.controller("AdminActivityBookingsContainerController", adminActivityBookingsContainerController);
}());

