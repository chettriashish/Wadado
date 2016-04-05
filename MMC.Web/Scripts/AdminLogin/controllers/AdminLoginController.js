(function () {
    var app = angular.module("appMain");
    var adminLoginController = function (userAccount, isLoggedIn, $state, $scope, AdminCompanyDataService) {
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

        var setToken = function (){
           
        }

        vm.login = function () {
            vm.userData.grant_type = "password";
            vm.userData.userName = vm.userData.email;
            userAccount.login.loginUser(vm.userData,
                function (data) {
                    vm.isLoggedIn = true;
                    vm.message = "";
                    vm.password = "";
                    vm.token = data.access_token;
                    vm.userId = data.userId;
                    if (typeof (Storage) !== 'undefined') {
                        sessionStorage.token = vm.token;
                        sessionStorage.userId = vm.userData.email;                       
                    }
                    AdminCompanyDataService.checkIfUserBelongsToCompanyAsync(data.userName).then(function (response) {
                        vm.company = response;
                        if (vm.company.CompanyKey) {
                            $state.go("adminbookings_parent.adminActivityBookingsPending");
                            $scope.$emit("u_l", true);
                        }
                        else {
                            $state.go("adminCompanyDetails");
                        }
                    });                    
                    
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
    app.controller("AdminLoginController", ["userAccount", "isLoggedIn", "$state", "$scope","AdminCompanyDataService", adminLoginController]);
}());
