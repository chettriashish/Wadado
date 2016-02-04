(function () {
    var app = angular.module("appMain");
    var favoritesDirective = function () {

        return {
            scope: {
                entityKey: "=",
                type:"@"
            },
            controller: function ($scope, FavoritesDataService) {
                $scope.isFavoritesAdded = false;
                $scope.activity;
                $scope.favouritesClass = "";
                $scope.checkUserLoginAndAddToFavorites = function () {                    
                    if ($scope.isFavoritesAdded == true) {
                        $scope.isFavoritesAdded = false;
                        FavoritesDataService.removeFavorites($scope.entityKey);
                    }
                    else {
                        FavoritesDataService.checkIfUserLoggedIn().then(function (response) {
                            if (response == true) {
                                $scope.isFavoritesAdded = true;
                                FavoritesDataService.addFavorites($scope.entityKey);
                            }
                            else {
                                FavoritesDataService.storeAction($scope.entityKey).then(function (result) {
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

                $scope.init = function () {                    
                    FavoritesDataService.checkIfActivityInGuestFavorites($scope.entityKey).then(function (result) {
                        $scope.isFavoritesAdded = result;
                        setFavorites();
                    });
                }
                $(window).resize(function () {
                    setFavorites();
                });
            },
            templateUrl:"../../Templates/_favourites.html"
        }
    }



    app.directive("ndsFavorites", favoritesDirective);
}());