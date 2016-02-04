///<reference path="../../Scripts/angular.js" />
///<reference path="../../Scripts/angular.animate" />
///<reference path="../../Scripts/angular-ui/ui-bootstrap-tpls-js" />
///<reference path="../../Scripts/angular-ui/ui-bootstrap.js" />

(function ($parse) {
    var app = angular.module("appMain");
    var dateDirective = function ($parse) {
        return {
            restrict: 'E',//restricting the directive to an element
            templateUrl:"../../Templates/dateTemplate.html",
            link: function (scope, el, attrs) {
                scope.$watch('minStartDate', function (newValue, oldValue) {
                    if (newValue != oldValue) {
                        scope.getCurrentDates();
                    }
                });
                scope.$watch('dateSelected', function (newValue, oldValue) {
                    if (newValue !== oldValue
                        && newValue == '') {
                        scope.clearSelectedDate();
                    }
                });
            },
            scope: {
                dateformat: "@dateFormat",
                numberofMonths: "=monthsToDisplay",
                disableDays: "=disableDays",
                disableDates: "=disableDates",
                multiDates: "@allowMultiDate",
                showCurrent: "@allowCurrentDate",
                numOfDaysFromStart: "@",
                dateSelected: "=",
                minStartDate: "=",               
            },
            controller: function ($scope, $parse) {
                var monthSet = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                var daySet = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
                var resultSet = [];
                $scope.oVal = null;
                $scope.selectedDates = [];
                //will go upto the num(months) * num(days) and be zero indexed
                var dataSetKeyCounter = 0;

                var daysInMonth = function (month, year) {
                    month++;
                    var date = new Date(year, month, 0);
                    return date.getDate();
                }

                var createHeader = function (year, month, startDay, key) {
                    var date = new Date(year, month, startDay);
                    var preMonth = date.getMonth();
                    var year = date.getFullYear();
                    maxDays = new Date(year, preMonth, 0).getDate();
                    var minDate = getMinDates();
                    preMonth--;
                    for (n = (startDay - 1) ; n >= 0; n--) {
                        var dataSet = {
                            year: "",
                            key: "",
                            calendarDate: {
                                date: "",
                                longMonth: "",
                                shortMonth: "",
                                day: "",
                                longDay: "",
                                css: ""
                            }
                        };
                        dataSet.key = key;
                        dataSet.year = (preMonth == -1 ? year - 1 : year);
                        dataSet.calendarDate.longMonth = monthSet[preMonth == -1 ? 11 : preMonth];
                        dataSet.calendarDate.shortMonth = preMonth == -1 ? 11 : preMonth;
                        dataSet.calendarDate.date = (maxDays - n);
                        dataSet.calendarDate.day = new Date((preMonth == -1 ? year - 1 : year), (preMonth == -1 ? 11 : preMonth), (maxDays - n)).getDay();
                        dataSet.calendarDate.longDay = daySet[dataSet.calendarDate.day];
                        if (new Date() > new Date(dataSet.year, dataSet.calendarDate.shortMonth, dataSet.calendarDate.date)) {
                            if (isInSelectedDate(dataSet)) {
                                clearSelectedDate();
                            }
                            dataSet.calendarDate.css = "past-month";
                        }
                        else if (new Date(dataSet.year, dataSet.calendarDate.shortMonth, dataSet.calendarDate.date) < minDate) {

                            dataSet.calendarDate.css = "past-month";
                        }
                        else {
                            dataSet.calendarDate.css = disableDates(dataSet, "other-month");
                        }
                        if (isInSelectedDate(dataSet) && !(new Date(dataSet.year, dataSet.calendarDate.shortMonth, dataSet.calendarDate.date) < date)) {
                            dataSet.calendarDate.css = "other-month active";
                        }
                        resultSet.push(dataSet);
                        dataSetKeyCounter++;
                    }
                }

                var createTrailer = function (year, month, startDay, key) {
                    var date = new Date(year, month, 0);
                    var nextMonth = date.getMonth();
                    var year = date.getFullYear();
                    var daysRemaining = 6 - startDay;
                    nextMonth++;
                    for (k = 1; k <= daysRemaining; k++) {
                        var dataSet = {
                            year: "",
                            key: "",
                            calendarDate: {
                                date: "",
                                longMonth: "",
                                shortMonth: "",
                                day: "",
                                longDay: "",
                                css: ""
                            }
                        };
                        dataSet.key = key;
                        dataSet.year = nextMonth == 12 ? year + 1 : year;
                        dataSet.calendarDate.longMonth = monthSet[nextMonth == 12 ? 0 : nextMonth];
                        dataSet.calendarDate.shortMonth = nextMonth == -1 ? 11 : nextMonth;
                        dataSet.calendarDate.date = k;
                        dataSet.calendarDate.day = new Date((nextMonth == 12 ? year + 1 : year), (nextMonth == 12 ? 0 : nextMonth), k).getDay();
                        dataSet.calendarDate.longDay = daySet[dataSet.calendarDate.day];
                        if ((new Date().getMonth()) != month || (new Date().getFullYear()) != dataSet.year) {
                            var currentDate = new Date(dataSet.year, month, dataSet.calendarDate.date);
                            var minDate = getMinDates();
                            var maxDate = new Date((minDate.getFullYear()), (minDate.getMonth() + ($scope.numberofMonths - 1)), minDate.getDate());
                            if (currentDate > maxDate) {
                                if (isInSelectedDate(dataSet)) {
                                    clearSelectedDate();
                                }
                                dataSet.calendarDate.css = "out-of-range";
                            }
                            else {
                                dataSet.calendarDate.css = disableDates(dataSet, "other-month");
                            }
                        }
                        if (isInSelectedDate(dataSet) && !(new Date(dataSet.year, dataSet.calendarDate.shortMonth, dataSet.calendarDate.date) < date)) {
                            dataSet.calendarDate.css = "other-month active";
                        }
                        resultSet.push(dataSet);
                        dataSetKeyCounter++;
                    }
                }

                var prependDate = function (date) {
                    if (date.toString().length == 1) {
                        date = "0" + date.toString();
                    }
                    return date.toString();
                }

                var dateDifference = function (currentTime, css) {
                    var date = new Date();
                    var newDate = new Date(date.getFullYear(), (date.getMonth() + 1), date.getDate());
                    var dateDiff = currentTime - newDate.getTime();
                    return (dateDiff / (24 * 60 * 60 * 1000));
                }

                var disableDates = function (dataSet, css) {
                    var dateToCompare = $scope.dateformat == "dd/MM/yy" ? prependDate(dataSet.calendarDate.date) + "/" + prependDate(dataSet.calendarDate.shortMonth + 1) + "/" + dataSet.year :
                                     $scope.dateformat == "MM/dd/yy" ? prependDate(dataSet.calendarDate.shortMonth + 1) + "/" + prependDate(dataSet.calendarDate.date) + "/" + dataSet.year :
                                     prependDate(dataSet.calendarDate.date) + "/" + prependDate(dataSet.calendarDate.shortMonth + 1) + "/" + dataSet.year;

                    if ($.inArray(dateToCompare, $scope.disableDates) > -1 || !($.inArray(dataSet.calendarDate.day.toString(), $scope.disableDays) == -1)) {
                        return "out-of-range";
                    }
                    else {
                        return css;
                    }
                }

                var isCurrentDate = function (dataSet) {
                    var date = new Date();
                    if (date.getFullYear() === dataSet.year && date.getMonth() === dataSet.calendarDate.shortMonth
                        && date.getDate() === dataSet.calendarDate.date) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                var isInSelectedDate = function (dataSet) {
                    var selectedDate = new Date(dataSet.year, dataSet.calendarDate.shortMonth, dataSet.calendarDate.date).toString();
                    if ($.inArray(selectedDate, $scope.selectedDates) > -1) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                var setSelectedDates = function (obj) {
                    var selectedDate = new Date(obj.year, obj.calendarDate.shortMonth, obj.calendarDate.date).toString();
                    $scope.selectedDates.push(selectedDate);
                    $scope.dateSelected = $scope.dateformat == "dd/MM/yy" ? prependDate(obj.calendarDate.date) + "/" + prependDate(obj.calendarDate.shortMonth + 1) + "/" + obj.year :
                                     $scope.dateformat == "MM/dd/yy" ? prependDate(obj.calendarDate.shortMonth + 1) + "/" + prependDate(obj.calendarDate.date) + "/" + obj.year :
                                     prependDate(obj.calendarDate.date) + "/" + prependDate(obj.calendarDate.shortMonth + 1) + "/" + obj.year;                   
                }
                var clearSelectedDate = function () {
                    $scope.dateSelected = '';
                    $scope.oVal = null;
                    if ($scope.selectedDates != null && $scope.selectedDates.length > 0) {
                        $scope.selectedDates[0] = {};
                        $scope.selectedDates = [];
                    }
                }
                var removeSelectedDate = function (obj) {
                    var selectedDate = new Date(obj.year, obj.calendarDate.shortMonth, obj.calendarDate.date).toString();
                    if ($.inArray(selectedDate, $scope.selectedDates) > -1) {
                        var index = $scope.selectedDates.indexOf(selectedDate);
                        $scope.selectedDates.splice(index, 1);
                    }
                }

                var createBody = function (year, month, key, startDate) {
                    var date = new Date(year, month, startDate);
                    var year = date.getFullYear();
                    var currentMonth = date.getMonth();
                    var daysRemaining = new Date(year, (currentMonth + 1), 0).getDate();
                    for (j = 1; j <= daysRemaining; j++) {
                        var dataSet = {
                            year: "",
                            key: "",
                            calendarDate: {
                                date: "",
                                longMonth: "",
                                shortMonth: "",
                                day: "",
                                longDay: "",
                                css: ""
                            }
                        };
                        dataSet.key = key;
                        dataSet.year = year;
                        dataSet.calendarDate.longMonth = monthSet[currentMonth];
                        dataSet.calendarDate.shortMonth = currentMonth;
                        dataSet.calendarDate.date = j;
                        dataSet.calendarDate.day = new Date(year, currentMonth, j).getDay();
                        dataSet.calendarDate.longDay = daySet[dataSet.calendarDate.day];
                        var currentDate = new Date(dataSet.year, currentMonth, dataSet.calendarDate.date);
                        if ((new Date().getMonth()) != month || (new Date().getFullYear()) != dataSet.year) {
                            var minDate = getMinDates();
                            var maxDate = new Date((minDate.getFullYear()), (minDate.getMonth() + ($scope.numberofMonths - 1)), minDate.getDate());
                            if (currentDate > maxDate) {
                                dataSet.calendarDate.css = "out-of-range";
                            }
                            else if (currentDate < date) {
                                if (isInSelectedDate(dataSet)) {
                                    clearSelectedDate();
                                }
                                dataSet.calendarDate.css = "past-month";
                            }
                            else {
                                dataSet.calendarDate.css = disableDates(dataSet, "current-month");
                            }
                        }
                        else {
                            if (currentDate < date) {
                                if (isInSelectedDate(dataSet)) {
                                    clearSelectedDate();
                                }
                                dataSet.calendarDate.css = "past-month";
                            }
                            else {
                                dataSet.calendarDate.css = disableDates(dataSet, "current-month");
                                if ($scope.showCurrent == "true" && isCurrentDate(dataSet)) {
                                    dataSet.calendarDate.css = dataSet.calendarDate.css + ' ' + "current";
                                }
                            }
                        }
                        if (isInSelectedDate(dataSet) && !(new Date(dataSet.year, dataSet.calendarDate.shortMonth, dataSet.calendarDate.date) < date)) {
                            dataSet.calendarDate.css = "current-month active";
                        }

                        resultSet.push(dataSet);
                        dataSetKeyCounter++;
                    }
                }

                $scope.getDaysForPrevMonth = function () {
                    var date = new getMinDates();
                    resultSet = [];
                    if (date.getMonth() < $scope.month) {
                        $scope.month = $scope.month - 1;
                        var month = $scope.month;
                        var positionOfFirstDate = new Date(date.getFullYear(), month, 1).getUTCDay();
                        var positionOfLastDate = new Date(date.getFullYear(), month, daysInMonth(month, date.getFullYear()), 0).getUTCDay();
                        createHeader(date.getFullYear(), month, positionOfFirstDate, month);
                        var minDate = getMinDates();
                        if (minDate.getMonth() > month) {
                            createBody(date.getFullYear(), minDate.getMonth(), minDate.getMonth(), minDate.getDate());
                        }
                        else if (minDate.getMonth() == month) {
                            createBody(date.getFullYear(), month, month, minDate.getDate());
                        }
                        else {
                            createBody(date.getFullYear(), month, month, 1);
                        }
                        createTrailer(date.getFullYear(), (month + 1), positionOfLastDate, month);
                        $scope.currentMonthHeader = monthSet[(month % 12)] + "\t" + date.getFullYear();
                        $scope.resultSet = resultSet;
                        $scope.month = month;
                    }
                }

                $scope.getDaysForNextMonth = function () {
                    var date = new getMinDates();
                    resultSet = [];
                    if (($scope.numberofMonths - 1) > $scope.month) {
                        $scope.month = $scope.month + 1;
                        var month = $scope.month;
                        var positionOfFirstDate = new Date(date.getFullYear(), month, 1).getUTCDay();
                        var positionOfLastDate = new Date(date.getFullYear(), month, daysInMonth(month, date.getFullYear()), 0).getUTCDay();
                        createHeader(date.getFullYear(), month, positionOfFirstDate, month);
                        var minDate = getMinDates();
                        if (minDate.getMonth() == month) {
                            createBody(date.getFullYear(), month, month, minDate.getDate());
                        }
                        else {
                            createBody(date.getFullYear(), month, month, 1);
                        }
                        createTrailer(date.getFullYear(), (month + 1), positionOfLastDate, month);
                        $scope.currentMonthHeader = monthSet[(month % 12)] + "\t" + resultSet[0].year;
                        $scope.resultSet = resultSet;
                        $scope.month = month;
                    }
                }
                $scope.setDate = function (obj) {
                    if ($scope.multiDates == "true") {
                        if (obj.calendarDate.css.indexOf('current-month') > -1 || obj.calendarDate.css.indexOf('other-month') > -1) {
                            if (obj.calendarDate.css.indexOf('current-month current') > -1) {
                                obj.calendarDate.css = obj.calendarDate.css.split(' ')[0];
                            }
                            obj.calendarDate.css = obj.calendarDate.css.indexOf("active") > -1 ? obj.calendarDate.css.split(' ')[0] : obj.calendarDate.css + ' ' + "active";
                        }
                    }
                    else if (obj.calendarDate.css.indexOf('current-month') > -1 || obj.calendarDate.css.indexOf('other-month') > -1) {
                        if ($scope.oVal == null) {
                            $scope.oVal = obj;
                        }
                        else {
                            $scope.oVal.calendarDate.css = $scope.oVal.calendarDate.css.indexOf("active") > -1 ? $scope.oVal.calendarDate.css.split(' ')[0] : $scope.oVal.calendarDate.css + ' ' + "active";
                        }
                        if (obj.calendarDate.css.indexOf('current-month') > -1 || obj.calendarDate.css.indexOf('other-month') > -1) {
                            if (obj.calendarDate.css.indexOf('current-month current') > -1) {
                                obj.calendarDate.css = obj.calendarDate.css.split(' ')[0];
                            }
                            $.each($scope.resultSet, function (key, obj) {
                                var newDate = new Date($scope.resultSet[key].year, $scope.resultSet[key].calendarDate.shortMonth, $scope.resultSet[key].calendarDate.date).toString();
                                var oldDate = new Date($scope.oVal.year, $scope.oVal.calendarDate.shortMonth, $scope.oVal.calendarDate.date).toString();
                                if (newDate === oldDate) {
                                    if ($scope.resultSet[key] !== $scope.oVal) {
                                        $scope.resultSet[key] = $scope.oVal;
                                    }
                                    return false;
                                }

                            });
                            removeSelectedDate($scope.oVal);
                            obj.calendarDate.css = obj.calendarDate.css.indexOf("active") > -1 ? obj.calendarDate.css.split(' ')[0] : obj.calendarDate.css + ' ' + "active";
                            $scope.oVal = obj;
                            setSelectedDates(obj);
                        }
                    }
                }
                var getMinDates = function () {
                    if (typeof $scope.minStartDate != "undefined" && $scope.minStartDate != "") {
                        var minDate = $scope.minStartDate.split('/');
                        minDate[0] = typeof $scope.numOfDaysFromStart !== "undefined" && parseInt($scope.numOfDaysFromStart) > 0 ? (parseInt(minDate[0]) + parseInt($scope.numOfDaysFromStart)).toString() : minDate[0];
                        if ($scope.dateformat == "dd/MM/yy") {
                            date = new Date(minDate[2], (minDate[1] - 1), minDate[0]);
                        }
                        else {
                            date = new Date(minDate[2], (minDate[0] - 1), minDate[1]);
                        }
                    }
                    else {
                        date = new Date();
                    }
                    return date;
                }
                var GetCurrentDates = function () {
                    var date = getMinDates();
                    resultSet = [];
                    var month = date.getMonth();
                    var selectedDate = $scope.dateSelected != '' ? ($scope.dateformat == "dd/MM/yy" ? new Date($scope.dateSelected.split('/')[2], ($scope.dateSelected.split('/')[1] - 1), $scope.dateSelected.split('/')[0]) :
                       new Date($scope.dateSelected.split('/')[2], ($scope.dateSelected.split('/')[0] - 1), $scope.dateSelected.split('/')[1])) : '';
                    if (typeof $scope.month == "undefined" || selectedDate < getMinDates()) {
                        clearSelectedDate();
                        var positionOfFirstDate = new Date(date.getFullYear(), month, 1).getUTCDay();
                        var positionOfLastDate = new Date(date.getFullYear(), month, daysInMonth(month, date.getFullYear()), 0).getUTCDay();
                        createHeader(date.getFullYear(), month, positionOfFirstDate, month);
                        createBody(date.getFullYear(), month, month, date.getDate());
                        createTrailer(date.getFullYear(), (month + 1), positionOfLastDate, month);
                        $scope.currentMonthHeader = monthSet[(date.getMonth() % 12)] + "\t" + date.getFullYear();
                        $scope.days = daySet;
                        $scope.resultSet = resultSet;
                        $scope.month = month;
                    }
                }
                //this responds to the change event while setting minimum value
                $scope.getCurrentDates = function () {
                    //Gets days of current month
                    GetCurrentDates();
                }
                //this responds to when initial value is set to emplty
                $scope.clearSelectedDate = function () {                   
                    GetCurrentDates();
                }
                //Gets days of current month
                GetCurrentDates();
            }
        }
    }
    app.directive("ndsDate", dateDirective);
    app.directive("ndsOnDateSet", function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, el, attrs) {
                var fn = $parse(attrs['ndsOnDateSet']);
                el.on("click", function () {
                    scope.$apply(function () {
                        fn(scope);
                    })
                });
            },
        }
    })
}());