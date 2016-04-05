"use strict";
(function ($parse) {

    var app = angular.module("appMain");

    var universalSearch = function () {
        return {
            restrict: 'E',
            templateUrl: "../../Templates/TemplateName",
            scope: {
                searchResults: "=",
            },
        }
    }
    app.directive("UniversalSearch", universalSearch);
}());