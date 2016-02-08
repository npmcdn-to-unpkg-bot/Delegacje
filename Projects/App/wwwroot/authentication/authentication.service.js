(function () {
    'use strict';

    angular
        .module('app')
        .factory('authenticationService', authenticationService);

    authenticationService.$inject = ['appSettings', '$http', '$localStorage'];

    /* @ngInject */
    function authenticationService(appSettings, $http, $localStorage,
                                   $sessionStorage) {
        var service = {
            token: getToken,
            authenticate: authenticate,
            isAuthenticated: isAuthenticated,
            logout: logout
        };
        return service;

        ////////////////

        /**
         * Gets the current authentication token from local/session storage
         * @return {string} The current token or undefined if not found
         */
        function getToken() {
            return $localStorage[appSettings.tokenName];
        }

        /**
         * Saves the token to session storage when using a public
         * computer, local storage for a private computer
         * @param {string} token
         * @param {boolean} publicComputer true if the user is using a public computer,
         *        false for a private computer.
         */
        function saveToken(token) {
            clearToken();
            $localStorage[appSettings.tokenName] = token;
        }

        /**
         * Deletes the authentication token from local/session storage
         */
        function clearToken() {
            delete $localStorage[appSettings.tokenName];
        }

        /**
         * Returns whether or not the user is authenticated
         * @return {boolean} true if the user is authenticated, otherwise false
         */
        function isAuthenticated() {
            return angular.isDefined(getToken());
        }

        /**
         * Authenticates the given user using the supplied username and password
         *
         * @param {string} userName Username
         * @param {string} password User's password
         * @param {boolean} publicComputer truthy if the user is logging in from
         *                  a public computer
         * @returns {*|Promise}
         */
        function authenticate(userName, password) {
            logout();

            var request = {
                method: 'POST',
                data: 'grant_type=password&username=' + encodeURIComponent(userName) +
                '&password=' + encodeURIComponent(password),
                url: appSettings.apiUrl.replace('api/', 'token'),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            };

            return $http(request)
                .then(function(response) {
                    var user = response.data;
                    saveToken(user['access_token']);
                });
        }

        /**
         * Logs the current user out by clearing the access token
         */
        function logout() {
            clearToken();
        }
    }

})();

