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
            reload: reload,
            create: create,
            remove: remove,
            copy: copy
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

        function create(report) {
            var promise = $http
                .put('../api/businessTrips/create', report)
                .then(function (response) {
                    console.log('Report creted');
                });

            return promise;
        }

        function copy(report) {
            var promise = $http
                .put('../api/businessTrips/create', report)
                .then(function (response) {
                    console.log('Report creted');
                });

            return promise;
        }

        function remove(Id) {
            var promise = $http
                .delete('../api/businessTrips/' + Id)
                .then(function (response) {
                    for (var i = 0; i < reports.length; i++) {
                        if (reports[i].Id == Id) {
                            reports.splice(i, 1);
                        };
                    }

                    console.log('Report removed');
                });

            return promise;
        }
    }
})();
