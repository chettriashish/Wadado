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

        var setLayout = function () {
            if (window.styleMedia.matchMedium("screen and (max-width:900px)")) {
                $scope.largeScreen = false;
            }
            else {
                $scope.largeScreen = true;
            }
        }
        setLayout();
        var sliderInit = false;
        /*****************************TOP ACTIVITIES****************************************/
        var setImages = function () {
            if (WURFL.is_mobile) {
                $.each($scope.topActivity, function (key, value) {

                    if (WURFL.form_factor == "Smartphone") {
                        $scope.topActivity[key].ImageURL = Wadado.rootPath + "/" + $scope.topActivity[key].ImageURL + "_landscape.jpg";                      
                    }
                    else {
                        $scope.topActivity[key].ImageURL = Wadado.rootPath + "/" + $scope.topActivity[key].ImageURL + "_landscape.jpg";                        
                    }
                });
            }
            else {
                $.each($scope.topActivity, function (key, value) {
                    $scope.topActivity[key].ImageURL = Wadado.rootPath + "/" + $scope.topActivity[key].ImageURL + ".jpg";
                });
            }
        };

        $scope.topActivities = {};

        HomeDataService.getTopTrendingActivities().then(function (topActivities) {
            $scope.topActivity = topActivities;
            setImages();
            setRatings($scope.topActivity);
        });


        /*END TOP ACTIVITIES*/

        /*****************************TOP OFFERS****************************************/
        /*******************ALWAYS USE SINGLE SIZE IMAGE FOR THE SLIDER*****************/
        $scope.topOffers = {};
        var setCarasoulImages = function () {
            if (WURFL.is_mobile) {
                $.each($scope.topOffer, function (key, value) {
                    if (WURFL.form_factor == "Smartphone") {
                        $scope.topOffer[key].ImageURL = Wadado.rootPath + "/" + $scope.topOffer[key].ImageURL + "_landscape.jpg";
                    }
                    else {
                        $scope.topOffer[key].ImageURL = Wadado.rootPath + "/" + $scope.topOffer[key].ImageURL + "_landscape.jpg";
                    }
                });
            }
            else {
                $.each($scope.topOffer, function (key, value) {
                    $scope.topOffer[key].ImageURL = Wadado.rootPath + "/" + $scope.topOffer[key].ImageURL + ".jpg";
                });
            }
            if (sliderInit == true) {
                $scope.$apply();
            }
            else if (sliderInit == false) {
                setTimeout(function () {
                    sliderInit = true;
                    HomeDataService.setSlider();
                }, 1);
            }
        };

        HomeDataService.getTopOffers().then(function (topOffer) {
            $scope.topOffer = topOffer;
            setCarasoulImages();
            setRatings($scope.topOffer);
        });
        /*END TOP OFFERS*/

        /*****************************LATEST NEWS****************************************/
        HomeDataService.getLatestNews().then(function (latestNews) {
            $scope.latestNews = latestNews;
        });
        /*END LATEST NEWS*/

        $(window).resize(function () {
            //setImages();
            //setCarasoulImages();
            if (!WURFL.is_mobile) {
                setBackground();
            }
            setLayout();
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
            var windowHeight = $(window).height() * (0.85);
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
            setLayout();
        }
        var setRatings = function (allActivities) {
            //if (WURFL.is_mobile)
            {
                $.each(allActivities, function (key, value) {
                    var count = 0;
                    allActivities[key].ratingURL = [];
                    var result = Math.round(allActivities[key].Rating);
                    var half = false;
                    if (result > allActivities[key].Rating) {
                        result = result - 1;
                        half = true;
                    }
                    for (i = 0; i < result; i++) {
                        //if (window.styleMedia.matchMedium("screen and (max-width:550px)")) {
                        //    $scope.allSelectedActivity[key].ratingURL[count] = Wadado.rootPath + "/Images/Icons/full_star_gold.png";
                        //}
                        allActivities[key].ratingURL[count] = Wadado.rootPath + "/Images/Icons/full_star_white.png";
                        count++;
                    }
                    if (half) {
                        //if (window.styleMedia.matchMedium("screen and (max-width:550px)")) {
                        //    $scope.allSelectedActivity[key].ratingURL[count] = Wadado.rootPath + "/Images/Icons/half_star_gold.png";
                        //}
                        allActivities[key].ratingURL[count] = Wadado.rootPath + "/Images/Icons/half_star_white.png";
                        count++;
                    }
                    for (i = count; i < 5 ; i++) {
                        //if (window.styleMedia.matchMedium("screen and (max-width:550px)")) {
                        //    $scope.allSelectedActivity[key].ratingURL[count] = Wadado.rootPath + "/Images/Icons/line_star_gold.png";
                        //}
                        allActivities[key].ratingURL[count] = Wadado.rootPath + "/Images/Icons/line_star_white.png";
                        count++;
                    }
                });
            }
        };
    }
    app.controller("HomeController", homeController);
}());