(function () {
    'use strict';

    angular
        .module('app')
        .factory('userReportsFactoryService', userReportsFactoryService);

    userReportsFactoryService.$inject = ['$http'];

    function userReportsFactoryService($http) {
        var service = {
            getReport: getReport,
            getExpense: getExpense,
            getMileage: getMileage,
            getSubsistence: getSubsistence,
            getSubsistenceDay: getSubsistenceDay
        };
        return service;

        function getReport() {
            var bt = {};
            bt.Id = null;
            bt.Title = '';
            bt.Date = formatDate(new Date());
            bt.BusinessReason = '';
            bt.BusinessPurpose = '';
            bt.Notes = '';
            bt.UserId = null;
            bt.Expenses = [];
            bt.MileageAllowances = [];
            bt.Subsistence = null;
            bt.Total = function () {
                var total = 0;
                for (var e = 0; e < bt.Expenses.length; e++) {
                    total += parseFloat(bt.Expenses[e].FinalAmount());
                }
                for (var m = 0; m < bt.MileageAllowances.length; m++) {
                    total += bt.MileageAllowances[m].CalculateAmount();
                }
                if (bt.Subsistence != null) {
                    for (var s = 0; s < bt.Subsistence.Days.length; s++) {
                        total += bt.Subsistence.Days[s].TotalPLN();
                    }
                }
                return total;
            };
            bt.TotalToReturn = function () {
                var total = 0;
                for (var e = 0; e < bt.Expenses.length; e++) {
                    if (!bt.Expenses[e].DoNotReturn) {
                        total += parseFloat(bt.Expenses[e].FinalAmount());
                    }
                }
                for (var m = 0; m < bt.MileageAllowances.length; m++) {
                    total += bt.MileageAllowances[m].CalculateAmount();
                }
                if (bt.Subsistence != null) {
                    for (var s = 0; s < bt.Subsistence.Days.length; s++) {
                        total += bt.Subsistence.Days[s].TotalPLN();
                    }
                }
                return total;
            };
            return bt;
        }

        function getMileage() {
            var ma = {};

            ma.Type = null;
            ma.Date= '';
            ma.Distance = '';
            ma.Notes = '';
            ma.CalculateAmount = function () {
                if (ma.Type === null || ma.Distance === '')
                    return '';

                return ma.Type.Rate * ma.Distance;
            };

            return ma;
        }

        function getExpense() {
            var exp = {};
            exp.ExpenseId = null;
            exp.Type = null;
            exp.Date = '';
            exp.Country = null;
            exp.CurrencyCode = null;
            exp.ExchangeRate = null;
            exp.City = '';
            exp.Amount = '';
            exp.Document = null;
            exp.VATRate = '';
            exp.DoNotReturn = false;
            exp.Notes = '';
            exp.FinalAmount = function () {
                if (exp.Amount === '' || exp.Country === null)
                    return '';

                return exp.Amount * exp.ExchangeRate;
            };
            return exp;
        }

        function getSubsistence() {
            return {
                StartDate: null,
                EndDate: null,
                Country: null,
                City: '',
                Days: []
            };
        }

        function getSubsistenceDay(date, diet, dietFull, accomodation, exchangeRate, isForeign) {
            var s = {};
            s.ExchangeRate = exchangeRate;
            s.Date = date;
            s.Diet = diet;
            s.IsForeign = isForeign
            s.Breakfast = false;
            s.Dinner = false;
            s.Supper = false;
            s.Night = false;
            s.Total = function () {
                var total = diet;
                if (s.Breakfast) {
                    total -= (isForeign ? 0.15 : 0.25) * dietFull;
                }
                if (s.Dinner) {
                    total -= (isForeign ? 0.3 : 0.5) * dietFull;
                }
                if (s.Supper) {
                    total -= (isForeign ? 0.3 : 0.25) * dietFull;
                }
                if (s.Night) {
                    total += (isForeign ? 0.25 : 1) * accomodation;
                }

                if (total < 0)
                    total = 0;

                return total;
            };
            s.TotalPLN = function () {
                return s.Total() * exchangeRate;
            }
            return s;
        }

        function formatDate(date) {
            if (date == null || date == '')
                return '';

            var day = addLeadingZero(date.getDate());
            var month = addLeadingZero(date.getMonth() + 1);
            var year = date.getFullYear();
            return day + '/' + month + '/' + year;

            function addLeadingZero(num) {
                return ('0' + num).slice(-2);
            }
        }
    }
})();