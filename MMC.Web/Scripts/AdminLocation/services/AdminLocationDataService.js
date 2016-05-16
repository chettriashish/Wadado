(function () {
    var app = angular.module("appMain");
    var adminLocationDataService = function ($http, $q, $window) {

        var getSelectedLocationDetails = function (locationKey) {
            var selectedLocationKey = locationKey;
            return $http({
                url: '/AdminLocation/GetSelectedLocation',
                method: 'GET',
                params: { selectedLocationKey: selectedLocationKey }
            }).then(function (response) {
                return response.data;
            })
            .catch(function (reponse) {

            });

        }
        var uploadImages = function (imagesToUpload) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                data: { locationImages: imagesToUpload },
                url: '/AdminLocation/UploadImages'
            }).success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        }
        var setNewLocationDetails = function () {
            return $http({
                url: '/AdminLocation/CreateNewLocation',
                method: 'GET'
            }).then(function (response) {
                return response.data;
            })
            .catch(function (reponse) {

            });
        }
        var saveLocationDetails = function (locationDetails) {
            var deferred = $q.defer();
            $http({
                url: '/AdminLocation/SaveLocationDetails',
                method: 'POST',               
                data: locationDetails
            }).success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        }

        /*USING THE REVEALING MODULE PATTERN TO EXPOSE ONLY THE METHODS THAT WE CHOOSE TO*/
        return {
            getSelectedLocationDetails: getSelectedLocationDetails,
            saveLocationDetails: saveLocationDetails,
            setNewLocationDetails: setNewLocationDetails,
            uploadImages: uploadImages,
        }
    };
    app.factory("AdminLocationDataService", adminLocationDataService);
}());