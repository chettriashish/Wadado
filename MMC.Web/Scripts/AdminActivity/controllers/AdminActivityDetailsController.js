(function () {
    var app = angular.module("appMain");
    var adminActivityDetailsController = function ($scope, $http, $timeout, $interval, $location, $state, activity, isEdit, location
        , AdminActivityDataService, AdminCategoryDataService) {
        $scope.isEdit = isEdit;
        $scope.activityDetails = activity;
        $scope.isBasicInfo = true;
        $scope.isPeopleInfo = false;
        $scope.isDatesInfo = false;
        $scope.interval = {
            minutes: new Date(1970, 0, 1, 0, 0)
        };
        var patt1 = /[0-9]/g;
        var patt2 = /[a-z A-Z]/g;
        $scope.activityStartEnd = {};
        /*Setting activity start end times */
        if ($scope.activityDetails.ActivityStartTime) {
            var timeComp = $scope.activityDetails.ActivityStartTime.match(patt1);
            var amPM = $scope.activityDetails.ActivityStartTime.match(patt2);
            if ($scope.activityDetails.ActivityStartTime.indexOf(':') > -1 && timeComp.length < 4) {
                $scope.activityStartEnd.activityStartTime = new Date(1970, 0, 1, timeComp[0], (timeComp[1] + '' + (typeof timeComp[2] == 'undefined' ? 0 : timeComp[2])), 0);
            }
            else if ($scope.activityDetails.ActivityStartTime.indexOf(':') > -1 && timeComp.length == 4) {
                $scope.activityStartEnd.activityStartTime = new Date(1970, 0, 1, (timeComp[0] + '' + timeComp[1]), (timeComp[2] + '' + (typeof timeComp[3] == 'undefined' ? 0 : timeComp[3])), 0);
            }
            else if ($scope.activityDetails.ActivityStartTime.indexOf(':') == -1 && timeComp.length == 2) {
                $scope.activityStartEnd.activityStartTime = new Date(1970, 0, 1, (timeComp[0] + '' + timeComp[1]), 0, 0);
            }
            $scope.activityStartEnd.activityStartTimeAMPM = (amPM[0] + amPM[1]);
        }
        else {
            $scope.activityStartEnd.activityStartTime = new Date(1970, 0, 1, 9, 0);
            $scope.activityStartEnd.activityStartTimeAMPM = "AM";
        }
        if ($scope.activityDetails.ActivityEndTime) {
            var timeComp = $scope.activityDetails.ActivityEndTime.match(patt1);
            var amPM = $scope.activityDetails.ActivityEndTime.match(patt2);
            if ($scope.activityDetails.ActivityEndTime.indexOf(':') > -1 && timeComp.length < 4) {
                $scope.activityStartEnd.activityEndTime = new Date(1970, 0, 1, timeComp[0], (timeComp[1] + '' + (typeof timeComp[2] == 'undefined' ? 0 : timeComp[2])), 0);
            }
            else if ($scope.activityDetails.ActivityEndTime.indexOf(':') > -1 && timeComp.length == 4) {
                $scope.activityStartEnd.activityEndTime = new Date(1970, 0, 1, (timeComp[0] + '' + timeComp[1]), (timeComp[2] + '' + (typeof timeComp[3] == 'undefined' ? 0 : timeComp[3])), 0);
            }
            else if ($scope.activityDetails.ActivityEndTime.indexOf(':') == -1 && timeComp.length == 2) {
                $scope.activityStartEnd.activityStartTime = new Date(1970, 0, 1, (timeComp[0] + '' + timeComp[1]), 0, 0);
            }
            $scope.activityStartEnd.activityEndTimeAMPM = (amPM[0] + amPM[1]);
        }
        else {
            $scope.activityStartEnd.activityEndTime = new Date(1970, 0, 1, 5, 0);
            $scope.activityStartEnd.activityEndTimeAMPM = "PM";
        }
        /*End Setting activity start end times */
        $scope.activityDetails.Location = location;
        AdminCategoryDataService.getAllAvailableSubCategoriesAsync().then(function (response) {
            $scope.allActivitySubCategories = response;
            $.each($scope.allActivitySubCategories, function (key, value) {
                if ($scope.allActivitySubCategories[key].ActivityType == $scope.activityDetails.ActivitySubCategory) {
                    $scope.category = {};
                    $scope.category.selected = $scope.allActivitySubCategories[key];
                    return false;
                }
            });
        });

        $scope.$watch('category.selected', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                if ($scope.allActivitySubCategories.indexOf(newVal) === -1) {
                    $scope.allActivitySubCategories.unshift(newVal);
                }
            }
        });

        $scope.$watch('location.selected', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                if ($scope.allLocations.indexOf(newVal) === -1) {
                    $scope.allLocations.unshift(newVal);
                }
            }
        });

        AdminActivityDataService.getAllAvailableLocationsAsync().then(function (response) {
            $scope.allLocations = response;
            $.each($scope.allLocations, function (key, value) {
                if ($scope.allLocations[key].LocationName == $scope.activityDetails.Location) {
                    $scope.location = {};
                    $scope.location.selected = $scope.allLocations[key];
                    return false;
                }
            });

        });

        $scope.days = {
            sun: false,
            mon: false,
            tue: false,
            wed: false,
            thu: false,
            fri: false,
            sat: false
        }

        $.each($scope.activityDetails.AllActivityDates, function (key, value) {
            switch ($scope.activityDetails.AllActivityDates[value]) {
                case 0: $scope.days.sun = true; break;
                case 1: $scope.days.mon = true; break;
                case 2: $scope.days.tue = true; break;
                case 3: $scope.days.wed = true; break;
                case 4: $scope.days.thu = true; break;
                case 5: $scope.days.fri = true; break;
                case 6: $scope.days.sat = true; break;
            }
        });

        if ($scope.activityDetails.AllActivityTimes.length > 0) {
            $scope.AllActivityTimes = [];
            $.each($scope.activityDetails.AllActivityTimes, function (key, value) {
                var activityTime = {};
                activityTime.ActivityKey = $scope.activityDetails.ActivityKey;
                activityTime.time = $scope.activityDetails.AllActivityTimes[key];
                activityTime.editMode = false;
                var timeComp = activityTime.time.match(patt1);
                var amPM = activityTime.time.match(patt2);
                if (activityTime.time.indexOf(':') > -1 && timeComp.length < 4) {
                    activityTime.timeComp = new Date(1970, 0, 1, timeComp[0], (timeComp[1] + '' + (typeof timeComp[2] == 'undefined' ? 0 : timeComp[2])), 0);
                }
                else if (activityTime.time.indexOf(':') > -1 && timeComp.length == 4) {
                    activityTime.timeComp = new Date(1970, 0, 1, (timeComp[0] + '' + timeComp[1]), (timeComp[2] + '' + (typeof timeComp[3] == 'undefined' ? 0 : timeComp[3])), 0);
                }
                else if (activityTime.time.indexOf(':') == -1 && timeComp.length == 2) {
                    activityTime.timeComp = new Date(1970, 0, 1, (timeComp[0] + '' + timeComp[1]), 0, 0);
                }
                activityTime.AMPM = (amPM[0] + amPM[1]);
                $scope.AllActivityTimes.push(activityTime);
            });
            $scope.listTime = true;

            $scope.editSelectedTime = function (item) {
                item.editMode = true;

            }
        }
        $scope.loadBasicDetails = function () {
            setBasicInfo();
        }

        $scope.loadPersonnelDetails = function () {
            setPersonnelInfo();
        }

        $scope.loadBookingDetails = function () {
            setBookingInfo();
        }

        var setBasicInfo = function () {
            $scope.isBasicInfo = true;
            $scope.isPeopleInfo = false;
            $scope.isDatesInfo = false;
            $("#personnel").removeClass("active");
            $("#booking").removeClass("active");
            $("#basic").addClass("active");
        }

        var setPersonnelInfo = function () {
            $scope.isBasicInfo = false;
            $scope.isDatesInfo = false;
            $scope.isPeopleInfo = true;
            $("#basic").removeClass("active");
            $("#booking").removeClass("active");
            $("#personnel").addClass("active");
        }

        var setBookingInfo = function () {
            $scope.isBasicInfo = false;
            $scope.isPeopleInfo = false;
            $scope.isDatesInfo = true;
            $("#basic").removeClass("active");
            $("#personnel").removeClass("active");
            $("#booking").addClass("active");
        }

        $scope.saveSelectedTime = function (item) {
            if (item == "") {
                var index = $scope.AllActivityTimes.indexOf(item);
                if (index > -1) {
                    $scope.AllActivityTimes.splice(index, 1);
                    item.editMode = false;
                }
            }
            else {
                item.time = (item.timeComp.getHours() == 12 ? 12 : item.timeComp.getHours() % 12) + (item.timeComp.getMinutes() > 0 ? ':' + item.timeComp.getMinutes() : '') + item.AMPM;
                item.editMode = false;
            }
        }

        $scope.addNewTime = function () {
            var newTime = {
                ActivityKey: $scope.activityDetails.ActivityKey,
                time: '1 PM',
                editMode: true,
                timeComp: new Date(1970, 0, 1, 1, 0, 0),
                AMPM: 'PM'
            };
            if ($scope.AllActivityTimes.length == 0) {
                $scope.listTime = true;
            }
            $scope.AllActivityTimes.push(newTime);
        }

        $scope.deleteSelectedTime = function (item) {
            var index = $scope.AllActivityTimes.indexOf(item);
            if (index > -1) {
                $scope.AllActivityTimes.splice(index, 1);
                if ($scope.AllActivityTimes.length == 0) {
                    $scope.listTime = false;
                }
            }
        }

        $scope.setTimeInterval = function () {
            var startTimeHours = ($scope.activityStartEnd.activityStartTime.getHours() == 12 ? 12 : $scope.activityStartEnd.activityStartTime.getHours());
            var endTimeHours = ($scope.activityStartEnd.activityEndTime.getHours() == 12 ? 12 : $scope.activityStartEnd.activityEndTime.getHours());
            startTimeHours = $scope.activityStartEnd.activityStartTimeAMPM == 'PM' ? startTimeHours + 12 : startTimeHours;
            endTimeHours = $scope.activityStartEnd.activityEndTimeAMPM == 'PM' ? endTimeHours + 12 : endTimeHours;
            var startTimeMins = $scope.activityStartEnd.activityStartTime.getMinutes();
            var endTimeMins = $scope.activityStartEnd.activityEndTime.getMinutes();
            var newTime = {
                ActivityKey: $scope.activityDetails.ActivityKey,
                time: ((startTimeHours == 12 ? 12 : startTimeHours % 12) + (startTimeMins > 0 ? ((startTimeMins + '').length > 1 ? ':' + startTimeMins : ':0' + startTimeMins) : ':00') + (startTimeHours >= 12 ? 'PM' : 'AM')),
                editMode: false,
                timeComp: new Date(1970, 0, 1, (startTimeHours == 12 ? 12 : startTimeHours % 12), parseInt(startTimeMins), 0),
                AMPM: (startTimeHours >= 12 ? 'PM' : 'AM')
            };
            $scope.AllActivityTimes.push(newTime);
            while ((startTimeHours + (startTimeMins == 0 ? 0 : startTimeMins / 60)) < (endTimeHours + (endTimeMins == 0 ? 0 - ($scope.interval.minutes.getMinutes() / 60) : ((endTimeMins - $scope.interval.minutes.getMinutes()) / 60)))) {
                if (startTimeMins + $scope.interval.minutes.getMinutes() >= 60) {
                    startTimeHours = startTimeHours + 1;
                    startTimeMins = (startTimeMins + $scope.interval.minutes.getMinutes()) - 60;
                    var newTime = {
                        ActivityKey: $scope.activityDetails.ActivityKey,
                        time: ((startTimeHours == 12 ? 12 : startTimeHours % 12) + (startTimeMins > 0 ? ((startTimeMins + '').length > 1 ? ':' + startTimeMins : ':0' + startTimeMins) : ':00') + (startTimeHours >= 12 ? 'PM' : 'AM')),
                        editMode: false,
                        timeComp: new Date(1970, 0, 1, (startTimeHours == 12 ? 12 : startTimeHours % 12), (startTimeMins > 0 ? startTimeMins : 0), 0),
                        AMPM: (startTimeHours >= 12 ? 'PM' : 'AM')
                    };
                }
                else {
                    var newTime = {
                        ActivityKey: $scope.activityDetails.ActivityKey,
                        time: ((startTimeHours == 12 ? 12 : startTimeHours % 12) + ($scope.interval.minutes.getMinutes() > 0 ? ':' + parseInt(startTimeMins + $scope.interval.minutes.getMinutes()) : '') + (startTimeHours >= 12 ? 'PM' : 'AM')),
                        editMode: false,
                        timeComp: new Date(1970, 0, 1, (startTimeHours == 12 ? 12 : startTimeHours % 12), parseInt(startTimeMins + $scope.interval.minutes.getMinutes()), 0),
                        AMPM: (startTimeHours >= 12 ? 'PM' : 'AM')
                    };
                    startTimeMins = startTimeMins + $scope.interval.minutes.getMinutes();
                }
                $scope.AllActivityTimes.push(newTime);
            }
            if ($scope.AllActivityTimes.length > 0) {
                $scope.listTime = true;
            }
        }
        $scope.loadNext = function () {
            if ($scope.isBasicInfo == true) {
                setPersonnelInfo();
            }
            else if ($scope.isPeopleInfo == true) {
                setBookingInfo();
            }
        }
        $scope.loadPrevious = function () {
            if ($scope.isPeopleInfo == true) {
                setBasicInfo();
            }
            else if ($scope.isDatesInfo == true) {
                setPersonnelInfo();
            }
        }

        $scope.saveChanges = function () {
            $scope.activityDetails.ActivityStartTime = ($scope.activityStartEnd.activityStartTime.getHours() == 12 ? 12 : $scope.activityStartEnd.activityStartTime.getHours() % 12) + ($scope.activityStartEnd.activityStartTime.getMinutes() > 0 ? ':' + $scope.activityStartEnd.activityStartTime.getMinutes() : '') + $scope.activityStartEnd.activityStartTimeAMPM;
            $scope.activityDetails.ActivityEndTime = ($scope.activityStartEnd.activityEndTime.getHours() == 12 ? 12 : $scope.activityStartEnd.activityEndTime.getHours() % 12) + ($scope.activityStartEnd.activityEndTime.getMinutes() > 0 ? ':' + $scope.activityStartEnd.activityEndTime.getMinutes() : '') + $scope.activityStartEnd.activityEndTimeAMPM;
            AdminActivityDataService.saveActivityDetails($scope.activityDetails, $scope.days, $scope.AllActivityTimes)
        }

        $scope.cancelChanges = function () {
            $state.go("adminActivities");
        }
    }
    app.controller("AdminActivityDetailsController", adminActivityDetailsController);
}());
