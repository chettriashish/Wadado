///<reference path="angular.js" />
///<reference path="angular.animate" />
///<reference path="angular-ui/ui-bootstrap-tpls-js" />
///<reference path="angular-ui/ui-bootstrap.js" />
'use strict';
(function () {
    //var app = angular.module('appMain', ['ngSanitize', 'ui.select', 'ngAnimate', 'ui.bootstrap', 'ngRoute']);
    var app = angular.module('appMain', ['ngSanitize', 'ui.select', 'ngAnimate', 'ngRoute', 'angular-click-outside'])
        .config(function ($routeProvider) {
        $routeProvider
        .when('/Location/:locationName', {
            templateUrl: '/Location/Index',
            controller: 'LocationController'
        })
        .when('/Activities/:locationName/:activity', {
            templateUrl: '/Activity/Index',
            controller: 'ActivityController'
        })
        .when('/', {
            templateUrl: '/Home/Index',
            controller: 'HomeController'
        })
        .otherwise({ redirectTo: "/" });
    });
}());