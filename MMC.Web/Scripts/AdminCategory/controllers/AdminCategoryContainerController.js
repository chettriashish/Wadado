(function () {
    var app = angular.module("appMain");
    var adminCategoryContainerController = function ($scope, $http, $timeout, $interval, $location, $state) {
        $scope.loadContent = function (content) {            
            if (content == 'main-category') {
                $scope.isSubCategory = false;
                $scope.isSubCategoryMapping = false;
                $scope.isCategory = true;
                $state.go('admincategory_parent.admincategories');
            }
            else if (content == 'subcategory') {
                $scope.isCategory = false;
                $scope.isSubCategoryMapping = false;
                $scope.isSubCategory = true;
                $state.go('admincategory_parent.adminsubcategories');
            }
            else {
                $scope.isSubCategory = false;
                $scope.isCategory = false;
                $scope.isSubCategoryMapping = true;
                $state.go('admincategory_parent.admincategorytypemapping');
            }

            if ($('.active a')[1].id != content) {
                var current = $('.active a')[1].id;
                $('#' + current).parent().removeClass('active');
                $('#' + content).parent().addClass('active');
            }
            //if ($('.active a')[1].id != content) {
            //    var current = $('.active a')[1].id;
            //    $('#' + current).parent().removeClass('active');
            //    $('#' + content).parent().addClass('active');                
            //}
        }

        var setDefaults = function () {
            $scope.isSubCategory = false;
            $scope.isSubCategoryMapping = false;
            $scope.isCategory = true;            
            $state.go('admincategory_parent.admincategories');            
        }

        setDefaults();
    }
    app.controller("AdminCategoryContainerController", adminCategoryContainerController);
}());

