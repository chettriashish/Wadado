(function () {
    var app = angular.module("appMain");
    var favoritesController = function ($scope, $http, $timeout, $interval, $location, FavoritesDataService) {
        $scope.isFavoritesAdded = false;
        $scope.activity;
        $scope.checkUserLoginAndAddToFavorites = function (item) {
            var ActivityKey = item.Activity.ActivitesKey;
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
            setTimeout(function () {
                var width = $(".list-activity-wrapper img").width();
                var height = $(".list-activity-wrapper img").height();
                $(".like-container").css("width", width);
                $(".like-container div").css("top", (height - 25))
            }, 120);

        }

        $scope.init = function (item) {
            var ActivityKey = item.Activity.ActivitesKey;
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