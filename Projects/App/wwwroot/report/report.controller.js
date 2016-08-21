(function () {
    'use strict';

    angular
         .module('app')
         .controller('ReportController', ReportController);

    ReportController.$inject = ['userReportsFactoryService', 'dictionariesService', 'userReportsService', 'ngDialog', '$state', '$scope', '$stateParams', 'currenciesService'];

    function ReportController(userReportsFactoryService, dictionariesService, userReportsService, ngDialog, $state, $scope, $stateParams, currenciesService) {
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
                        mileage.id = mileageDto.id;
                        mileage.Type = dictionariesService.VehicleTypeById(mileageDto.VehicleTypeId);;
                        mileage.Date= mileageDto.Date;
                        mileage.Distance = mileageDto.Distance;
                        mileage.Notes = mileageDto.Notes;

                        report.MileageAllowances.push(mileage);
                    }

                    //map subsistences
                    if (reportDto.Subsistence != undefined) {
                        var subDto = reportDto.Subsistence;

                        var subsistance = new userReportsFactoryService.getSubsistence();
                        subsistance.Id = subDto.Id;
                        subsistance.StartDate = stringToDate(subDto.StartDate);
                        subsistance.EndDate = stringToDate(subDto.EndDate);
                        subsistance.Country = dictionariesService.CountryById(subDto.CountryId);
                        subsistance.City = subDto.City;

                        vm.NewSubsistence.StartDate = subDto.StartDate;
                        vm.NewSubsistence.EndDate = subDto.EndDate;
                        vm.NewSubsistence.Country = subsistance.Country;
                        vm.NewSubsistence.City = subsistance.City;

                        for (var s = 0; s < subDto.Days.length; s++) {
                            var day = new userReportsFactoryService.getSubsistenceDay(
                                subDto.Days[s].Date,
                                subDto.Days[s].Diet,
                                subsistance.Country.SubsistenceAllowance,
                                subsistance.Country.AccomodationLimit,
                                subDto.Days[s].ExchangeRate,
                                subDto.Days[s].IsForeign);

                            day.Breakfast = subDto.Days[s].Breakfast;
                            day.Dinner = subDto.Days[s].Dinner;
                            day.Supper = subDto.Days[s].Supper;
                            day.Night = subDto.Days[s].Night;

                            subsistance.Days.push(day);
                        }

                        report.Subsistence = subsistance;
                    }

                    vm.Report = report;
                });
                break;
        }

        //dictionaries
        var countries = [];
        vm.Countries = dictionariesService;

        //expenses
        var clickedExpense = null;
        vm.maxDate = function () {
            return dateToString(new Date());
        };
        vm.isEditingExpense = false;
        vm.expensePopupVisible = false;
        vm.expenseShowPopup = function (event, epxense) {
            clickedExpense = epxense;
            var targetTag = angular.element(document.querySelector('#expense-table-menu'));
            targetTag.css({
                position: "fixed",
                display: "block",
                left: event.clientX - 290 + 'px',
                top: event.clientY + 10 + 'px'
            });

            vm.expensePopupVisible = true;
        };
        vm.expenseHidePopup = function () {
            vm.expensePopupVisible = false;
        };

        vm.NewExpense = userReportsFactoryService.getExpense();
        vm.AddExpenseToReport = function () {
            vm.NewExpense.Date = vm.NewExpense.Date.substr(0, 10);
            vm.Report.Expenses.push(vm.NewExpense);
            vm.NewExpense = userReportsFactoryService.getExpense();
        };
        vm.editExpense = function () {
            vm.isEditingExpense = true;
            vm.NewExpense = clickedExpense;
        };
        vm.stopEditExpense = function () {
            vm.NewExpense = new userReportsFactoryService.getExpense();
            vm.isEditingExpense = false;
        };
        vm.removeExpense = function () {
            for (var i = 0; i < vm.Report.Expenses.length; i++) {
                if (vm.Report.Expenses[i] === clickedExpense) {
                    vm.Report.Expenses.splice(i, 1);
                    break;
                }
            }
            clickedExpense = null;
        };

        //validation
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
        vm.SubsistenceIsValid = function () {
            return vm.NewSubsistence.StartDate != null
                && vm.NewSubsistence.EndDate != null
                && vm.NewSubsistence.Country != null
                && vm.NewSubsistence.City != '';
        };
        vm.ReportIsValid = function () {
            return vm.Report !== undefined
                && vm.Report.Title !== ''
                &&
                (vm.Report.Expenses.length > 0 ||
                 vm.Report.MileageAllowances.length > 0 ||
                 vm.Report.Subsistence != null);
        };

        vm.Mode = function () {
            return $state.current.name;
        }

        //mileages
        var clickedMileage = null;
        vm.isEditingMileage = false;
        vm.mileagePopupVisible = false;
        vm.mileageShowPopup = function (event, mileage) {
            clickedMileage = mileage;
            var targetTag = angular.element(document.querySelector('#mileage-table-menu'));
            targetTag.css({
                position: "fixed",
                display: "block",
                left: event.clientX - 290 + 'px',
                top: event.clientY + 10 + 'px'
            });

            vm.mileagePopupVisible = true;
        };
        vm.mileageHidePopup = function () {
            vm.mileagePopupVisible = false;
        };

        vm.NewMileage = new userReportsFactoryService.getMileage();
        vm.AddMileageToReport = function () {
            vm.Report.MileageAllowances.push(vm.NewMileage);
            vm.NewMileage = userReportsFactoryService.getMileage();
        };
        vm.editMileage = function () {
            vm.isEditingMileage = true;
            vm.NewMileage = clickedMileage;
        };
        vm.stopEditMileage = function () {
            vm.NewMileage = new userReportsFactoryService.getMileage();
            vm.isEditingMileage = false;
        };
        vm.removeMileage = function () {
            for (var i = 0; i < vm.Report.MileageAllowances.length; i++) {
                if (vm.Report.MileageAllowances[i] === clickedMileage) {
                    vm.Report.MileageAllowances.splice(i, 1);
                    break;
                }
            }
            clickedMileage = null;
        };

        //subsistences
        vm.NewSubsistence = userReportsFactoryService.getSubsistence();
        vm.GettingSubsistenceExchageRate = false;
        vm.SubsistenceCurrencyData = null;
        vm.InitializeSubsistences = function () {
            vm.GettingSubsistenceExchageRate = true;
            var currencyCode = vm.NewSubsistence.Country.LimitCurrency.Code;
            var sDate = stringToDate(vm.NewSubsistence.StartDate);
            sDate = addDays(sDate, -1);

            currenciesService.getExchangeRate(currencyCode, sDate)
            .then(function (data) {
                vm.SubsistenceCurrencyData = data;

                vm.Report.Subsistence = new userReportsFactoryService.getSubsistence();
                vm.Report.Subsistence.StartDate = vm.NewSubsistence.StartDate;
                vm.Report.Subsistence.EndDate = vm.NewSubsistence.EndDate;
                vm.Report.Subsistence.Country = vm.NewSubsistence.Country;
                vm.Report.Subsistence.City = vm.NewSubsistence.City;

                var startDateObj = stringToDate(vm.NewSubsistence.StartDate);
                var endDateObj = stringToDate(vm.NewSubsistence.EndDate);

                var hourDiff = endDateObj - startDateObj; //in ms
                var minDiff = hourDiff / 60 / 1000; //in minutes
                var hDiff = hourDiff / 3600 / 1000; //in hours

                for (var i = 0; i < hDiff + 24; i += 24) {
                    var date = addDays(startDateObj, i / 24);
                    var diet = vm.Report.Subsistence.Country.SubsistenceAllowance;
                    var dietFull = diet;
                    var accLimit = vm.Report.Subsistence.Country.AccomodationLimit;

                    
                    var hoursLeft = hDiff - i;
                    var isForeign = false;
                    
                    if (hoursLeft > 0) {
                        //krajowa, krócej niż dobę
                        if (vm.NewSubsistence.Country.Currency.Code == 'PLN' && hDiff < 24) {
                            if (hoursLeft <= 12 && hoursLeft > 8)
                                diet = diet * 0.5;
                            if (hoursLeft <= 8)
                                diet = 0;
                        }
                        //krajowa, dłużej niż dobę
                        else if (vm.NewSubsistence.Country.Currency.Code == 'PLN' && hDiff >= 24) {
                            if (hoursLeft <= 8)
                                diet = diet * 0.5;
                        }
                        //zagraniczna
                        else {
                            isForeign = true;
                            if (hoursLeft <= 12 && hoursLeft > 8)
                                diet = diet * 0.5;
                            if (hoursLeft <= 8)
                                diet = diet * 0.3;
                        }

                        if (diet > 0) {
                            var s = new userReportsFactoryService.getSubsistenceDay(dateToString(date), diet, dietFull, accLimit, vm.SubsistenceCurrencyData.ExchangeRate, isForeign);
                            vm.Report.Subsistence.Days.push(s);
                        }
                    }
                }

                vm.GettingSubsistenceExchageRate = false;
            });            
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
                report.Expenses[i].ExchangeRateModifiedByUser = false;
                report.Expenses[i].AmountPLN = report.Expenses[i].FinalAmount();
                report.Expenses[i].ExpenseTypeId = report.Expenses[i].Type.Id;
                report.Expenses[i].CountryId = report.Expenses[i].Country.Id;
                report.Expenses[i].ExpenseDocumentTypeId = report.Expenses[i].Document.Id;
            }

            for (var j = 0; j < report.MileageAllowances.length; j++) {
                report.MileageAllowances[j].VehicleTypeId = report.MileageAllowances[j].Type.Id;
                report.MileageAllowances[j].Amount = report.MileageAllowances[j].CalculateAmount();
            }

            if (report.Subsistence != null) {
                report.Subsistence.CountryId = report.Subsistence.Country.Id;
                for (var d = 0; d < report.Subsistence.Days.length; d++) {
                    report.Subsistence.Days[d].Amount = report.Subsistence.Days[d].Total();
                    report.Subsistence.Days[d].AmountPLN = report.Subsistence.Days[d].TotalPLN();
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
        });

        $scope.$watch('vm.NewExpense.CurrencyCode', function (currencyCode) {
        	if (!currencyCode)
        		return;                	        	

        	var currencies = vm.Currencies;
        	for (var i = 0; i < currencies.length; i++) {
        		if (currencies[i].Code === currencyCode) {
        			vm.NewExpense.ExchangeRate = currencies[i].ExchangeRate;
        			return;
        		}
        	}
        });

        $scope.$watch('vm.NewExpense.Date', function () {
            ExpenseDateChanged();
        });
        
        function addDays(date, days) {
            var result = new Date(date);
            result.setDate(result.getDate() + days);
            return result;
        }

        function dateToString(date) {
            if (date == '')
                return '';

            var day = date.getDate();
            var month = date.getMonth() + 1;
            var year = date.getFullYear();
            return day + '/' + month + '/' + year;
        }

        function stringToDate(date) {
            var parts = date.split(' ');

            if (parts.length == 2) {
                var dParts = parts[0].split('/');
                var tParts = parts[1].split(':');
                return new Date(dParts[2], dParts[1] - 1, dParts[0], tParts[0], tParts[1]);
            }
            else {
                var dParts = parts[0].split('/');
                return new Date(dParts[2], dParts[1] - 1, dParts[0]);
            }
        };

        function dateToUrlString(date) {
            return date.split('/').join('-').substr(0, 10);
        }

        function countDays(date1, date2) {
            var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
            var d1 = new Date(date1.getTime());
            d1.setHours(0, 0, 0, 0);

            var d2 = new Date(date2.getTime());
            d2.setHours(0, 0, 0, 0);

            var daysCount = Math.round(Math.abs((d1.getTime() - d2.getTime()) / (oneDay))) + 1;
            return daysCount;
        }

        
        function ExpenseDateChanged() {
            if (vm.NewExpense.Date == '')
                return;

            dictionariesService.loadCurrenciesForDate(vm.NewExpense.Date).
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
