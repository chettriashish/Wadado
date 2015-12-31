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

    window.fbAsyncInit = function () {
        FB.init({
            appId: '963933207007150',
            cookie: true,
            xfbml: true,
            version: 'v2.2'
        });        
    };

    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement(s); js.id = id;
        js.src = "//connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));


}());