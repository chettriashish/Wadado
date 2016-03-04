(function () {
    var app = angular.module("appMain");
    var adminActivityBookingDataService = function ($http, $q) {

        var getAllActivitiesPendingBooking = function () {
            return $http.get('AdminBookings/GetAllActivitiesPendingForConfirmation')
                .then(function (response) {
                return response.data;
            }).catch(function (response) {
                console.log(reponse);
            });
        }

        var getAllUpcomingActivities = function () {
            return $http.get('AdminBookings/GetAllUpcomingActivities')
                .then(function (response) {
                    return response.data;
                }).catch(function (response) {
                    console.log(reponse);
                });
        }

        var getAllActivitiesCompletedBooking = function () {
            return $http.get('AdminBookings/GetAllCompletedActivities')
                .then(function (response) {
                    return response.data;
                }).catch(function (response) {
                    console.log(reponse);
                });
        }

        var getAllRegisteredCompanies = function () {
            var deferred = $q.defer();
            $http.get("AdminBookings/GetAllRegisteredCompanies").success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var getAllActivitiesPendingForSelectedCompany = function (companyKey) {
            var deferred = $q.defer();
            $http({
                url: "AdminBookings/GetAllActivitiesPendingForSelectedCompany",
                params: { companyKey: companyKey },
                method:'GET'
                }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var getAllUpcomingCompanyActivities = function (companyKey) {
            var deferred = $q.defer();
            $http({
                url: "AdminBookings/getAllUpcomingCompanyActivities",
                params: { companyKey: companyKey },
                method: 'GET'
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        var getAllActivitiesCompletedForSelectedCompany = function (companyKey) {
            var deferred = $q.defer();
            $http({
                url: "AdminBookings/GetAllActivitiesCompletedForSelectedCompany",
                params: { companyKey: companyKey },
                method: 'GET'
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }

        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getAllActivitiesPendingBooking: getAllActivitiesPendingBooking,            
            getAllRegisteredCompanies: getAllRegisteredCompanies,
            getAllActivitiesPendingForSelectedCompany: getAllActivitiesPendingForSelectedCompany,
            getAllActivitiesCompletedForSelectedCompany: getAllActivitiesCompletedForSelectedCompany,
            getAllActivitiesCompletedBooking: getAllActivitiesCompletedBooking,
            getAllUpcomingCompanyActivities: getAllUpcomingCompanyActivities,
            getAllUpcomingActivities: getAllUpcomingActivities,
        }
    };
    app.factory("AdminActivityBookingDataService", adminActivityBookingDataService);
}());