(function () {
    'use strict';

    angular
        .module('app')
        .factory('userReportsFactoryService', userReportsFactoryService);

    userReportsFactoryService.$inject = ['$http'];

    function userReportsFactoryService($http) {
        var service = {
            getReport: getReport,
            getExpense: getExpense
        };
        return service;

        function getReport() {
            var bt = {};
            bt.Id = null;
            bt.Title = 'Title';
            bt.Date = formatDate(new Date());
            bt.BusinessReason = 'BusinessReason';
            bt.BusinessPurpose = 'BusinessPurpose';
            bt.Notes = 'Notes';
            bt.UserId = null;
            bt.Expenses = [];
            bt.MileageAllowance = getMileageAllowance();
            bt.Subsistences = null;
            return bt;
        }

        function getMileageAllowance() {
            var ma = {};

            ma.Type = 'Type';
            ma.Date = 'Date';
            ma.Distance = 'Distance';
            ma.Notes = 'Mileage Notes';
            ma.Amount = 'Amount';// function () {
                //if (ma.Type == null || ma.Distance == null)
                  //  return '';

                //return ma.Type.Rate * ma.Distance;
           // };
            return ma;
        }

        function getExpense() {
            var exp = {};
            exp.Type = 'Type';
            exp.Date = 'Date';
            exp.Country = 'Country';
            exp.City = 'City';
            exp.Amount = 'Amount';
            exp.Document = 'Document';
            exp.VATRate = 'VATRate';
            exp.DoNotReturn = 'DoNotReturn'
           // exp.CurrencyCode = null;
           // exp.ExchangeRate = null;
            exp.Notes = 'Expense Notes';
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