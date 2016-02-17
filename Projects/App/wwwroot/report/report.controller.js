(function () {
    'use strict';

    angular
         .module('app')
         .controller('ReportController', ReportController);

    ReportController.$inject = ['userReportsFactoryService', 'dictionariesService', 'userReportsService', 'ngDialog', '$state', '$scope'];

    function ReportController(userReportsFactoryService, dictionariesService, userReportsService, ngDialog, $state, $scope) {
        var vm = this;
        vm.visibleSection = 'Expenses';
        vm.Dictionaries = dictionariesService;
        vm.Report = userReportsFactoryService.getReport();

        //dictionaries
        var countries = [];
        vm.Countries = dictionariesService;

        //expenses
        vm.NewExpense = userReportsFactoryService.getExpense();
        vm.AddExpenseToReport = function () {
            vm.Report.Expenses.push(vm.NewExpense);
            vm.NewExpense = userReportsFactoryService.getExpense();
        };
        vm.RemoveExpense = function (expense) {
            for (var i = 0; i < vm.Report.Expenses.length; i++) {
                if (vm.Report.Expenses[i] === expense) {
                    vm.Report.Expenses.splice(i, 1);
                    break;
                }
            }
        };
        vm.ExpenseIsValid = function () {
            return vm.NewExpense.Type !== null
            && vm.NewExpense.Date !== ''
            && vm.NewExpense.Country !== null
            && vm.NewExpense.City !== ''
            && vm.NewExpense.Amount !== ''
            && vm.NewExpense.Document !== null;
        };
        vm.MileageIsValid = function () {
            return vm.NewMileage.Type !== null
            && vm.NewMileage.Date !== ''
            && vm.NewMileage.Distance !== '';
        };
        vm.ReportIsValid = function () {
            return vm.Report.Title !== ''
            && vm.Report.Expenses.length > 0;
        };

        //mileages
        vm.NewMileage = userReportsFactoryService.getMileage();
        vm.AddMileageToReport = function () {
            vm.Report.MileageAllowances.push(vm.NewMileage);
            vm.NewMileage = userReportsFactoryService.getMileage();
        };
        vm.RemoveMileage = function (mileage) {
            for (var i = 0; i < vm.Report.MileageAllowances.length; i++) {
                if (vm.Report.MileageAllowances[i] === mileage) {
                    vm.Report.MileageAllowances.splice(i, 1);
                    break;
                }
            }
        };

        vm.Save = function () {
            var creatingDialog = ngDialog.open({
                template: 'wwwroot/report/popups/creating.template.html',
                closeByEscape: false,
                closeByDocument: false,
                showClose: false,
                closeByNavigation: false
            });

            //prepare data
            var report = vm.Report;

            for (var i = 0; i < report.Expenses.length; i++) {
                report.Expenses[i].CurrencyCode = report.Expenses[i].Country.CurrencyCode;
                report.Expenses[i].ExchangeRateModifiedByUser = false;

                report.Expenses[i].ExpenseTypeId = report.Expenses[i].Type.Id;
                //report.Expenses[i].Type = undefined;

                report.Expenses[i].CountryId = report.Expenses[i].Country.Id;
                //report.Expenses[i].Country = undefined;
                //report.Expenses[i].FinalAmount = undefined;
            }

            for (var j = 0; j < report.MileageAllowances.length; j++) {
                report.MileageAllowances[j].VehicleTypeId = report.MileageAllowances[j].Type.Id;
                report.MileageAllowances[j].Type = undefined;
            }

            var promise = userReportsService.create(report);
            promise.then(function () {
                userReportsService.reload();
                $state.go('landing');
            })
            .catch(function (err) {
                console.log(err);
            })
            .finally(function () {
                creatingDialog.close();
            });
        };

        //ui switches
        vm.toggleSection = function (section) {
            vm.visibleSection = section;
        };

        //watches
        $scope.$watch('vm.NewExpense.Country', function (country) {
            if (!country)
                return;

            var currencyCode = country.CurrencyCode;

            if (currencyCode === 'PLN') {
                vm.NewExpense.ExchangeRate = 1;
                return;
            }

            var currencies = dictionariesService.Currencies;
            for (var i = 0; i < currencies.length; i++) {
                if (currencies[i].Code === currencyCode) {
                    vm.NewExpense.ExchangeRate = currencies[i].ExchangeRate;
                    return;
                }
            }
        });
    }
})();
