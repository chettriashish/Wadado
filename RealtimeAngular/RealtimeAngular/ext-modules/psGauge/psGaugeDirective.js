angular.module('psGauge').directive('psGauge', function (psWebMetricsService) {
    return {
        templateUrl: 'ext-modules/psGauge/psGaugetemplate.html',
        link: function (scope, element, attrs) {
            scope.options = {
                width: 400, height: 120,
                redFrom: 90, redTo: 100,
                yellowFrom: 75, yellowTo: 90,
                minorTicks: 5
            };
            scope.init = false;
            scope.$on('psWebMetricsService-received-data-event',
                function (event, data) {
                    if (!scope.init) {
                        scope.data = google.visualization.arrayToDataTable([
                                                                       ['Label', 'Value'],
                                                                       ['CPU%', 0]
                        ]);
                        scope.chart = new google.visualization.Gauge(element[0]);
                        scope.init = true;
                    }
                    scope.data.setValue(0, 1, Math.round(data['cpuPct']))
                    scope.chart.draw(scope.data, scope.options);
                });
        }
    };
});