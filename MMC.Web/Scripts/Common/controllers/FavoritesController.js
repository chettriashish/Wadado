(function () {
    var app = angular.module("appMain");
    var favoritesController = function ($scope, $http, $timeout, $interval, $location, FavoritesDataService) {
        $scope.isFavoritesAdded = false;
        $scope.activity;
        $scope.favouritesClass = "";
        $scope.checkUserLoginAndAddToFavorites = function (item) {
            var ActivityKey = item.ActivityKey;
            if ($scope.isFavoritesAdded == true) {
                $scope.isFavoritesAdded = false;
                FavoritesDataService.removeFavorites(ActivityKey);
            }
            else {
                FavoritesDataService.checkIfUserLoggedIn().then(function (response) {
                    if (response == true) {
                        $scope.isFavoritesAdded = true;
                        FavoritesDataService.addFavorites(ActivityKey);
                    }
                    else {
                        FavoritesDataService.storeAction(ActivityKey).then(function (result) {
                            FavoritesDataService.loginUser();
                        });
                    }
                });
            };
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
            var ActivityKey = item.ActivityKey;
            FavoritesDataService.checkIfActivityInGuestFavorites(ActivityKey).then(function (result) {
                $scope.isFavoritesAdded = result;
                setFavorites();
            });
        }
        $(window).resize(function () {
            setFavorites();
        });
    }
    app.controller("FavoritesController", favoritesController);
}());