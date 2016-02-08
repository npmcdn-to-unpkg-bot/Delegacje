(function () {
    'use strict';

    angular
         .module('app')
         .controller('ReportController', ReportController);

    ReportController.$inject = [];

    function ReportController() {
        var vm = this;
        vm.visibleSection = 'Expenses';

        vm.toggleSection = function (section) {
            vm.visibleSection = section;
        }
    }
})();
