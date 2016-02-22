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
                    total += bt.MileageAllowances[m].Amount();
                }
                if (bt.Subsistence != null) {
                    for (var s = 0; s < bt.Subsistence.Days.length; s++) {
                        total += bt.Subsistence.Days[s].Total();
                    }
                }
                return total;
            };
            return bt;
        }

        function getMileage() {
            var ma = {};

            ma.Type = null;
            ma.Date = '';
            ma.Distance = '';
            ma.Notes = '';
            ma.Amount = function () {
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
            exp.ExchangeRate = 1;
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

        function getSubsistenceDay(date, diet) {
            var s = {};
            s.Date = date;
            s.Breakfast = false;
            s.Dinner = false;
            s.Supper = false;
            s.Night = false;
            s.Total = function () {
                var total = diet;
                if (s.Breakfast) {
                    total -= 0.15 * diet;
                }
                if (s.Dinner) {
                    total -= 0.3 * diet;
                }
                if (s.Supper) {
                    total -= 0.3 * diet;
                }
                return total;
            };
            return s;
        }

        function formatDate(date) {
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