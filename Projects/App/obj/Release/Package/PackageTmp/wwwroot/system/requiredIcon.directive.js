(function() {
    'use strict';

    angular
        .module('app')
        .directive('requiredIcon', requiredIcon);

    requiredIcon.$inject = ['$compile'];
    
    function requiredIcon($compile) {
        var validTemplate = '<div class="icon-valid"></div>';
        var invalidTemplate = '<div class="icon-invalid"></div>';

        var getTemplate = function (v) {
            var template = '';

            if (v === '' || v === false || v === null || v === undefined)
                return invalidTemplate;
            else
                return validTemplate;
        }

        var linker = function (scope, element, attrs) {
            scope.$watch('content', function () {
                element.html(getTemplate(scope.content));
                $compile(element.contents())(scope);
            });
        }

        return {
            restrict: "E",
            link: linker,
            scope: {
                content: '='
            }
        };
    }

})();