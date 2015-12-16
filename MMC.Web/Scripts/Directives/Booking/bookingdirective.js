(function () {
    var app = angular.module("appMain");
    app.directive('bookingdirective', function ($compile, $http) {
        return {
            link: function (scope, element, attrs) {
                return {
                    restrict: "E",
                    templateUrl:"../Views/Partial/_booking.html"
                }
            }
        }
    })
}());
