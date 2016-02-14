(function () {
	'use strict';

	angular
        .module('app')
        .controller('RegisterController', RegisterController);

	RegisterController.$inject = ['appSettings', 'authenticationService', '$location', '$http'];

	/* @ngInject */
	function RegisterController(appSettings, authenticationService, $location, $http) {
		var vm = this;
		vm.isBusy = false;
		vm.title = 'RegisterController';
		vm.registerModel = {};
		vm.registerModel.email = '';
		vm.registerModel.password = '';
		vm.registerModel.passwordConfirm = '';		

		////////////////

		/**
         * Invoked when the user clicks the 'Register' button
         */
		vm.register = function() {
			vm.isBusy = true;
			$http.post('../api/account/register', vm.registerModel)
				   .success(function (data, status, headers, config) {
				   	vm.registerModel.email = '';
				   	vm.registerModel.password = '';
				   	vm.registerModel.passwordConfirm = '';

				   	//toastr.success(data);

				   	$location.url('/');
				   })
				   .error(function (data, status, headers, config) {
				   	//toastr.alertModelState(data, status);
				   });
		}
	}

})();
