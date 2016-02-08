(function () {
    'use strict';

    angular
        .module('app')
        .factory('userReportsService', userReportsService);

    userReportsService.$inject = ['$http'];

    /* @ngInject */
    function userReportsService($http) {
        var reports = null;

        var service = {
            reports: function () { return reports; },
            reload: reload
        };
        return service;

        function reload() {
            reports = null;

            return $http
                .get('../api/businessTrips')
                .then(function (response) {
                    console.log(response);
                    reports = response.data;
                });
        }

        reload();
    }
})();
