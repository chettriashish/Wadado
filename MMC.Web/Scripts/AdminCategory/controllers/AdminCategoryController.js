(function () {
    var app = angular.module("appMain");
    var adminCategoryController = function ($scope, $http, $timeout, $interval, $location, $state, allAvailableCategoryList,
        AdminCategoryDataService) {
        $scope.allCategoryDetailsList = [];      
        if (typeof allAvailableCategoryList != 'undefined') {
            $scope.allCategoryDetailsList = allAvailableCategoryList;
            $.each($scope.allCategoryDetailsList, function (key, value) {
                $scope.allCategoryDetailsList[key].editMode = false;
            });
            $scope.editSelectedCategory = function (item) {
                item.editMode = true;
            }

            $scope.saveSelectedCategoryDetails = function (item) {
                if (item.ActivityCategoryKey == ""
                    && item.ActivityCategory.trim() == "") {
                    var index = $scope.allCategoryDetailsList.indexOf(item);
                    if (index > -1) {
                        $scope.allCategoryDetailsList.splice(index, 1);
                        item.editMode = false;
                    }
                }
                else {
                    AdminCategoryDataService.saveActivityCategory(item).then(function (response) {
                        if (response == true) {
                            //Show success message
                            var message = {};
                            message.header = 'Confirmation';
                            message.body = 'activity category has been saved';
                            message.showButtons = false;
                            message.isUserAction = false;
                            $scope.$emit("DIALOG_S", message);
                            setTimeout(function () {
                                $scope.$emit("DIALOG_H", message);
                            }, 1500);
                            item.editMode = false;
                        }
                    });
                }                
            }
            $scope.addNewCategory = function () {
                var newCategory = {
                    ActivityCategoryKey: "",
                    ActivityCategory: "",
                    editMode: true
                };
                $scope.allCategoryDetailsList.push(newCategory);
            }
        }       
    }
    app.controller("AdminCategoryController", adminCategoryController);
}());
