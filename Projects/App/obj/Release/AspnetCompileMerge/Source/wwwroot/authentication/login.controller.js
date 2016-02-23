(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['appSettings', 'authenticationService', '$location'];

    /* @ngInject */
    function LoginController(appSettings, authenticationService, $location) {
        var vm = this;
        vm.isBusy = false;
        vm.title = 'LoginController';
        vm.userName = '';
        vm.password = '';
        vm.tenant = appSettings.tenant;
        vm.login = login;

        ////////////////

        /**
         * Invoked when the user clicks the 'Sign In' button
         */
        function login() {
            vm.isBusy = true;
            authenticationService.authenticate(vm.userName, vm.password)
                .then(function(user) {
                    $location.path('/');
                })
                .finally(function() {
                    vm.isBusy = false;
                });
        }
    }

})();
