(function () {

    var userAccount = function ($resource, appSettings, $window, $http, $q) {

        var redirectToRegister = function () {
            $window.location.href = "Register";
        }

        var redirectToProfessional = function () {
            $window.location.href = Wadado.rootPath + Wadado.professional;
        }

        var login = function (userName, password) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/Admin/Login',
                data: { userName: userName, password: password }
            }).success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        }

        return {
            registration:
                $resource(appSettings.serverPath + "/api/account", null,
                {
                    'registerUser': { method: 'POST' },
                }),
            login: login,
            //login: $resource(appSettings.serverPath + "/api/account", null,
            //       {
            //           'loginUser': {
            //               method: 'POST',
            //               headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            //               transformRequest: function (data, headersGetter) {
            //                   var str = [];
            //                   for (var d in data)
            //                       str.push(encodeURIComponent(d) + "=" +
            //                                           encodeURIComponent(data[d]));
            //                   return str.join("&");
            //               }
            //           }
            //       }),
            isUserloggedIn: function () {
                if (typeof (Storage) !== 'undefined' && sessionStorage.IsLoggedIn
                    && sessionStorage.userId) {
                    return true;
                }
                else if (typeof sessionStorage.IsLoggedIn == 'undefined') {
                    return false;
                }
                else {
                    //GetSession from server for IE 6/7
                }
            },
            logout: function () {
                sessionStorage.removeItem("IsLoggedIn");
                sessionStorage.removeItem("userId");
                sessionStorage.removeItem("companyKey");
                var deferred = $q.defer();
                $http({
                    method: 'POST',
                    url: '/Admin/Logout',                    
                }).then(function(data){
                    $window.location.href = Wadado.rootPath + Wadado.professional;
                });
            },
            redirectToRegister: redirectToRegister,
            redirectToProfessional: redirectToProfessional,
        };

    }
    angular.module("common.services")
    .factory("userAccount", ["$resource", "appSettings", "$window", "$http", "$q", userAccount]);
}());