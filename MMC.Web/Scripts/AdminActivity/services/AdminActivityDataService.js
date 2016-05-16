(function () {
    var app = angular.module("appMain");
    var adminActivityDataService = function ($http, $q) {

        var getAllAvailableLocations = function () {
            return $http.get('/AdminLocation/GetAllLocations').then(
                function (response) {
                    return response.data;
                })
            .catch(function (response) {
                console.log(response);
            });
        }

        var getAllAvailableLocationsAsync = function () {
            var deferred = $q.defer();
            $http.get('/AdminLocation/GetAllLocations').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var getAllActivitiesForSelectedLocation = function (item) {
            var deferred = $q.defer();
            $http({
                url: 'AdminActivity/GetAllActivitiesByLocation',
                method: 'GET',
                params: { locationKey: item.selected.LocationKey }
            }).success(deferred.resolve).error(deferred.reject)

            return deferred.promise;
        }

        var createNewActivity = function () {
            return $http.get('/AdminActivity/CreateNewActivity').then(function (response) {
                return response.data;
            }).catch(function (response) {
                console.log(response);
            });
        }

        var getSelectedActivityDetails = function (activityKey) {
            return $http({
                method: 'GET',
                url: 'AdminActivity/GetSelectedActivityDetails',
                params: { activityKey: activityKey }
            })
                .then(function (response) {
                    return response.data
                })
                .catch(function (response) {
                    console.log(response);
                });
        }

        var saveActivityDetails = function (activityDetails, activityDays, activityTimes, allActivityPricingOptions, activityCategoryKey, activityLocationKey) {
            var deferred = $q.defer();
            var time = [];
            activityDetails.AllPriceOptions = [];
            $.each(activityTimes, function (key, value) {
                time.push(activityTimes[key].time);
            });
            $.each(allActivityPricingOptions, function (key, value) {
                var priceOption = {};
                priceOption.OptionDescription = allActivityPricingOptions[key].OptionDescription;
                priceOption.PriceForAdults = allActivityPricingOptions[key].PriceForAdults;
                priceOption.PriceForChildren = allActivityPricingOptions[key].PriceForChildren;
                priceOption.IsDeleted = false;
                priceOption.ActivityKey = activityDetails.ActivityKey;
                priceOption.ActivityPricingKey = allActivityPricingOptions[key].ActivityPricingKey;
                priceOption.CreatedDate = new Date();
                priceOption.NumAdults = activityDetails.NumAdults;
                priceOption.NumChild = activityDetails.NumChildren;
                priceOption.CommissionPercentage = activityDetails.Comission;
                activityDetails.AllPriceOptions.push(priceOption);
            });
            $http({
                method: 'POST',
                url: '/AdminActivity/SaveActivityDetails',
                data: { activityDetails: activityDetails, activityDays: activityDays, activityTimes: time, activityCategoryKey: activityCategoryKey, activityLocationKey: activityLocationKey}
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var saveEventDetails = function (activityDetails, eventDateTime, allActivityPricingOptions, activityCategoryKey, activityLocationKey) {
            var deferred = $q.defer();
            activityDetails.AllActivityUniqueDates = [];
            activityDetails.AllPriceOptions = [];
            $.each(eventDateTime, function (key, value) {
                var event = {};
                event.Date = eventDateTime[key].Date;
                event.Time = eventDateTime[key].time;
                event.IsDeleted = false;
                event.ActivityKey = activityDetails.ActivityKey;
                event.ActivityDatesKey = '';
                activityDetails.AllActivityUniqueDates.push(event);
            });
            $.each(allActivityPricingOptions, function (key, value) {
                var priceOption = {};
                priceOption.OptionDescription = allActivityPricingOptions[key].OptionDescription;
                priceOption.PriceForAdults = allActivityPricingOptions[key].PriceForAdults;
                priceOption.PriceForChildren = allActivityPricingOptions[key].PriceForChildren;
                priceOption.IsDeleted = false;
                priceOption.ActivityKey = activityDetails.ActivityKey;
                priceOption.ActivityPricingKey = allActivityPricingOptions[key].ActivityPricingKey;
                priceOption.CreatedDate = new Date();                
                priceOption.NumAdults = activityDetails.NumAdults;
                priceOption.NumChild = activityDetails.NumChildren;
                priceOption.CommissionPercentage = activityDetails.Comission;
                activityDetails.AllPriceOptions.push(priceOption);
            });
            $http({
                method: 'POST',
                url: '/AdminActivity/SaveEventDetails',
                data: { activityDetails: activityDetails, activityCategoryKey: activityCategoryKey, activityLocationKey: activityLocationKey }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var uploadImages = function (activityKey, imagesToUpload) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                data: { activityKey: activityKey, activityImages: imagesToUpload },
                url: '/AdminActivity/UploadImages'
            }).success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        }

        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getAllAvailableLocations: getAllAvailableLocations,
            getAllActivitiesForSelectedLocation: getAllActivitiesForSelectedLocation,
            getSelectedActivityDetails: getSelectedActivityDetails,
            saveActivityDetails: saveActivityDetails,
            getAllAvailableLocationsAsync: getAllAvailableLocationsAsync,
            createNewActivity: createNewActivity,
            saveEventDetails: saveEventDetails,
            uploadImages: uploadImages,
        }
    };
    app.factory("AdminActivityDataService", adminActivityDataService);
}());