(function () {
    var app = angular.module("appMain");
    var activitiesCartDataService = function ($http, $q, $window) {       

        var getUserActivityCart = function () {
            var deferred = $q.defer();
            $http.get('/ActivitiesCart/GetUsersActivityCart').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getUserActivityCart: getUserActivityCart,            
        }
    };
    app.factory("ActivitiesCartDataService", activitiesCartDataService);
}());