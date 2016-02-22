(function () {
    'use strict';

    angular
        .module('app')
        .factory('currenciesService', currenciesService);

    currenciesService.$inject = ['$http'];

    /* @ngInject */
    function currenciesService($http) {

        var service = {
            getExchangeRate: getExchangeRate
        };
        return service;

        function getExchangeRate(currencyCode, date) {
            return $http.get('api/currencies/' + currencyCode + '/' + date).then(
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
