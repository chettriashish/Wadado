angular.module('psWebMetricsService', []).factory('psWebMetricsService',
    function ($rootScope) {
        var hub = $.connection.metricHub;
        hub.client.broadcastMessage = function (time, bandwidthPct, cpuPct, salesAmt, alphaSalesAmt, betaSalesAmt) {
            $rootScope.$broadcast('psWebMetricsService-received-data-event',
                {
                    'time': time,
                    'bandwidthPct': bandwidthPct,
                    'cpuPct': cpuPct,
                    'salesAmt': salesAmt,
                    'alphaSalesAmt': alphaSalesAmt,
                    'betaSalesAmt': betaSalesAmt,
                });
        }

        $.connection.hub.start()
        .done()
        .fail(function (data) {
            alert(data);
        });

        return {

        };
    });