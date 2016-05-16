(function () {
    var app = angular.module("appMain");
    var homeDataService = function ($http, $q, $window) {

        var getHomeScreenDetails = function () {
            var deferred = $q.defer();
            $http.get('/Home').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var getTopOffers = function () {
            var deferred = $q.defer();
            $http.get('/Home/GetTopOffers').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var getTopTrendingActivities = function () {
            var deferred = $q.defer();
            $http.get('/Home/GetTopTrendingActivities').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var getLatestNews = function () {
            var deferred = $q.defer();
            $http.get('/Home/GetLatestNews').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var searchForLocation = function (locationName) {
            $window.location.href = "/Location/" + locationName;
        }

        var getActivitiesForSelectedSearchTag = function (tags) {
            var deferred = $q.defer();
            tags = tags.split(' ');
            $http({
                url: '/Home/GetActivitiesForSelectedSearchTag',
                method: 'GET',
                params: { tags: tags }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var getSelectedActivity = function (searchUrl) {
            $window.location.href = searchUrl;
        }

        var setSlider = function () {
            $('.owl-carousel').owlCarousel({
                loop: true,
                margin: 2,
                responsiveClass: true,
                autoplay: true,
                autoplayHoverPause: true,
                dots: true,
                responsiveBaseElement: window,
                responsiveRefreshRate: 50,
                animateOut: 'fadeOut',
                animateIn: 'flipIn',
                smartSpeed: 450,
                dotsEach: true,
                nav: true,
                responsive: {
                    0: {
                        items: 1,
                        nav: false,
                        loop: true,
                        margin: 0,
                        touchDrag: true
                    },
                    600: {
                        items: 2,
                        nav: false,
                        loop: true,
                        touchDrag: true,
                        margin: 4,

                    },
                    960: {
                        items: 3,
                        nav: true,
                        navText: ["<img src='../Images/Icons/prev.png' />", "<img  src='../Images/Icons/next.png' />"],
                        loop: true,
                        touchDrag: true,
                        margin: 5,
                        autoplayTimeout: 5000,

                    },
                    1200: {
                        items: 4,
                        nav: true,
                        navText: ["<img src='../Images/Icons/prev.png' />", "<img  src='../Images/Icons/next.png' />"],
                        loop: true,
                        touchDrag: false,
                        margin: 5
                    }
                }
            })
        }

        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getHomeScreendetails: getHomeScreenDetails,
            getTopOffers: getTopOffers,
            getTopTrendingActivities: getTopTrendingActivities,
            searchForLocation: searchForLocation,
            getLatestNews: getLatestNews,
            setSlider: setSlider,
            getActivitiesForSelectedSearchTag: getActivitiesForSelectedSearchTag,
            getSelectedActivity: getSelectedActivity,
        }
    };
    app.factory("HomeDataService", homeDataService);
}());