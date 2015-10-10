
appMainModule.controller("AccountLoginViewModel", function ($scope, $http, viewModelHelper, validator) {
    
    $scope.viewModelHelper = viewModelHelper;
    $scope.accountModel = new MMC.AccountLoginModel();
    $scope.returnUrl = '';


});