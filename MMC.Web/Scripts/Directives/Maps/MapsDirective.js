(function () {
    var app = angular.module("appMain");
    var mapsDirective = function ($compile) {
        return {
            restrict: 'E',
            templateUrl: "../../Templates/_mapTemplate.html",
            scope: {
                geoJSON: "=ndsDataSource",
                center: "=center",
                activityIcon: "=activityIcon",
                startIcon: "=startIcon",
                onLoadActivity: "&ndsOnActivitySelect",
                showRoute: "@",
            },
            link: function (scope, el, attr) {
                scope.$watch('geoJSON', function (newValue, oldValue) {
                    if (newValue != oldValue) {
                        scope.loadMaps();
                    }
                }, true);
            },
            controller: function ($scope) {
                /*SETTING MAP DETAILS*/
                var destination;
                var map;
                var initMap = function () {
                    var lat = parseFloat($scope.center.split(',')[0]);
                    var long = parseFloat($scope.center.split(',')[1]);
                    var mapOptions = {
                        center: new google.maps.LatLng(lat, long),
                        zoom: 13,
                        mapTypeId: google.maps.MapTypeId.ROADMAP,
                        mapTypeControl: false,
                        scrollwheel:false,
                    };

                    map = new google.maps.Map(document.getElementById("map"), mapOptions);
                    initialCenter = mapOptions.center;
                    initialZoom = mapOptions.zoom;                    
                    //addGeoJSONDataLayer();
                    if ($scope.showRoute == "true") {
                        destination = "" + $scope.geoJSON.LatLong.split(',')[0] + "," + $scope.geoJSON.LatLong.split(',')[1] + "";
                        calcRoute();
                    }
                    else {
                        createGeoJSON();
                    }                   
                }


                var infowindow = new google.maps.InfoWindow();

                var addGeoJSONDataLayer = function () {
                    map.data.addGeoJson($scope.JSON);
                    //map.data.setStyle({
                    //    icon: Wadado.rootPath + $scope.activityIcon,
                    //    strokeColor: 'green'
                    //});
                }

                var icons = {
                    start: new google.maps.MarkerImage(
                     // TBD:ICON URL FROM FOLDER
                     Wadado.rootPath + $scope.startIcon,
                     // (width,height)
                     new google.maps.Size(44, 32),
                     // The origin point (x,y)
                     new google.maps.Point(0, 0),
                     // The anchor point (x,y)
                     new google.maps.Point(22, 32)
                    ),
                    end: new google.maps.MarkerImage(
                     // TBD:ICON URL FROM DB
                     Wadado.rootPath + $scope.activityIcon,
                     // (width,height)
                     new google.maps.Size(44, 32),
                     // The origin point (x,y)
                     new google.maps.Point(0, 0),
                     // The anchor point (x,y)
                     new google.maps.Point(22, 32)
                    )
                };

                var calcRoute = function () {
                    var directionsService = new google.maps.DirectionsService();
                    var directionsDisplay;
                    //directionsDisplay = new google.maps.DirectionsRenderer({ suppressMarkers: true });
                    directionsDisplay = new google.maps.DirectionsRenderer();
                    directionsDisplay.setMap(map);
                    var request = {
                        origin: $scope.center,
                        destination: destination,
                        travelMode: google.maps.TravelMode.DRIVING,
                    };
                    directionsService.route(request, function (result, status) {
                        if (status == google.maps.DirectionsStatus.OK) {
                            directionsDisplay.setDirections(result);
                            var leg = result.routes[0].legs[0];
                            //makeMarker(leg.start_location, icons.start, "A");
                            //makeMarker(leg.end_location, icons.end, "B");
                        }
                    });
                }

                var makeMarker = function (position, icon, title) {
                    new google.maps.Marker({
                        position: position,
                        map: map,
                        icon: icon,
                        title: title
                    });
                }

                var createGeoJSON = function () {
                    //Create GEO JSON
                    //var jsonStr = '{ "type":"FeatureCollection",' +
                    //'"features":[';                   
                    $.each($scope.geoJSON, function (key, value) {
                        addMarkers($scope.geoJSON[key]);
                        //this is an example of geoJSON
                        //var content = '{' +
                        //    '"type": "Feature",' +
                        //    '"geometry": {' +
                        //        '"type": "Point",' +
                        //        '"coordinates":[' +
                        //         parseFloat($scope.geoJSON[key].LatLong.split(',')[1]) + ',' +
                        //         parseFloat($scope.geoJSON[key].LatLong.split(',')[0]) + ',' +
                        //         0 + ']' +
                        //        '},' +
                        //   '"properties": {' +
                        //   '"name":' + '"' + $scope.geoJSON[key].ActivityName + '"' + ',' +
                        //   '"styleUrl":"#style8",' +
                        //   '"styleHash":"43da3fc5"' +
                        //   '}';
                        //jsonStr = jsonStr + content;
                        //if (key == ($scope.geoJSON.length - 1)) {
                        //    jsonStr = jsonStr + '}'
                        //}
                        //else {
                        //    jsonStr = jsonStr + '},'
                        //}

                        //end geoJSON
                    });

                    //jsonStr = jsonStr + ']}';
                    //$scope.JSON = JSON.parse(jsonStr);
                }

                $scope.loadLocation = function (obj) {
                    $scope.onLoadActivity({ item: obj });
                }
                var labelIndex = 0;
                var openInfoWindows = [];
                var addMarkers = function (item) {
                    $scope.selectedItem;
                    var centerMarker = new google.maps.Marker({
                        //icon: Wadado.rootPath + item.MapIconURL,
                        position: new google.maps.LatLng(parseFloat(item.LatLong.split(',')[0]), parseFloat(item.LatLong.split(',')[1])),
                        map: map,
                        title: item.ActivityName,
                        id: item.ActivityKey.replace(' ', ''),
                    });
                    google.maps.event.addListener(
					centerMarker,
					'mouseover',
					    (function (centerMarker, $scope) {
					        return function () {
					            $scope.boundItem = item;
					            var contentString = '<div id="content' + centerMarker.id + '" class="map-content">' +
                                                         '<div class="column-12">' +
                                                                 '<div>' +
                                                                     '<span>' + item.ActivityName + '</span>' +
                                                                 '</div>' +
                                                                 '<div ng-click="loadLocation(boundItem)">' +
                                                                     '<span>View Details</span>' +
                                                                 '</div>' +
                                                         '</div>' +
                                                     '</div>';



					            //$scope.$apply(); // must be inside write new values for each marker
					            var onload = function () {
					                $scope.$apply(function () {
					                    $compile(document.getElementById("content" + centerMarker.id))($scope)
					                });
					            }
					            if (!infowindow[centerMarker.id]) {
					                infowindow[centerMarker.id] = new google.maps.InfoWindow({
					                    content: contentString
					                });
					                google.maps.event.addListener(infowindow[centerMarker.id], 'domready', function (a, b, c, d) {
					                    onload();
					                });
					            }
					            if (!centerMarker.get('open')) {
					                if (openInfoWindows.length > 0) {
					                    $.each(openInfoWindows, function (key) {
					                        openInfoWindows[key].close();
					                    })
					                }
					                $scope.selectedItem =
					                centerMarker.set('open', false);
					                infowindow[centerMarker.id].open(centerMarker.get('map'), centerMarker);
					                openInfoWindows.push(infowindow[centerMarker.id]);

					            }
					            else {
					                centerMarker.set('open', false);
					                infowindow[centerMarker.id].close();
					            }

					        };
					    })(centerMarker, $scope)
				    ); // addListener
                }


                /*END SETTING MAP DETAILS*/
                $scope.loadMaps = function () {
                    initMap();
                }
            }
        }
    }
    app.directive("ndsMaps", mapsDirective);
}());