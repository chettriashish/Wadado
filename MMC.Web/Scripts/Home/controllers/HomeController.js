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
        var sliderInit = false;
        /*****************************TOP ACTIVITIES****************************************/
        var setImages = function () {
            if (WURFL.is_mobile) {
                $.each($scope.topActivity, function (key, value) {

                    if (WURFL.form_factor == "Smartphone") {
                        if (window.styleMedia.matchMedium("screen and (max-width:500px)")) {
                            $scope.topActivity[key].ImageURL = Wadado.rootPath + "/" + $scope.topActivity[key].DefaultActivityImage.ImageURL + "_portrait.jpg";
                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:550px)")) {
                            $scope.topActivity[key].ImageURL = Wadado.rootPath + "/" + $scope.topActivity[key].DefaultActivityImage.ImageURL + "_landscape.jpg";
                        }
                    }
                    else {
                        if (window.styleMedia.matchMedium("screen and (max-width:800px)")) {
                            $scope.topActivity[key].ImageURL = Wadado.rootPath + "/" + $scope.topActivity[key].DefaultActivityImage.ImageURL + "_portrait.jpg";

                        }
                        else if (window.styleMedia.matchMedium("screen and (min-width:900px)")) {
                            $scope.topActivity[key].ImageURL = Wadado.rootPath + "/" + $scope.topActivity[key].DefaultActivityImage.ImageURL + "_landscape.jpg";

                        }
                    }
                });
            }
            else {
                $.each($scope.topActivity, function (key, value) {
                    $scope.topActivity[key].ImageURL = Wadado.rootPath + "/" + $scope.topActivity[key].DefaultActivityImage.ImageURL + ".jpg";
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
        /*******************ALWAYS USE SINGLE SIZE IMAGE FOR THE SLIDER*****************/
        $scope.topOffers = {};
        var setCarasoulImages = function () {
            if (WURFL.is_mobile) {
                $.each($scope.topOffer, function (key, value) {

                    if (WURFL.form_factor == "Smartphone") {
                        $scope.topOffer[key].ImageURL = Wadado.rootPath + "/" + $scope.topOffer[key].Offer.ImageUrl + "_landscape.jpg";
                    }
                    else {
                        $scope.topOffer[key].ImageURL = Wadado.rootPath + "/" + $scope.topOffer[key].Offer.ImageUrl + "_landscape.jpg";
                    }
                });
            }
            else {
                $.each($scope.topOffer, function (key, value) {
                    $scope.topOffer[key].ImageURL = Wadado.rootPath + "/" + $scope.topOffer[key].Offer.ImageUrl + ".jpg";
                });
            }
            if (sliderInit == true) {
                $scope.$apply();
            }
            else if (sliderInit == false) {
                setTimeout(function () {
                    sliderInit = true;
                    HomeDataService.setSlider();
                }, 100);
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
            if (!WURFL.is_mobile) {
                setBackground();
            }
            $scope.$apply();
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

        /***********************SETTING TOP LEVEL CONTROLS********************************/
        var setBackground = function () {
            var windowHeight = $(window).height() * (0.95);
            //parseInt removes the 'px'
            var minHeight = parseInt($('.cover-image').css("min-height"), 10);
            var navHeight = $('.home-nav').height();
            if (windowHeight > minHeight) {
                $('.cover-image').height(windowHeight);
                $('.home-nav').css("top", (windowHeight - navHeight));
            }
            else {
                $('.home-nav').css("top", (minHeight - navHeight));
            }          
        }
        if (!WURFL.is_mobile) {
            setBackground();
            $scope.goTo = function (loc) {
                $('html,body').animate({
                    scrollTop: $("." + loc).offset().top
                },1000,"linear");
            };
        }
    }
    app.controller("HomeController", homeController);
}());