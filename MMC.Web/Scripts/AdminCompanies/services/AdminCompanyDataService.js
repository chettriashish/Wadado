(function () {

    var app = angular.module("appMain");

    var adminCompanyDataService = function ($http, $q) {
        var _isAdmin = false;
        var checkIfUserBelongsToCompany = function (userId) {
            return $http({
                url: 'AdminLogin/CheckIfUserBelongsToCompany',
                params: { userId: userId },
                method: 'GET'
            }).then(function (response) {
                return response.data;
            }).catch(function (response) {
                console.log(response);
            });
        }

        var checkIfUserBelongsToCompanyAsync = function (userId) {
            var deferred = $q.defer();
            $http({
                url: 'AdminLogin/CheckIfUserBelongsToCompany',
                params: { userId: userId },
                method: 'GET'
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;

        }

        var checkForAdminAsync = function (userId) {
            var deferred = $q.defer();
            $http({
                url: 'AdminLogin/CheckAdminUser',
                params: { userId: userId },
                method: 'GET'
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;

        }

        var setAdmin = function (isAdmin) {
            _isAdmin = isAdmin;
        }

        var getAdmin = function () {
            return _isAdmin;
        }
        var createCompanyForSelectedUser = function (userId, company) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                data: { userId: userId, company: company },
                url: "Adminlogin/CreateCompanyForSelectedUser"
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            checkIfUserBelongsToCompany: checkIfUserBelongsToCompany,
            createCompanyForSelectedUser: createCompanyForSelectedUser,
            checkIfUserBelongsToCompanyAsync: checkIfUserBelongsToCompanyAsync,
            checkForAdminAsync: checkForAdminAsync,
            getAdmin: getAdmin,
            setAdmin: setAdmin,
        }
    }

    app.factory("AdminCompanyDataService", adminCompanyDataService);

}());