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
            MealTypes: $localStorage['MealTypes'],
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
                    $localStorage['MealTypes'] = response.data.MealTypes;
                });
        }
    }
})();
