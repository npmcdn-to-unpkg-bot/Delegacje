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
            get: get,
            create: create,
            remove: remove,
            copy: copy,
            update: update,
            print: print
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

        function get(Id) {
            var promise = $http
                .get('../api/businessTrips/' + Id)
                .then(function (response) {
                    console.log(response);
                    return response;
                });

            return promise;
        }

        function create(report) {
            var promise = $http
                .put('../api/businessTrips/create', report)
                .then(function (response) {
                    console.log('Report created');
                });

            return promise;
        }

        function update(report) {
            var promise = $http
                .post('../api/businessTrips/update', report)
                .then(function (response) {
                    console.log('Report updated');
                });

            return promise;
        }

        function copy(Id) {
            var promise = $http
                .get('../api/businessTrips/clone/' + Id)
                .then(function (response) {
                    return response;
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

        function print(Id) {
            window.location = '../api/pdf/' + Id;
        }
    }
})();
