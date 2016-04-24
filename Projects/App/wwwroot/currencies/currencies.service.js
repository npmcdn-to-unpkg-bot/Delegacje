(function () {
    'use strict';

    angular
        .module('app')
        .factory('currenciesService', currenciesService);

    currenciesService.$inject = ['appSettings', '$http'];

    /* @ngInject */
    function currenciesService(appSettings, $http) {

        var service = {
            getExchangeRate: getExchangeRate
        };
        return service;

        function getExchangeRate(currencyCode, date) {
            return $http.post(appSettings.baseUrl + 'api/currencies/forCodeAndDate', JSON.stringify({ currencyCode: currencyCode, date: date })).then(
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
