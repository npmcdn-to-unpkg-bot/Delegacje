(function() {
    'use strict';

    angular
        .module('app')
        .directive('datepickerDirective', datepickerDirective);

    datepickerDirective.$inject = ['$window'];
    
    function datepickerDirective($window) {

        var directive = {
            link: link,
            restrict: 'A',
            scope: {
                ngModel: '='
            }
        };
        return directive;

        function link(scope, element, attrs) {

            rome.moment().locale('pl');

            var date = attrs.romeDate || 'true';
            var time = attrs.romeTime || 'false';
            var weekstart = attrs.romeWeekstart || 1;
            var min = attrs.romeMin || null;
            var max = attrs.romeMax || null;
            var dateValidator = attrs.romeDateValidator || null;
            var beforeEq = attrs.romeBeforeEq || null;
            var afterEq = attrs.romeAfterEq || null;

            var config = {
                timeInterval: 3600,
                date: date === 'true',
                time: time === 'true',
                weekStart: parseInt(weekstart)
            }
            config.inputFormat = config.time ? 'DD/MM/YYYY HH:mm' : 'DD/MM/YYYY';

            if (min) config.min = min;
            if (max) config.max = max;
            if (dateValidator) {
                config.dateValidator = dateValidator;
            }
            else {
                if (beforeEq) {
                    var target = angular.element(document.getElementById(beforeEq));
                    config.dateValidator = rome.val.beforeEq(target[0]);
                }
                if (afterEq) {
                    var target = angular.element(document.getElementById(afterEq));
                    config.dateValidator = rome.val.afterEq(target[0]);
                }
            }

            var rome_instance = rome(element[0], config);

            //watch plugin changes and update model data
            rome_instance.on('data', function (value) {
                scope.$apply(function () {
                    scope.ngModel = value;
                });
            });

            //watch model changes and update plugin
            scope.$watch('ngModel', function (value) {
                if (value) {
                    rome_instance.setValue(value);
                }
            }, true);
        }
    }

})();