(function () {
    'use strict';

    angular
        .module('app')
        .controller('RegisterController', RegisterController);

    RegisterController.$inject = ['appSettings', 'authenticationService', '$state', '$http'];

    /* @ngInject */
    function RegisterController(appSettings, authenticationService, $state, $http) {
        var vm = this;
        vm.isBusy = false;
        vm.registerModel = {
            email: '',
            password: '',
            confirmPassword: ''
        };
        vm.passwordsMatch = function () {
            if (vm.registerModel.password == '' || vm.registerModel.confirmPassword == '')
                return true;

            return vm.registerModel.password === vm.registerModel.confirmPassword;
        }

        ////////////////

        /**
         * Invoked when the user clicks the 'Register' button
         */
        vm.register = function () {
            vm.isBusy = true;
            $http.post(appSettings.baseUrl + '/api/account/register', vm.registerModel)
				.success(function (data, status, headers, config) {
				    authenticationService.authenticate(vm.registerModel.email, vm.registerModel.password).then(function () {
				        $state.go('landing');
				    });
				})
				.error(function (data, status, headers, config) {
				    //toastr.alertModelState(data, status);
				})
                .finally(function () {
                    vm.isBusy = false;
                    vm.registerModel.email = '';
                    vm.registerModel.password = '';
                    vm.registerModel.passwordConfirm = '';
                });
        }
    }

})();
