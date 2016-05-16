(function () {
    var app = angular.module("appMain");
    var adminLoginController = function (userAccount, $state, $scope, AdminCompanyDataService) {
        vm = this;
        vm.isLoggedIn = userAccount.isUserloggedIn();
        if (vm.isLoggedIn) {
            AdminCompanyDataService.checkIfUserBelongsToCompanyAsync(sessionStorage.userId).then(function (response) {
                vm.company = response;
                if (vm.company.CompanyKey) {
                    $state.go("adminbookings_parent.adminActivityBookingsPending");
                }
                else {
                    $state.go("adminCompanyDetails");
                }
            });
        }

        vm.message = '';
        vm.userData = {
            userName: '',
            email: '',
            password: '',
            confirmPassword: ''
        };

        var setToken = function () {

        }

        vm.login = function () {           
            vm.userData.userName = vm.userData.email;
            userAccount.login(vm.userData.email, vm.userData.password).then(function (data) {
                vm.isLoggedIn = true;
                vm.message = "";
                vm.password = "";                                
                if (typeof (Storage) !== 'undefined') {                    
                    sessionStorage.userId = data.Email;
                    sessionStorage.IsLoggedIn = data.IsLoggedIn;
                }
                userAccount.redirectToProfessional();
            }).catch(function (response) {
                vm.password = "";
                vm.message = response.statusText + "\r\n";
                if (response.data.exceptionMessage)
                    vm.message += response.data.exceptionMessage;

                if (response.data.error) {
                    vm.message += response.data.error;
                }
            });
        }
        vm.registerUser = function () {
            userAccount.registration.registerUser(vm.userData, function (data) {
                vm.login();
            }, function (response) {
                console.log(response);
            });
        };
        vm.goToLogin = function () {
            $state.go("adminLogin");
        }
        vm.registerNewUser = function () {
            userAccount.redirectToRegister();
            //$state.go("adminRegisterUser");
        }
    }
    app.controller("AdminLoginController", ["userAccount", "$state", "$scope", "AdminCompanyDataService", adminLoginController]);
}());
