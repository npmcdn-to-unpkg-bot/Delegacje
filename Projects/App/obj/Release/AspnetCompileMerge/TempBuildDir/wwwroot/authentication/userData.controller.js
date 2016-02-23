(function () {
	'use strict';

	angular
        .module('app')
        .controller('UserDataController', UserDataController);

	UserDataController.$inject = ['authenticationService', '$state'];

	function UserDataController(authenticationService, $state) {
		var vm = this;
		vm.user = authenticationService.user();
		vm.logout = function() {
		    authenticationService.logout();
		    $state.go('login');
		}
	}

})();
