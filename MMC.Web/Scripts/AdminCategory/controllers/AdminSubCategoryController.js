(function () {
    var app = angular.module("appMain");
    var adminSubCategoryController = function ($scope, $http, $timeout, $interval, $location, $state, allAvailableSubCategoryList,
        AdminCategoryDataService) {
        $scope.allSubCategoryDetailsList = [];
        if (typeof allAvailableSubCategoryList != 'undefined') {
            $scope.allSubCategoryDetailsList = allAvailableSubCategoryList;
            $.each($scope.allSubCategoryDetailsList, function (key, value) {
                $scope.allSubCategoryDetailsList[key].editMode = false;
            });

            $scope.editSelectedSubCategory = function (item) {
                item.editMode = true;
            }

            $scope.saveSelectedSubCategoryDetails = function (item) {
                if (item.ActivityTypeKey == ""
                    && item.ActivityType.trim() == "") {
                    var index = $scope.allSubCategoryDetailsList.indexOf(item);
                    if (index > -1) {
                        $scope.allSubCategoryDetailsList.splice(index, 1);
                        item.editMode = false;
                    }
                }
                else {
                    AdminCategoryDataService.saveActivitySubCategory(item).then(function (response) {
                        if (response == true) {
                            //Show success message
                            var message = {};
                            message.header = 'Confirmation';
                            message.body = 'activity subcategory has been saved';
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
            $scope.addNewSubCategory = function () {
                var newSubCategory = {
                    ActivityTypeKey: "",
                    ActivityType: "",
                    editMode: true
                };
                $scope.allSubCategoryDetailsList.push(newSubCategory);
            }
        }
    }
    app.controller("AdminSubCategoryController", adminSubCategoryController);
}());
