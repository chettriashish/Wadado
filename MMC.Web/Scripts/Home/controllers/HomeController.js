(function () {
    var app = angular.module("appMain");
    app.filter('propsFilter', function () {
        return function (items, props) {
            var out = [];

            if (angular.isArray(items)) {
                var keys = Object.keys(props);

                items.forEach(function (item) {
                    var itemMatches = false;

                    for (var i = 0; i < keys.length; i++) {
                        var prop = keys[i];
                        var text = props[prop].toLowerCase();
                        if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                            itemMatches = true;
                            break;
                        }
                    }

                    if (itemMatches) {
                        out.push(item);
                    }
                });
            } else {
                // Let the output be the input untouched
                out = items;
            }

            return out;
        };
    });

    var homeController = function ($scope, $http, $timeout, $interval, $location, HomeDataService) {
        $scope.selectedLocation = 'What to do?  Where to go? ';
        $scope.counter = 0;        
        /*****************************TOP ACTIVITIES****************************************/        
        var setImages = function () {
            if (WURFL.is_mobile) {
                $.each($scope.topActivity, function (key, value) {

                    if (WURFL.form_factor == "Smartphone") {
                        if (window.styleMedia.matchMedium("screen and (max-width:500px)")) {
                            $scope.topActivity[key].ImageURL = $scope.topActivity[key].DefaultActivityImage.ImageURL + "_potrait.jpg";
                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:550px)")) {
                            $scope.topActivity[key].ImageURL = $scope.topActivity[key].DefaultActivityImage.ImageURL + "_landscape.jpg";
                        }
                    }
                    else {
                        if (window.styleMedia.matchMedium("screen and (max-width:800px)")) {
                            $scope.topActivity[key].ImageURL = $scope.topActivity[key].DefaultActivityImage.ImageURL + "_potrait.jpg";
                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:900px)")) {
                            $scope.topActivity[key].ImageURL = $scope.topActivity[key].DefaultActivityImage.ImageURL + "_landscape.jpg";
                        }
                    }
                });
            }
        };       

        $scope.topActivities = {};

        HomeDataService.getTopTrendingActivities().then(function (topActivities) {
            $scope.topActivity = topActivities;
            setImages();
        });
        /*END TOP ACTIVITIES*/

        /*****************************TOP OFFERS****************************************/
        $scope.topOffers = {};
        var setCarasoulImages = function () {
            if (WURFL.is_mobile) {
                $.each($scope.topOffer, function (key, value) {

                    if (WURFL.form_factor == "Smartphone") {
                        if (window.styleMedia.matchMedium("screen and (max-width:479px)")) {
                            $scope.topOffer[key].ImageURL = $scope.topOffer[key].Offer.ImageUrl + "_potrait.jpg";
                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:480px)")) {
                            $scope.topOffer[key].ImageURL = $scope.topOffer[key].Offer.ImageUrl + "_landscape.jpg";
                        }
                    }
                    else {
                        if (window.styleMedia.matchMedium("screen and (max-width:800px)")) {
                            $scope.topOffer[key].ImageURL = $scope.topOffer[key].Offer.ImageUrl + "_potrait.jpg";
                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:900px)")) {
                            $scope.topOffer[key].ImageURL = $scope.topOffer[key].Offer.ImageUrl + "_landscape.jpg";
                        }
                    }
                });
            }          
        };

        HomeDataService.getTopOffers().then(function (topOffer) {
            $scope.topOffer = topOffer;
            setCarasoulImages();
        });
        /*END TOP OFFERS*/

        /*****************************LATEST NEWS****************************************/
        HomeDataService.getLatestNews().then(function (latestNews) {
            $scope.latestNews = latestNews;
        });
        /*END LATEST NEWS*/        

        $(window).resize(function () {
            setImages();
            setCarasoulImages();
            $scope.$apply();
        });
        
        $(window).load(function () {
            setTimeout(function () {
                HomeDataService.setSlider();
            }, 500);
        });
        $scope.singleDemo = {};
        $scope.singleDemo.color = '';

        $scope.appendToBodyDemo = {
            remainingToggleTime: 0,
            present: true,
            startToggleTimer: function () {
                var scope = $scope.appendToBodyDemo;
                var promise = $interval(function () {
                    if (scope.remainingTime < 1000) {
                        $interval.cancel(promise);
                        scope.present = !scope.present;
                        scope.remainingTime = 0;
                    } else {
                        scope.remainingTime -= 1000;
                    }
                }, 1000);
                scope.remainingTime = 3000;
            }
        };

        $scope.addLocations = function (item, model) {
            if (item.hasOwnProperty('isTag')) {
                delete item.isTag;
                $scope.locations.push(item);
            }
        }
    }
    app.controller("HomeController", homeController);
}());