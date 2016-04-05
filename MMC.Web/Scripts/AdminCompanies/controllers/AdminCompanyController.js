(function () {
    var app = angular.module('appMain');

    var adminCompanyController = function (companyDetails, AdminCompanyDataService, $state) {
        vm = this;
        vm.company = companyDetails;
        vm.saveCompanyDetails = function () {
            AdminCompanyDataService.createCompanyForSelectedUser(sessionStorage.userId, vm.company).then(function (response) {
                vm.company = response;
                $state.go("adminbookings_parent.adminActivityBookingsPending");

            }).catch(function (response) {
                console.log(response)
            });
        }
    }

    app.controller("AdminCompanyController", ["companyDetails", "AdminCompanyDataService","$state", adminCompanyController]);
}());