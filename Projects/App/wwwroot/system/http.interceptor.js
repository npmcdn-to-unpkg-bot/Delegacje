(function () {
    'use strict';

    angular
        .module('app')
        .factory('httpInterceptor', httpInterceptor);

    httpInterceptor.$inject = ['$q', '$injector', '$location'];

    /* @ngInject */
    function httpInterceptor($q, $injector, $location) {
        var service = {
            request: request,
            responseError: responseError
        };
        return service;

        ////////////////

        /**
         * Shows a toast for failed http requests
         * @param {Object} response HTTP response object
         * @return {*|Promise}
         */
        function responseError(response) {
            // Redirect to login page on a 401. 403 errors are allowed
            // through by design.
            if (response.status === 401) {
                $location.path('/login');
            }
            else {
                //// Circular dependency if inject |toastr| directly
                var msg;
                var toastr = $injector.get('toastr');
                var data = response.data;

                if (data) {
                    console.log(response);
                    msg = data['error_description'] || data.error ||
                        data.ExceptionMessage || data.Message ||
                        data.message;
                } else {
                    msg = response.statusText || response.statusCode;
                }

                toastr.error(msg);
            }
            return $q.reject(response);
        }

        /**
         * Adds the bearer token to the header for each request, if the user is
         * authenticated
         * @param {Object} config
         */
        function request(config) {
            config.headers['Content-Type'] = 'application/json';
            // Circular dependency if inject |authenticationService| directly
            var authenticationService = $injector.get('authenticationService');
            if (authenticationService.isAuthenticated()) {
                config.headers['Authorization'] = 'Bearer ' + authenticationService.token();
            }

            return config;
        }
    }

})();

