(function () {
    var app = angular.module("appMain");
    var adminLocationDetailsController = function ($scope, $http, $timeout, $interval, $location, $state, locationDetails, AdminLocationDataService) {
        $scope.locationDetails = locationDetails[0];

        $scope.saveLocationDetails = function () {
            AdminLocationDataService.saveLocationDetails($scope.locationDetails).then(function (response) {
                if (response == true) {
                    //Show success message
                    var message = {};
                    message.header = 'Message';
                    message.body = 'Location has been saved';
                    message.showButtons = false;
                    message.isUserAction = false;
                    $scope.$emit("DIALOG_S", message);
                    setTimeout(function () {
                        $scope.$emit("DIALOG_H", message);
                    }, 1500);
                }
                else {
                    //Show friendly error message
                    console.log(response);
                }
            });
        }

        $scope.cancelChanges = function () {
            $state.go("adminLocation");
        }
    }
    app.controller("AdminLocationDetailsController", adminLocationDetailsController);
}());
