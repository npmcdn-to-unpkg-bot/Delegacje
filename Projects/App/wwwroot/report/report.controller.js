(function () {
    'use strict';

    angular
         .module('app')
         .controller('ReportController', ReportController);

    ReportController.$inject = ['userReportsFactoryService'];

    function ReportController(userReportsFactoryService) {
        var vm = this;
        vm.visibleSection = 'Expenses';
        vm.Report = userReportsFactoryService.getReport();

        //expenses
        vm.NewExpense = userReportsFactoryService.getExpense();
        vm.AddExpenseToReport = function () {
            vm.Report.Expenses.push(vm.NewExpense);
            vm.NewExpense = userReportsFactoryService.getExpense();
        };
        vm.RemoveExpense = function (expense) {
            for (var i = 0; i < vm.Report.Expenses.length; i++) {
                if (vm.Report.Expenses[i] == expense) {
                    vm.Report.Expenses.splice(i, 1);
                    break;
                }
            }
        };

        //ui switches
        vm.toggleSection = function (section) {
            vm.visibleSection = section;
        }
    }
})();
