(function () {
    var app = angular.module("appMain");
    var setDates = function (date, obj, scope) {
        var datepicker = [];
        datepicker = $('.datepicker');
        if (datepicker.length > 1) {
            var newDate = $(obj).datepicker('getDate');                
            var id = $(obj).attr("date-val");
            for (i = 0; i < datepicker.length; i++) {
                if (id == 'init') {
                    newDate.setDate(newDate.getDate() + 1);
                    i++;
                    $(datepicker[i]).datepicker('option', 'minDate', newDate);
                    scope.setStartDate(date);
                    //scope.setEndDate($.datepicker.formatDate("dd/mm/yy", newDate));
                    break;
                }
                else if (id == 'end') {                    
                    $(datepicker[i]).datepicker('option', 'setDate', date);
                    scope.setEndDate($.datepicker.formatDate("dd/mm/yy", newDate));
                    break;
                }
            }
        }
        else {
            scope.dateSelected(date);
        }
    }
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
                        defaultDate:+2,
                        onSelect: function (date) {
                            setDates(date, this, scope);
                        },

                        beforeShowDay: function (day) {
                            var setday = day.getDay();
                            if (typeof scope.selectedActivityDetails !== "undefined") {
                                if ($.inArray(setday, scope.selectedActivityDetails.AllActivityDates) != -1) {
                                    return [true];
                                }
                                else {
                                    return [false];
                                }
                            }
                            else {
                                return [true];
                            }
                        }
                    });
                });
            }
        }
    });
}());
