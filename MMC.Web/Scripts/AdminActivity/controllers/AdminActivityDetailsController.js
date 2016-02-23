(function () {
    var app = angular.module("appMain");
    var adminActivityDetailsController = function ($scope, $http, $timeout, $interval, $location, $state, activityDetails, AdminActivityDataService) {
        $scope.activityDetails = activityDetails;        
    }
    app.controller("AdminActivityDetailsController", adminActivityDetailsController);
}());
