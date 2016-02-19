(function () {
    'use strict';

    angular
         .module('app')
         .controller('LandingController', LandingController);

    LandingController.$inject = ['userReportsService', 'ngDialog', 'authenticationService', '$state'];

    function LandingController(userReportsService, ngDialog, authenticationService, $state) {
        var vm = this;
        vm.reports = userReportsService.reports;
        vm.remove = function (report) {
            var warningDialog = ngDialog.open({
                template: 'wwwroot/report/popups/remove.warning.template.html'
            });

            warningDialog.closePromise.then(function (data) {
                if (data.value === true) {
                    report.isRemoving = true;
                    userReportsService.remove(report.Id);
                }
            });            
        }
        vm.copy = function (Id) {
            userReportsService.copy(Id).then(function (result) {
                vm.reports().push(result.data);
            });
        }

        userReportsService.reload();

        if (!authenticationService.isAuthenticated())
            $state.go('login');
    }
})();
