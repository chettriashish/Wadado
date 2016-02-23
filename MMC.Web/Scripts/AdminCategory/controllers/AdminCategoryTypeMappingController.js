(function () {
    var app = angular.module("appMain");
    var adminCategoryTypeMappingController = function ($scope, $http, $timeout, $interval, $location, $state, allAvailableCategoryList,
        AdminCategoryDataService) {

        if (typeof allAvailableCategoryList != 'undefined') {
            $scope.allCategoryDetailsList = [];
            $scope.allCategoryDetailsList = allAvailableCategoryList;
            $.each($scope.allCategoryDetailsList, function (key, value) {
                $scope.allCategoryDetailsList[key].editMode = false;
            });
        }

        var getAllSubCategories = function () {
            AdminCategoryDataService.getAllAvailableSubCategoriesAsync().then(function (response) {
                $scope.allSubCategoryDetailsList = [];
                $scope.allSubCategoryDetailsList = response;
                $.each($scope.allSubCategoryDetailsList, function (key, value) {
                    $scope.allSubCategoryDetailsList[key].selected = false;
                });
            });
        }

        getAllSubCategories();

        $scope.getAllSubCategoriesForSelectedItem = function (item) {
            $scope.selectedCategory = item.selected.ActivityCategoryKey;
            AdminCategoryDataService.getAllAvailableSubCategoriesForSelectedCategory(item).then(function (response) {
                $scope.allSelectedCategoryDetailsList = [];
                $scope.allSelectedSubCategory = [];
                $scope.allSelectedCategoryDetailsList = response;
                $scope.categorySelected = true;
                if ($scope.allSelectedCategoryDetailsList.length > 0) {
                    var counter = $scope.allSelectedCategoryDetailsList.length;
                    $.each($scope.allSubCategoryDetailsList, function (key1, value1) {
                        $scope.allSubCategoryDetailsList[key1].selected = false;
                        $.each($scope.allSelectedCategoryDetailsList, function (key, value) {
                            if ($scope.allSubCategoryDetailsList[key1].ActivityTypeKey == $scope.allSelectedCategoryDetailsList[key].ActivityTypeKey) {
                                $scope.allSubCategoryDetailsList[key1].selected = true;
                                $scope.allSelectedSubCategory.push($scope.allSubCategoryDetailsList[key1]);
                            }
                            counter--;
                            if (counter == 0) {
                                return false;
                            }
                        });
                    });
                }
                else {
                    $.each($scope.allSubCategoryDetailsList, function (key1, value1) {
                        $scope.allSubCategoryDetailsList[key1].selected = false;
                    });
                }
            });
        }
        $scope.allSelectedSubCategory = [];
        $scope.toggleSelectedItem = function (item) {
            if (!item.selected) {
                var index = $scope.allSelectedSubCategory.indexOf(item);
                if (index > -1) {
                    $scope.allSelectedSubCategory.splice(index, 1);
                }
            }
            else {
                $scope.allSelectedSubCategory.push(item);
            }
        }
        $scope.removeSelectedItem = function (item) {
            item.selected = false;
            if (!item.selected) {
                var index = $scope.allSelectedSubCategory.indexOf(item);
                if (index > -1) {
                    $scope.allSelectedSubCategory.splice(index, 1);
                }
                index = $scope.allSubCategoryDetailsList.indexOf(item);
                if (index > -1) {
                    $scope.allSubCategoryDetailsList[index].selected = false;
                }
            }
        }

        $scope.saveMapping = function () {
            AdminCategoryDataService.saveMapping($scope.allSelectedSubCategory, $scope.selectedCategory).then(function (response) {
                if(response == true){
                    //Show success message
                    var message = {};
                    message.header = 'Confirmation';
                    message.body = 'mapping has been saved';
                    message.showButtons = false;
                    message.isUserAction = false;
                    $scope.$emit("DIALOG_S", message);
                    setTimeout(function () {
                        $scope.$emit("DIALOG_H", message);
                    }, 1500);                    
                }
                else {

                }
            });
        }
    }
    app.controller("AdminCategoryTypeMappingController", adminCategoryTypeMappingController);
}());
