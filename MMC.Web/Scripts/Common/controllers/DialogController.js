(function () {
    var app = angular.module("appMain");
    var dialogController = function ($scope) {
        $scope.dialog = {};
        $scope.$on("SHOWDIALOG", function (event, args) {           
            $scope.showDialog = true;
            $scope.dialog.header = args.header;
            $scope.dialog.body = args.body;
            $scope.dialog.showButtons = args.showButtons;
            $scope.dialog.isUserAction = args.isUserAction;
        });
        $scope.$on("HIDEDIALOG", function (event, args) {           
            $scope.showDialog = false;
            $scope.$apply();
        });
    }
    app.controller("DialogController", dialogController);
}());