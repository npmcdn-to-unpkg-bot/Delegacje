﻿(function () {
    'use strict';

    angular
         .module('app')
         .controller('ReportController', ReportController);

    ReportController.$inject = ['userReportsFactoryService', 'dictionariesService', 'userReportsService', 'ngDialog', '$state', '$scope', '$stateParams'];

    function ReportController(userReportsFactoryService, dictionariesService, userReportsService, ngDialog, $state, $scope, $stateParams) {
        var vm = this;
        vm.visibleSection = 'Expenses';
        vm.Dictionaries = dictionariesService;
        vm.Report = undefined;
        vm.Currencies = dictionariesService.Currencies;
        
        switch ($state.current.name) {
            case 'report-new':
                var report = new userReportsFactoryService.getReport();
                vm.Report = report;
                break;
            case 'report-edit':
                userReportsService.get($stateParams.reportId).then(function (response) {
                    var reportDto = response.data;
                    
                    //map report fields
                    var report = new userReportsFactoryService.getReport();
                    report.Id = reportDto.Id;
                    report.Title = reportDto.Title;
                    report.Date = reportDto.Date;
                    report.BusinessReason = reportDto.BusinessReason;
                    report.BusinessPurpose = reportDto.BusinessPurpose;
                    report.Notes = reportDto.Notes;
                    report.UserId = reportDto.UserId;
                    

                    //map expenses
                    for (var e = 0; e < reportDto.Expenses.length; e++) {
                        var expenseDto = reportDto.Expenses[e];

                        var expense = new userReportsFactoryService.getExpense();
                        expense.ExpenseId = expenseDto.ExpenseId;
                        expense.Type = dictionariesService.ExpenseTypeById(expenseDto.ExpenseTypeId);
                        expense.Date = expenseDto.Date;
                        expense.Country = dictionariesService.CountryById(expenseDto.CountryId);
                        expense.CurrencyCode = expenseDto.CurrencyCode;
                        expense.ExchangeRate = expenseDto.ExchangeRate;
                        expense.City = expenseDto.City;
                        expense.Amount = expenseDto.Amount;
                        expense.Document = dictionariesService.ExpenseDocumentById(expenseDto.ExpenseDocumentTypeId);
                        expense.VATRate = expenseDto.VATRate;
                        expense.DoNotReturn = expenseDto.DoNotReturn;
                        expense.Notes = expenseDto.Notes;

                        report.Expenses.push(expense);
                    }

                    ///map mileages
                    for (var m = 0; m < reportDto.MileageAllowances.length; m++) {
                        var mileageDto = reportDto.MileageAllowances[m];

                        var mileage = new userReportsFactoryService.getMileage();
                       

                        report.MileageAllowances.push(mileage);
                    }

                    //map subsistences
                    //for (var s = 0; s < reportDto.Subsistences.length; s++) {
                    //    var subsistanceDto = reportDto.Subsistences[s];

                    //    //var subsistance = new userReportsFactoryService.getMileage();


                    //    //report.Subsistences.push(mileage);
                    //}


                    vm.Report = report;
                });
                break;
        }

        //dictionaries
        var countries = [];
        vm.Countries = dictionariesService;

        //expenses
        vm.NewExpense = userReportsFactoryService.getExpense();
        vm.AddExpenseToReport = function () {
            vm.NewExpense.Date = dateToString(vm.NewExpense.Date);
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
            return vm.Report !== undefined
                && vm.Report.Title !== ''
                && vm.Report.Expenses.length > 0;
        };

        vm.Mode = function () {
            return $state.current.name;
        }

        //mileages
        vm.NewMileage = userReportsFactoryService.getMileage();
        vm.AddMileageToReport = function () {
            vm.NewMileage.Date = dateToString(vm.NewMileage.Date);
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

        //subsistences
        vm.NewSubsistence = userReportsFactoryService.getSubsistence();
        vm.SubsistenceIsValid = function () {
            return vm.NewSubsistence.StartDate != null
                && vm.NewSubsistence.EndDate != null
                && vm.NewSubsistence.Country != null
                && vm.NewSubsistence.City != '';
        };
        vm.InitializeSubsistences = function () {
            vm.Report.Subsistence = new userReportsFactoryService.getSubsistence();
            vm.Report.Subsistence.StartDate = dateToString(vm.NewSubsistence.StartDate);
            vm.Report.Subsistence.EndDate = dateToString(vm.NewSubsistence.EndDate);
            vm.Report.Subsistence.Country = vm.NewSubsistence.Country;
            vm.Report.Subsistence.City = vm.NewSubsistence.City;

            var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
            var daysCount = Math.round(Math.abs((vm.NewSubsistence.StartDate.getTime() - vm.NewSubsistence.EndDate.getTime()) / (oneDay))) + 1;

            for (var i = 0; i < daysCount; i++) {
                var date = addDays(vm.NewSubsistence.StartDate, i);
                var diet = vm.Report.Subsistence.Country.SubsistenceAllowance;
                if (i === 0) {
                    //first day, calculate hours and modify diet
                }
                if (i === daysCount - 1) {
                    //last day, calculate hours and modify diet
                }

                var s = new userReportsFactoryService.getSubsistenceDay(dateToString(date), diet);
                vm.Report.Subsistence.Days.push(s);
            }
        };
        vm.SubsistenceToggleMeal = function (m) {
            m = !m;
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
                //report.Expenses[i].CurrencyCode = report.Expenses[i].Country.Currency.Code;
                report.Expenses[i].ExchangeRateModifiedByUser = false;

                report.Expenses[i].ExpenseTypeId = report.Expenses[i].Type.Id;
                report.Expenses[i].CountryId = report.Expenses[i].Country.Id;
                report.Expenses[i].ExpenseDocumentTypeId = report.Expenses[i].Document.Id;
            }

            for (var j = 0; j < report.MileageAllowances.length; j++) {
                report.MileageAllowances[j].VehicleTypeId = report.MileageAllowances[j].Type.Id;
            }

            if (report.Subsistence != null) {
                report.Subsistence.CountryId = report.Subsistence.Country.Id;
                for (var d = 0; d < report.Subsistence.Days.length; d++) {
                    report.Subsistence.Days[d].Amount = report.Subsistence.Days[d].Total();
                }
            }


            var promise;
            switch ($state.current.name) {
                case 'report-new':
                    promise = userReportsService.create(report);
                    break;
                case 'report-edit':
                    promise = userReportsService.update(report);
                    break;
            }

            promise.then(function () {
                userReportsService.reload();
                $state.go('landing');
            })
            .catch(function (err) {
                console.log(err);
            })
            .finally(function () {
                window.setTimeout(function () { creatingDialog.close(); }, 500);                
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

            var currencyCode = country.Currency.Code;
            vm.NewExpense.CurrencyCode = country.Currency.Code;

            //if (currencyCode === 'PLN') {
            //    vm.NewExpense.ExchangeRate = 1;
            //    return;
            //}

            //var currencies = dictionariesService.Currencies;
            //for (var i = 0; i < currencies.length; i++) {
            //    if (currencies[i].Code === currencyCode) {
            //        vm.NewExpense.ExchangeRate = currencies[i].ExchangeRate;
            //        return;
            //    }
            //}
        });

        $scope.$watch('vm.NewExpense.CurrencyCode', function (currencyCode) {
        	if (!currencyCode)
        		return;                	

        	if (currencyCode === 'PLN') {
        		vm.NewExpense.ExchangeRate = 1;
        		return;
        	}

        	var currencies = vm.Currencies;
        	for (var i = 0; i < currencies.length; i++) {
        		if (currencies[i].Code === currencyCode) {
        			vm.NewExpense.ExchangeRate = currencies[i].ExchangeRate;
        			return;
        		}
        	}
        });

        
        function addDays(date, days) {
            var result = new Date(date);
            result.setDate(result.getDate() + days);
            return result;
        }

        function dateToString(date) {
            var day = date.getDate();
            var month = date.getMonth() + 1;
            var year = date.getFullYear();
            return day + '/' + month + '/' + year;
        }

        
        vm.ExpenseDateChanged = function () {

        	var dateFormatted = dateToString(vm.NewExpense.Date);

        	dictionariesService.loadCurrenciesForDate(dateFormatted).
        	then(
			   function (response) {
			   	vm.Currencies = response;

			   	var currencyCode = vm.NewExpense.CurrencyCode;
			   	if (currencyCode === 'PLN') {
			   		vm.NewExpense.ExchangeRate = 1;
			   		return;
			   	}

			   	var currencies = vm.Currencies;
			   	for (var i = 0; i < currencies.length; i++) {
			   		if (currencies[i].Code === currencyCode) {
			   			vm.NewExpense.ExchangeRate = currencies[i].ExchangeRate;
			   			return;
			   		}
			   	}
			   },
			 function (response) {
			 	console.log(response);

			 });

				
        		      	
        };
    }
})();
