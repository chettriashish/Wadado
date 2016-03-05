(function () {
    var app = angular.module("appMain");
    var adminController = function ($scope, $state, userAccount, $stateParams) {
        $scope.isUserLoggedIn = false;
        $scope.$on("DIALOG_S", function (event, args) {
            $scope.$broadcast("SHOWDIALOG", args);
        });

        $scope.$on("DIALOG_H", function (event, args) {
            $scope.$broadcast("HIDEDIALOG", args);
        });
        $scope.$on("u_l", function (event, args) {
            $scope.isUserLoggedIn = userAccount.isUserloggedIn();            
        });
        if (userAccount.isUserloggedIn() == false) {
            $state.go("adminLogin");
        }
        $scope.logout = function () {
            userAccount.logout();
            $scope.isUserLoggedIn = userAccount.isUserloggedIn();
            $state.go("adminLogin");
        }
        var setActive = function (content) {
            if ($('.active a')[0] != null && $('.active a')[0].id != content) {
                var current = $('.active a')[0].id;
                $('#' + current).parent().removeClass('active');
                $('#' + content).parent().addClass('active');
            }
        }
        var setDefaultUrl = function () {
            var current = typeof $('.active a')[0] != 'undefined' ? $('.active a')[0].id : '';            
            selectedItem = window.location.href.split('/')[4];
            if (current != selectedItem) {
                $('#' + current).parent().removeClass('active');
                $('#' + selectedItem).parent().addClass('active');
            }                        
        }
        window.onload = function () {
            setDefaultUrl();
        }
        $scope.setActive = function (content) {
            setActive(content);
        }
        $scope.isUserLoggedIn = userAccount.isUserloggedIn();
    }
    app.controller("AdminController", ["$scope", "$state", "userAccount", "$stateParams", adminController]);
}());
