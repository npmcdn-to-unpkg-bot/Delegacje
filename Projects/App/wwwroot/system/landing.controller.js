(function () {
    'use strict';

    angular
         .module('app')
         .controller('LandingController', LandingController);

    LandingController.$inject = ['userReportsService'];

    function LandingController(userReportsService) {
        var vm = this;
        vm.reports = userReportsService.reports;
        vm.remove = function (report) {
            userReportsService.remove(report.Id);
        }
    }
})();
