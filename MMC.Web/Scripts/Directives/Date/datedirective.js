(function () {
    var app = angular.module("appMain");
    app.directive('datepicker', function () {
        return {
            restrict: 'A', //restricting the directive to an attibute
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                $(function () {
                    element.datepicker({
                        dateFormat: 'dd/mm/yy',
                        minDate:0,
                        onSelect: function (date) {
                            scope.$apply(function () {
                                ngModelCtrl.$setViewValue(date);
                            });
                        }
                    });
                });
            }
        }
    });
}());
