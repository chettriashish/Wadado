(function () {
    'use strict';
    var app = angular.module("appMain");
    var adminController = function ($scope, $state, userAccount, $stateParams, AdminCompanyDataService) {
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
        $scope.$on("DIALOG_IMAGE_S", function (event, args) {
            $scope.$broadcast("SHOWIMAGEDIALOG", args);
        });

        $scope.$on("IMAGE_CROPPED", function (event, args) {
            $scope.$broadcast("IMAGE_CROPPED_REDIRECT", args);
        });

        AdminCompanyDataService.checkIfUserBelongsToCompanyAsync(sessionStorage.userId).then(function (response) {
            if (response.CompanyKey) {
                $scope.isUserLoggedIn = userAccount.isUserloggedIn();
                sessionStorage.companyKey = response.CompanyKey;
                AdminCompanyDataService.checkForAdminAsync(sessionStorage.userId).then(function (response) {
                    AdminCompanyDataService.setAdmin(response);
                    $scope.isAdmin = AdminCompanyDataService.getAdmin();
                    //$state.go("adminbookings_parent.adminActivityBookingsPending");
                });                
            }
            else {
                $scope.isUserLoggedIn = false;
                $state.go("adminCompanyDetails");
            }
        });
        $scope.logout = function () {
            userAccount.logout();
            $scope.isUserLoggedIn = userAccount.isUserloggedIn();        
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
            var selectedItem = window.location.href.split('/')[4];
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
    }
    app.controller("AdminController", ["$scope", "$state", "userAccount", "$stateParams", "AdminCompanyDataService", adminController]);
}());
