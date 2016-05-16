(function () {
    var app = angular.module("appMain");
    var adminLocationDetailsController = function ($scope, $http, $timeout, $interval, $location, $state, locationDetails, AdminLocationDataService) {
        $scope.locationDetails = locationDetails[0];
        $scope.imagesForLocation = [];
        if (typeof $scope.locationDetails != 'undefined'
            && $scope.locationDetails.ImageURL != null
            && typeof $scope.locationDetails.ImageURL != 'undefined') {
            if ($scope.locationDetails.ImageURL.indexOf("/") > -1) {
                var image = { result: "", name: $scope.locationDetails.ImageURL.split('/')[1] };
                $scope.imagesForLocation.push(image);
            }
            else {
                var image = { result: "", name: $scope.locationDetails.ImageURL };
                $scope.imagesForLocation.push(image);
            }
        }
        $scope.saveLocationDetails = function () {
            if ($scope.imagesForLocation.length > 0) {
                $scope.locationDetails.ImageURL = $scope.imagesForLocation[0].name;
            }
            AdminLocationDataService.saveLocationDetails($scope.locationDetails).then(function (response) {
                if (response == true) {
                    AdminLocationDataService.uploadImages($scope.imagesForLocation).then(function (response) {
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
                    });                    
                }
                else {
                    //Show friendly error message
                    console.log(response);
                }
            });
        }
        $scope.addImage = function () {
            $scope.$emit("DIALOG_IMAGE_S", "NEW");
        }
        $scope.$on("IMAGE_CROPPED_REDIRECT", function (event, args) {
            var image = { result: "", name: "" };
            image.result = args.result;
            image.name = args.name;
            $scope.imagesForLocation.push(image);
        });
        $scope.cancelChanges = function () {
            $state.go("adminLocation");
        }
    }
    app.controller("AdminLocationDetailsController", adminLocationDetailsController);
}());
