(function () {
    'use strict';

    angular
         .module('app')
         .controller('ReportController', ReportController);

    ReportController.$inject = ['userReportsFactoryService', 'dictionariesService'];

    function ReportController(userReportsFactoryService, dictionariesService) {
        var vm = this;
        vm.visibleSection = 'Expenses';
        vm.Dictionaries = dictionariesService;
        vm.Report = userReportsFactoryService.getReport();

        vm.ddSelectOptions = [
        {
            text: 'Option1',
            value: 'a value'
        },
        {
            text: 'Option2',
            value: 'another value',
            someprop: 'somevalue'
        },
        {
            // Any option with divider set to true will be a divider
            // in the menu and cannot be selected.
            divider: true
        },
        {
            // Any divider option with a 'text' property will
            // behave similarly to a divider and cannot be selected.
            divider: true,
            text: 'divider label'
        },
        {
            // Example of an option with the 'href' property
            text: 'Option4',
            href: '#option4'
        }
        ];


        //dropdown values
        vm.SelectedCountry = '-';

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
