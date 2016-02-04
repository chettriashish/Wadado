(function () {
    var app = angular.module("appMain");
    var locationDataService = function ($http, $q, $window) {

        var getSelectedLocationDetails = function () {
            var selectedLocation = null;
            var deferred = $q.defer();
            if (purl().segment().length > 1) {
                if (purl().segment(2).trim().length > 0) {
                    var selectedLocation = purl().segment(2);
                    $http({
                        url: '/Location/GetSelectedLocation',
                        method: 'GET',
                        params: { selectedLocation: selectedLocation }
                    }).success(deferred.resolve).error(deferred.reject);
                }
            }
            return deferred.promise;
        }

        var getAllAvailableLocations = function () {
            var deferred = $q.defer();
            $http.get('/Location/GetAllOtherLocations').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var getActivitiesForType = function (activityType, location) {
            $window.location.href = "/Activities/" + location + "/" + activityType;
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
            getSelectedLocationDetails: getSelectedLocationDetails,            
            getActivitiesForType: getActivitiesForType,
            setSlider: setSlider,
        }
    };
    app.factory("LocationDataService", locationDataService);
}());