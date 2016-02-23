(function() {
    'use strict';

    angular
        .module('app')
        .directive('contextMenu', contextMenu);

    contextMenu.$inject = ['$document', "$parse"];
    
    function contextMenu($document, $parse) {
        var directive = {
            link: link,
            scope: { contextMenu: '&' },
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attr, controller) {

            var documentClickHandler = function (event) {
                var eventOutsideTarget = (element[0] !== event.target) && (0 === element.find(event.target).length);
                if (eventOutsideTarget) {
                    scope.$apply(function () {
                        scope.contextMenu();
                    });
                }
            };

            $document.on("click", documentClickHandler);
            scope.$on("$destroy", function () {
                $document.off("click", documentClickHandler);
            });
        }
    }

})();