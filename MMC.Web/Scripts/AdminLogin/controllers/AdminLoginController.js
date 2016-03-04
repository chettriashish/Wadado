(function () {
    var app = angular.module("appMain");
    var adminLoginController = function (userAccount, isLoggedIn, $state, $scope) {
        vm = this;
        vm.isLoggedIn = userAccount.isUserloggedIn();
        if (vm.isLoggedIn) {
            $state.go("adminbookings_parent.adminActivityBookingsPending");
        }
        
        vm.message = '';
        vm.userData = {
            userName: '',
            email: '',
            password: '',
            confirmPassword: ''
        };
        vm.login = function () {
            vm.userData.grant_type = "password";
            vm.userData.userName = vm.userData.email;
            userAccount.login.loginUser(vm.userData,
                function (data) {
                    vm.isLoggedIn = true;
                    vm.message = "";
                    vm.password = "";
                    vm.token = data.access_token;
                    if (typeof (Storage) !== 'undefined') {
                        sessionStorage.token = vm.token;
                        $scope.$emit("u_l", true);
                    }
                    $state.go("adminbookings_parent.adminActivityBookingsPending");
                },
                function (response) {
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
            });
        };
        vm.goToLogin = function () {
            $state.go("adminLogin");
        }
        vm.registerNewUser = function () {
            $state.go("adminRegisterUser");
        }
    }
    app.controller("AdminLoginController", ["userAccount", "isLoggedIn", "$state", "$scope", adminLoginController]);
}());
