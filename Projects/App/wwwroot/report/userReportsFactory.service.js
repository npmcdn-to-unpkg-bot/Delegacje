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
            getMileage: getMileage
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
            bt.Subsistences = null;
            bt.Total = function () {
                var total = 0;
                for (var e = 0; e < bt.Expenses.length; e++) {
                    total += parseFloat(bt.Expenses[e].FinalAmount());
                }
                for (var m = 0; m < bt.MileageAllowances.length; m++) {
                    total += bt.MileageAllowances[m].Amount();
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

                return (exp.Amount * exp.ExchangeRate).toFixed(2);
            };
            return exp;
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