(function () {
    var app = angular.module("appMain");
    var adminController = function ($scope) {
        
        $scope.$on("DIALOG_S", function (event, args) {
            $scope.$broadcast("SHOWDIALOG", args);
        });

        $scope.$on("DIALOG_H", function (event, args) {
            $scope.$broadcast("HIDEDIALOG", args);
        });      
    }
    app.controller("AdminController", adminController);
}());
