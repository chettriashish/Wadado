(function () {
    var app = angular.module("appMain");
    var adminActivityBookingController = function (allActivities, AdminActivityBookingDataService) {
        var vm = this;
        vm.allActivities = allActivities;
        vm.company = {};       
        var formatDate = function () {
            $.each(vm.allActivities, function (key, value) {
                //Setting Dates Correctly
                var regex = /[0-9]+/;
                var date = vm.allActivities[key].BookingDate;
                var result = Number(date.match(regex)[0]);
                vm.allActivities[key].f_BookingDate = new Date(result).toDateString();                        
            });
        }               

        AdminActivityBookingDataService.getAllRegisteredCompanies().then(function (response) {
            vm.companies = response;
            $.each(vm.companies, function (key, value) {
                if (vm.companies[key].CompanyKey == 'All') {
                    vm.company.selected = vm.companies[key];
                }
            });
        });       

        vm.getAllActivitiesPendingForSelectedCompany = function (item) {
            AdminActivityBookingDataService.getAllActivitiesPendingForSelectedCompany(vm.company.selected.CompanyKey).then(function (response) {
                vm.allActivities = response;
                formatDate();
            });
        }
        vm.getAllActivitiesCompletedForSelectedCompany = function (item) {
            AdminActivityBookingDataService.getAllActivitiesCompletedForSelectedCompany(vm.company.selected.CompanyKey).then(function (response) {
                vm.allActivities = response;
                formatDate();
            });
        }
        formatDate();
    }
    app.controller("AdminActivityBookingController", ["allActivities", "AdminActivityBookingDataService", adminActivityBookingController]);
}());
