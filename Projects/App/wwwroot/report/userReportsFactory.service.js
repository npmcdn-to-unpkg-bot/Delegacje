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
            return bt;
        }

        function getMileage() {
            var ma = {};

            ma.Type = '';
            ma.Date = '';
            ma.Distance = '';
            ma.Notes = '';
            ma.Amount = '';// function () {
                //if (ma.Type == null || ma.Distance == null)
                  //  return '';

                //return ma.Type.Rate * ma.Distance;
           // };
            return ma;
        }

        function getExpense() {
            var exp = {};
            exp.Type = '';
            exp.Date = '';
            exp.Country = '';
            exp.City = '';
            exp.Amount = '';
            exp.Document = '';
            exp.VATRate = '';
            exp.DoNotReturn = false;
           // exp.CurrencyCode = null;
           // exp.ExchangeRate = null;
            exp.Notes = '';
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