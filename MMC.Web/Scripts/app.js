///<reference path="angular.js" />
///<reference path="angular.animate" />
///<reference path="angular-ui/ui-bootstrap-tpls-js" />
///<reference path="angular-ui/ui-bootstrap.js" />
(function () {
    //var app = angular.module('appMain', ['ngSanitize', 'ui.select', 'ngAnimate', 'ui.bootstrap', 'ngRoute']);
    var app = angular.module('appMain', ['ngSanitize', 'ui.select', 'ngAnimate', 'ngRoute', 'angular-click-outside', 'ui.router'])

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

    //Routing all admin related pages
    app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise("/");

        $stateProvider
        .state('adminLogin', {
            url: '/',
            //controller: 'AdminLoginController',
            templateUrl: Wadado.rootPath + '/Templates/_adminLogin.html'
        })
        .state('adminLocation', {
            url: '/location',
            controller: 'AdminLocationSummaryController',
            templateUrl: Wadado.rootPath + '/Templates/_adminLocationList.html',
            resolve: {
                allAvailableLocations: function (AdminLocationSummaryDataService) {
                    return AdminLocationSummaryDataService.getAllAvailableLocations();
                }
            }
        })
        .state('adminLocationEdit', {
            url: '/location/edit/:id',
            controller: 'AdminLocationDetailsController',
            templateUrl: Wadado.rootPath + '/Templates/_adminLocationDetailsEdit.html',
            resolve: {
                locationDetails: function (AdminLocationDataService, $stateParams) {
                    return AdminLocationDataService.getSelectedLocationDetails($stateParams.id);
                }
            }
        })
        .state('adminLocationCreate', {
            url: '/location/add',
            controller: 'AdminLocationDetailsController',
            templateUrl: Wadado.rootPath + '/Templates/_adminLocationDetailsEdit.html',
            resolve: {
                locationDetails: function (AdminLocationDataService) {
                    return AdminLocationDataService.setNewLocationDetails();
                }
            }
        })
        .state('admincategory_parent', {
            url: '/category',
            abstract: true,
            templateUrl: Wadado.rootPath + '/Templates/_adminCategoryContainer.html',
            controller: 'AdminCategoryContainerController'
        })
        .state('admincategory_parent.admincategories', {
            url: '/category-list',
            views: {
                'category-list': {
                    controller: 'AdminCategoryController',
                    templateUrl: Wadado.rootPath + '/Templates/_adminCategoryList.html',
                    resolve: {
                        allAvailableCategoryList: function (AdminCategoryDataService) {
                            return AdminCategoryDataService.getAllAvailableCategories();
                        }
                    }
                }
            }
        })
        .state('admincategory_parent.adminsubcategories', {
            url: '/subcategory-list',
            views: {
                'admin-subcategory-list':{
                    controller: 'AdminSubCategoryController',
                    templateUrl: Wadado.rootPath + '/Templates/_adminSubCategoryList.html',
                    resolve: {
                        allAvailableSubCategoryList: function (AdminCategoryDataService) {
                            return AdminCategoryDataService.getAllAvailableSubCategories();
                        }
                    }
                }                
            }
        })
        .state('admincategory_parent.admincategorytypemapping', {
            url: '/category-mapping',
            views: {
                'admin-category-type-mapping':{
                    controller: 'AdminCategoryTypeMappingController',
                    templateUrl: Wadado.rootPath + '/Templates/_adminCategoryTypeMapping.html',
                    resolve: {
                        allAvailableCategoryList: function (AdminCategoryDataService) {
                            return AdminCategoryDataService.getAllAvailableCategories();
                        }
                    }
                }                
            }
        })
        .state('adminActivities', {
            url: '/activities',
            controller: 'AdminActivitySummaryController',
            templateUrl: Wadado.rootPath + '/Templates/_adminActivityList.html',
            resolve: {
                allAvailableLocations: function (AdminActivityDataService) {
                    return AdminActivityDataService.getAllAvailableLocations();
                }
            }
        })
        .state('adminActivityEdit', {
            url: '/activities/:id',
            controller: 'AdminActivityDetailsController',
            templateUrl: Wadado.rootPath + '/Templates/_adminActivityDetails.html',
            resolve: {
                activityDetails: function (AdminActivityDataService, $stateParams) {
                    return AdminActivityDataService.getSelectedActivityDetails($stateParams.id);
                }
            }
        });

        //.state('adminSubCategoryList', {

        //})
        //.state('adminCategoryTypeMapping', {

        //});

        //.state('admin-home', {
        //    url: '/professional/home',
        //    controller: '',
        //    templateUrl: '',
        //})
        //.state('admin-activities', {
        //    url: '/professional/activities',
        //    controller: 'adminActivitySummaryController',
        //    templateUrl: ''
        //})
        //.state('admin-activities-parent', {
        //    abstract: true,
        //    url: '/professional/activities/:id',
        //    controller: 'adminActivityController',
        //    templateUrl: '',
        //})
        //.state('admin-activities-parent.admin-activities-details', {
        //    url: '/detail',
        //    controller: 'adminActivityDetailController',
        //    templateUrl: '',
        //})
        //.state('admin-activities-parent.admin-activities-details-edit', {
        //    url: '/edit',
        //    controller: 'adminActivityDetailController',
        //    templateUrl: '',
        //})
        //.state('admin-location', {
        //    url: '/professional/location',
        //    controller: 'AdminLocationSummaryController',
        //    templateUrl: '../Views/Templates/_adminLocationList.html',
        //    resolve: {
        //        locationDetail: function (AdminLocationDataService, $stateParams) {
        //            return AdminLocationDataService.getSelectedLocationDetails($stateParams.id);
        //        }
        //    }
        //})

        //Break here
        //.state('admin-location-detail', {
        //    url: '/professional/location:id',
        //    controller: 'AdminLocationDetailController',
        //    templateUrl: '',
        //    resolve: {
        //        locationDetail: function (AdminLocationDataService, $stateParams) {
        //            return AdminLocationDataService.getSelectedLocationDetails($stateParams.id);
        //        }
        //    }
        //})
        //.state('admin-location-edit', {
        //    url: '/professional/location:id',
        //    controller: 'AdminLocationDetailController',
        //    templateUrl: '',
        //    resolve: {
        //        locationDetail: function (AdminLocationDataService, $stateParams) {
        //            return AdminLocationDataService.getSelectedLocationDetails($stateParams.id);
        //        }
        //    }
        //})
        //.state('admin-activitycategory', {
        //    url: '/professional/activitycategory',
        //    controller: '',
        //    templateUrl: '',
        //});

    }]);
}());