(function () {
    'use strict';

    angular
        .module('app')
        .factory('dictionariesService', dictionariesService);

    dictionariesService.$inject = ['$localStorage', '$http'];

    /* @ngInject */
    function dictionariesService($localStorage, $http) {

        var service = {
            Countries: $localStorage['Countries'],
            VehicleTypes: $localStorage['VehicleTypes'],
            ExpenseTypes: $localStorage['ExpenseTypes'],
            ExpenseDocumentTypes: $localStorage['ExpenseDocumentTypes'],
            Currencies: $localStorage['Currencies'],

            ExpenseTypeById: function(Id) {
                var types = $localStorage['ExpenseTypes'];
                for (var i = 0; i < types.length; i++) {
                    if (types[i].Id === Id)
                        return types[i];
                }
            },
            CountryById: function (Id) {
                var countries = $localStorage['Countries'];
                for (var i = 0; i < countries.length; i++) {
                    if (countries[i].Id === Id)
                        return countries[i];
                }
            },
            ExpenseDocumentById: function (Id) {
                var types = $localStorage['ExpenseDocumentTypes'];
                for (var i = 0; i < types.length; i++) {
                    if (types[i].Id === Id)
                        return types[i];
                }
            },

			loadCurrenciesForDate : loadCurrenciesForDate,

            reload: reload
        };
        return service;


        function reload() {
            return $http
                .get('../api/dictionaries')
                .then(function (response) {
                    console.log(response);

                    $localStorage['Countries'] = response.data.Countries;
                    $localStorage['VehicleTypes'] = response.data.VehicleTypes;
                    $localStorage['ExpenseTypes'] = response.data.ExpenseTypes;
                    $localStorage['ExpenseDocumentTypes'] = response.data.ExpenseDocumentTypes;
                    $localStorage['Currencies'] = response.data.Currencies;
                });
        }

        function loadCurrenciesForDate(date) {
        	return $http.post('api/currencies/forDate', '"' + date + '"').then(
                   function (response) {
                   		return response.data;                   	
                   },
                 function (response) {
                 	console.log(response);
                 	return null;
                 });        	
        }
    }
})();
