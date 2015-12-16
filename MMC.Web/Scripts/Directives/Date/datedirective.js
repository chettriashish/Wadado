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
                        minDate: 0,
                        maxDate: 180,
                        onSelect: function (date) {
                            scope.dateSelected(date);
                        },
                        beforeShowDay: function (day) {
                            var setday = day.getDay();
                            if ($.inArray(setday, scope.selectedActivityDetails.AllActivityDates) != -1) {
                                return [true];
                            }
                            else {
                                return [false];
                            }
                        }
                    });
                });
            }
        }
    });
}());
