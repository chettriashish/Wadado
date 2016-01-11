(function () {
    var app = angular.module("appMain");
    var activityDetailsDataService = function ($http, $q) {

        /****************FETCH SELECTED ACTIVITY DETAILS******************/
        var getSelectedActivityDetails = function () {
            var deferred = $q.defer();
            if ($.url().segment().length > 2) {
                if ($.url().segment(2).trim().length > 0 && $.url().segment(3).trim().length) {
                    var selectedLocation = $.url().segment(2);
                    var selectedActivityKey = $.url().segment(3);
                    $http({
                        url: '/ActivityDetails/GetSelectedActivityDetails',
                        method: 'GET',
                        params: { selectedLocation: selectedLocation, activityKey: selectedActivityKey }
                    }).success(deferred.resolve).error(deferred.reject);
                }
            }
            return deferred.promise;
        }
        /****************END FETCH SELECTED ACTIVITY DETAILS******************/

        /****************CHECK FOR ACTIVITY AVAILABILITY******************/
        var checkForSlotAvailability = function (date,time,numAdults,numChildren) {
            var deferred = $q.defer();            
            var selectedActivityKey = $.url().segment(3);
            $http({
                url: '/ActivityDetails/CheckForSlotAvailability',
                method: 'GET',
                params: { activityKey: activityKey, selectedDate: date,selectedTime:time,numberOfAdults:numAdults,numberOfChildren:numChildren }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        /****************END CHECK FOR ACTIVITY AVAILABILITY******************/

        /****************SIMILAR NEARBY ACTIVITIES******************/
        var setSimilarActivityImageSlider = function () {
            $('#similarActivityImages').owlCarousel({
                loop: true,
                margin: 1,
                responsiveClass: true,
                autoplay: true,
                autoplayHoverPause: true,
                responsiveBaseElement: window,
                responsiveRefreshRate: 50,
                dotsEach: true,
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

                    },
                    960: {
                        items: 3,
                        nav: false,
                        loop: true,
                        touchDrag: true

                    },
                    1200: {
                        items: 4,
                        nav: false,
                        loop: false,
                        touchDrag: false,
                        mouseDrag: true
                    }
                }
            })            
        }
        /****************END SIMILAR NEARBY ACTIVITIES******************/

        var setActivityImageSlider = function () {
            $('#activityImages').owlCarousel({
                loop: true,
                margin: 1,
                responsiveClass: true,
                autoplay: true,
                autoplayHoverPause: true,
                responsiveBaseElement: window,
                responsiveRefreshRate: 50,
                dotsEach: true,
                responsive: {
                    0: {
                        items: 1,
                        nav: false,
                        loop: true,
                        margin: 0,
                        touchDrag: true
                    },
                    600: {
                        items: 1,
                        nav: false,
                        loop: true,
                        touchDrag: true,

                    },
                    960: {
                        items: 1,
                        nav: false,
                        loop: true,
                        touchDrag: true

                    },
                    1200: {
                        items: 1,
                        nav: false,
                        loop: false,
                        touchDrag: false,
                        mouseDrag: true
                    }
                }
            })            
        }       

        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getSelectedActivityDetails: getSelectedActivityDetails,
            checkForSlotAvailability:checkForSlotAvailability,
            setSimilarActivityImageSlider: setSimilarActivityImageSlider,
            setActivityImageSlider: setActivityImageSlider,
        }
    };
    app.factory("ActivityDetailsDataService", activityDetailsDataService);
}());
