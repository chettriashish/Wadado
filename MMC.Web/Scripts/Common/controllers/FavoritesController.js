(function () {
    var app = angular.module("appMain");
    var favoritesController = function ($scope, $http, $timeout, $interval, $location, FavoritesDataService) {
        $scope.isFavoritesAdded = false;
        $scope.activity;
        $scope.checkUserLoginAndAddToFavorites = function (item) {
            var ActivityKey = item.Activity.ActivitesKey;
            if ($scope.isFavoritesAdded == true) {
                FavoritesDataService.removeFavorites(ActivityKey).then(function (response) {
                    $scope.isFavoritesAdded = false;                    
                });
            }
            else {
                FavoritesDataService.addFavorites(ActivityKey).then(function (result) {
                    if (result == true) {
                        $scope.isFavoritesAdded = true;
                    }
                    else {
                        FavoritesDataService.storeAction(ActivityKey).then(function (result) {
                            FavoritesDataService.loginUser();
                        });
                    }
                });
            }
        }
        ///*ONCE APPLICATION PASSES THE ACTIVITY KEY TO BE ADDED TO FAVORITES*/
        //$scope.$on("FAVORITESADDED", function (event, args) {

        //});
        $scope.isItemAddedToFavourites = function () {
            return $scope.isFavoritesAdded;
        }
        $scope.showCart = function () {
            FavoritesDataService.showUserActivityCart();
        }

        var setFavorites = function () {

        }

        $scope.init = function (item) {
            var ActivityKey = item.Activity.ActivitesKey;
            FavoritesDataService.checkIfActivityInGuestFavorites(ActivityKey).then(function (result) {
                $scope.isFavoritesAdded = result;
            });
        }
    }
    app.controller("FavoritesController", favoritesController);
}());