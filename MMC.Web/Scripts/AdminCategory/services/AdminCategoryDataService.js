(function () {
    var app = angular.module("appMain");
    var adminCategoryDataService = function ($http, $q, $window) {

        var getAllAvailableCategories = function () {
            return $http({
                url: '/AdminActivityCategory/GetAllCategories',
                method: 'GET'
            }).then(function (response) {
                return response.data;
            })
            .catch(function (reponse) {

            });
        }

        var getAllAvailableSubCategories = function () {
            return $http({
                url: '/AdminActivityCategory/GetAllActivitySubCategories',
                method: 'GET'
            }).then(function (response) {
                return response.data;
            })
            .catch(function (reponse) {

            });
        }

        var getAllAvailableSubCategoriesAsync = function () {
            var deferred = $q.defer();
            $http({
                url: '/AdminActivityCategory/GetAllActivitySubCategories',
                method: 'GET'
            }).success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        }

        var getAllAvailableSubCategoriesForSelectedCategory = function (item) {
            var deferred = $q.defer();
            $http({
                url: '/AdminActivityCategory/GetSubCategoriesForSelectedActivity',
                method: 'GET',
                params: { activityCategoryKey: item.selected.ActivityCategoryKey }
            }).success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        }

        var saveActivityCategory = function (activityCategory) {
            var deferred = $q.defer();
            $http({
                url: '/AdminActivityCategory/SaveActivityCategory',
                params: { activityCategoryKey: activityCategory.ActivityCategoryKey, activityCategory: activityCategory.ActivityCategory },
                method: 'GET'
            }).success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        }

        var saveActivitySubCategory = function (activitySubCategory) {
            var deferred = $q.defer();
            $http({
                url: '/AdminActivityCategory/SaveActivitySubCategory',
                method: 'GET',
                params: { activitySubCategoryKey: activitySubCategory.ActivityTypeKey, activitySubCategory: activitySubCategory.ActivityType }
            }).success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        }

        var saveMapping = function (activitySubCategories, activityCategory) {
            var selectedCategories = [];
            $.each(activitySubCategories, function (key, value) {
                selectedCategories.push(activitySubCategories[key].ActivityTypeKey);
            });
            var deferred = $q.defer();
            $http({
                url: '/AdminActivityCategory/SaveActivityCategoryMapping',
                method: 'POST',
                data: JSON.stringify({ activityTypes: selectedCategories, activityCategory: activityCategory })
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getAllAvailableCategories: getAllAvailableCategories,
            getAllAvailableSubCategories: getAllAvailableSubCategories,
            saveActivityCategory: saveActivityCategory,
            saveActivitySubCategory: saveActivitySubCategory,
            getAllAvailableSubCategoriesAsync: getAllAvailableSubCategoriesAsync,
            getAllAvailableSubCategoriesForSelectedCategory: getAllAvailableSubCategoriesForSelectedCategory,
            saveMapping: saveMapping,
        }
    };
    app.factory("AdminCategoryDataService", adminCategoryDataService);
}());