﻿(function () {
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
        $scope.imagesForActivity = [];
        if ($scope.activityDetails.ActivityImages != null) {
            if ($scope.activityDetails.ActivityImages.length > 0) {
                for (i = 0; i < $scope.activityDetails.ActivityImages.length; i++) {
                    var image = { result: "", name: $scope.activityDetails.ActivityImages[i].split('/')[1] };
                    $scope.imagesForActivity.push(image);
                }
            }
        }
        $scope.allSelectedActivityTags = [];
        $scope.tag = { Tag: "" };
        /*Setting initial activity tags if present*/
        if ($scope.activityDetails.Tags != null && $scope.activityDetails.Tags.length > 0) {
            $.each($scope.activityDetails.Tags, function (key, value) {
                $scope.tag.Tag = $scope.activityDetails.Tags[key];
                $scope.allSelectedActivityTags.push($scope.tag);
                $scope.tag = { Tag: "" };
            });
        }
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
            else if ($scope.activityDetails.ActivityStartTime.indexOf(':') == -1 && timeComp.length == 1) {
                $scope.activityStartEnd.activityStartTime = new Date(1970, 0, 1, ('0' + timeComp[0]), 0, 0);
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
            else if ($scope.activityDetails.ActivityEndTime.indexOf(':') == -1 && timeComp.length > 1) {
                $scope.activityStartEnd.activityEndTime = new Date(1970, 0, 1, (timeComp[0] + '' + timeComp[1]), 0, 0);
            }
            else if ($scope.activityDetails.ActivityEndTime.indexOf(':') == -1 && timeComp.length == 1) {
                $scope.activityStartEnd.activityEndTime = new Date(1970, 0, 1, ('0' + timeComp[0]), 0, 0);
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
            $scope.category = {};

            $.each($scope.allActivitySubCategories, function (key, value) {
                if ($scope.allActivitySubCategories[key].ActivityType == $scope.activityDetails.ActivitySubCategory) {
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

        $scope.setSelectedSubCategory = function (item) {
            $scope.category = item;
            $scope.tag.Tag = item;
            $scope.$scope.allSelectedActivityTags.push(tag);
        }

        $scope.selectedLocation = function (item) {
            $scope.location = item;
            var tag = { Tag: "" };
            tag.Tag = item;
            $scope.$scope.allSelectedActivityTags.push(tag);
        }

        AdminActivityDataService.getAllAvailableLocationsAsync().then(function (response) {
            $scope.allLocations = response;
            $scope.location = {};
            $.each($scope.allLocations, function (key, value) {
                if ($scope.allLocations[key].LocationName == $scope.activityDetails.Location) {
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
        if ($scope.activityDetails.AllActivityDates != null) {
            $.each($scope.activityDetails.AllActivityDates, function (key, value) {
                switch ($scope.activityDetails.AllActivityDates[key]) {
                    case 0: $scope.days.sun = true; break;
                    case 1: $scope.days.mon = true; break;
                    case 2: $scope.days.tue = true; break;
                    case 3: $scope.days.wed = true; break;
                    case 4: $scope.days.thu = true; break;
                    case 5: $scope.days.fri = true; break;
                    case 6: $scope.days.sat = true; break;
                }
            });
        }

        $scope.AllActivityTimes = [];
        $scope.AllEventDateTimes = [];
        $scope.AllActivityPricingOptions = [];
        if ($scope.activityDetails.AllActivityTimes != null && $scope.activityDetails.AllActivityTimes.length > 0) {
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
                else if (activityTime.time.indexOf(':') == -1 && timeComp.length > 1) {
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

        if ($scope.activityDetails.AllActivityUniqueDates != null && $scope.activityDetails.AllActivityUniqueDates.length > 0) {
            $.each($scope.activityDetails.AllActivityUniqueDates, function (key, value) {
                var eventDateTime = {};
                eventDateTime.ActivityKey = $scope.activityDetails.ActivityKey;
                eventDateTime.time = $scope.activityDetails.AllActivityUniqueDates[key].Time;
                eventDateTime.editMode = false;
                var regex = /[0-9]+/;
                var date = $scope.activityDetails.AllActivityUniqueDates[key].Date;
                var result = Number(date.match(regex)[0]);
                eventDateTime.m_date = new Date(result).toDateString();
                eventDateTime.Date = new Date(result);
                var timeComp = eventDateTime.time.match(patt1);
                var amPM = eventDateTime.time.match(patt2);
                if (eventDateTime.time.indexOf(':') > -1 && timeComp.length < 4) {
                    eventDateTime.timeComp = new Date(1970, 0, 1, timeComp[0], (timeComp[1] + '' + (typeof timeComp[2] == 'undefined' ? 0 : timeComp[2])), 0);
                }
                else if (eventDateTime.time.indexOf(':') > -1 && timeComp.length == 4) {
                    eventDateTime.timeComp = new Date(1970, 0, 1, (timeComp[0] + '' + timeComp[1]), (timeComp[2] + '' + (typeof timeComp[3] == 'undefined' ? 0 : timeComp[3])), 0);
                }
                else if (eventDateTime.time.indexOf(':') == -1 && timeComp.length > 1) {
                    eventDateTime.timeComp = new Date(1970, 0, 1, (timeComp[0] + '' + timeComp[1]), 0, 0);
                }
                eventDateTime.AMPM = (amPM[0] + amPM[1]);
                $scope.AllEventDateTimes.push(eventDateTime);
            });
            $scope.listEventTime = true;
        }

        if ($scope.activityDetails.AllPriceOptions != null && $scope.activityDetails.AllPriceOptions.length > 0) {
            $.each($scope.activityDetails.AllPriceOptions, function (key, value) {
                var priceOption = {};
                priceOption.OptionDescription = $scope.activityDetails.AllPriceOptions[key].OptionDescription;
                priceOption.PriceForAdults = $scope.activityDetails.AllPriceOptions[key].PriceForAdults;
                priceOption.PriceForChildren = $scope.activityDetails.AllPriceOptions[key].PriceForChildren;
                priceOption.ActivityPricingKey = $scope.activityDetails.AllPriceOptions[key].ActivityPricingKey;
                priceOption.editMode = false;
                $scope.AllActivityPricingOptions.push(priceOption);
            });
        }

        $scope.editSelectedDateTime = function (item) {
            item.editMode = true;
        }

        $scope.editSelectedPriceOption = function (item) {
            item.editMode = true;
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

        $scope.saveSelectedDateTime = function (item) {
            if (item == "") {
                var index = $scope.AllEventDateTimes.indexOf(item);
                if (index > -1) {
                    $scope.AllEventDateTimes.splice(index, 1);
                    item.editMode = false;
                }
            }
            else {
                item.time = (item.timeComp.getHours() == 12 ? 12 : item.timeComp.getHours() % 12) + (item.timeComp.getMinutes() > 0 ? ':' + item.timeComp.getMinutes() : '') + item.AMPM;
                item.editMode = false;
                //Setting Dates Correctly
                var regex = /[0-9]+/;
                var date = item.Date.toDateString();
                item.m_date = date;
            }
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
        $scope.saveSelectedPriceOption = function (item) {
            if (item == "") {
                var index = $scope.AllActivityPricingOptions.indexOf(item);
                if (index > -1) {
                    $scope.AllActivityPricingOptions.splice(index, 1);
                    item.editMode = false;
                }
            }
            else {
                item.editMode = false;
            }
        }
        $scope.addNewEventDateTime = function () {
            var newEventDateTime = {
                ActivityKey: $scope.activityDetails.ActivityKey,
                Date: new Date(),
                timeComp: new Date(1970, 0, 1, 1, 0, 0),
                editMode: true,
                AMPM: 'PM',
                time: '1 PM',
                m_date: ''
            };
            if ($scope.AllEventDateTimes.length > 0) {
                $scope.isEvent = true;
            }
            $scope.AllEventDateTimes.push(newEventDateTime);
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

        $scope.addPriceOption = function () {
            var newPriceOption = {
                ActivityKey: $scope.activityDetails.ActivityKey,
                ActivityPricingKey: '',
                OptionDescription: '',
                PriceForAdults: 0,
                PriceForChildren: 0,
                editMode: true
            };
            if ($scope.AllActivityPricingOptions.length > 0) {
                $scope.isPriceOptionEvent = true;
            }
            $scope.AllActivityPricingOptions.push(newPriceOption);
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

        $scope.deleteSelectedDateTime = function (item) {
            var index = $scope.AllEventDateTimes.indexOf(item);
            if (index > -1) {
                $scope.AllEventDateTimes.splice(index, 1);
                if ($scope.AllEventDateTimes.length == 0) {
                    $scope.listTime = false;
                }
            }
        }

        $scope.deleteSelectedPriceOption = function (item) {
            var index = $scope.AllActivityPricingOptions.indexOf(item);
            if (index > -1) {
                $scope.AllActivityPricingOptions.splice(index, 1);
                if ($scope.AllActivityPricingOptions.length == 0) {
                    $scope.isPriceOptionEvent = false;
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
            if ($scope.allSelectedActivityTags != null) {
                for (i = 0; i < $scope.allSelectedActivityTags.length; i++) {
                    var tag = $scope.allSelectedActivityTags[i].Tag;
                    if ($scope.activityDetails.Tags.indexOf(tag) == -1) {
                        $scope.activityDetails.Tags.push(tag);
                    }
                };
            }
            if ($scope.activityDetails.IsActivity) {
                $scope.activityDetails.ActivityStartTime = ($scope.activityStartEnd.activityStartTime.getHours() == 12 ? 12 : $scope.activityStartEnd.activityStartTime.getHours() % 12) + ($scope.activityStartEnd.activityStartTime.getMinutes() > 0 ? ':' + $scope.activityStartEnd.activityStartTime.getMinutes() : '') + $scope.activityStartEnd.activityStartTimeAMPM;
                $scope.activityDetails.ActivityEndTime = ($scope.activityStartEnd.activityEndTime.getHours() == 12 ? 12 : $scope.activityStartEnd.activityEndTime.getHours() % 12) + ($scope.activityStartEnd.activityEndTime.getMinutes() > 0 ? ':' + $scope.activityStartEnd.activityEndTime.getMinutes() : '') + $scope.activityStartEnd.activityEndTimeAMPM;
                AdminActivityDataService.saveActivityDetails($scope.activityDetails, $scope.days, $scope.AllActivityTimes, $scope.AllActivityPricingOptions, $scope.category.selected.ActivityTypeKey, $scope.location.selected.LocationKey).then(function (response) {
                    if (response == true) {
                        if ($scope.imagesForActivity.length > 0) {
                            AdminActivityDataService.uploadImages($scope.activityDetails.ActivityKey, $scope.imagesForActivity).then(function (response) {
                                var message = {};
                                message.header = 'Confirmation';
                                message.body = 'activity has been saved';
                                message.showButtons = false;
                                message.isUserAction = false;
                                $scope.$emit("DIALOG_S", message);
                                setTimeout(function () {
                                    $scope.$emit("DIALOG_H", message);
                                }, 1500);
                            })
                            .catch(function (response) {
                                console.log(response);
                            });;
                        }
                    }
                })
                .catch(function (response) {
                    console.log(response);
                });
            }
            else if ($scope.activityDetails.IsEvent) {
                AdminActivityDataService.saveEventDetails($scope.activityDetails, $scope.AllEventDateTimes, $scope.AllActivityPricingOptions, $scope.category.selected.ActivityTypeKey, $scope.location.selected.LocationKey).then(function (response) {
                    if (response == true) {
                        var message = {};
                        message.header = 'Confirmation';
                        message.body = 'activity has been saved';
                        message.showButtons = false;
                        message.isUserAction = false;
                        $scope.$emit("DIALOG_S", message);
                        setTimeout(function () {
                            $scope.$emit("DIALOG_H", message);
                        }, 1500);
                    }
                })
                .catch(function (response) {
                    console.log(response);
                });
            }
        }

        $scope.cancelChanges = function () {
            $state.go("adminActivities");
        }

        $scope.setEventActivity = function (option) {
            switch (option) {
                case 'event': $scope.activityDetails.IsActivity = !$scope.activityDetails.IsEvent; break;
                case 'activity': $scope.activityDetails.IsEvent = !$scope.activityDetails.IsActivity; break;
            }
        }

        $scope.addImage = function () {
            $scope.$emit("DIALOG_IMAGE_S", "NEW");
        }
        $scope.$on("IMAGE_CROPPED_REDIRECT", function (event, args) {
            var image = { result: "", name: "" };
            image.result = args.result;
            image.name = args.name;
            $scope.imagesForActivity.push(image);
        });
        $scope.addNewTag = function () {
            $scope.allSelectedActivityTags.push($scope.tag);
            $scope.tag = { Tag: "" };
        }

        $scope.removeSelectedTag = function (item) {
            var index = $scope.allSelectedActivityTags.indexOf(item);
            if (index > -1) {
                $scope.allSelectedActivityTags.splice(index, 1);
            }
        }
    }
    app.controller("AdminActivityDetailsController", adminActivityDetailsController);
}());
