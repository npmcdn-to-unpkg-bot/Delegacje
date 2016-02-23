(function () {
    'use strict';

    angular
        .module('app')
        .config(exceptionConfig);

    exceptionConfig.$inject = ['$provide'];

    function exceptionConfig($provide) {
        $provide.decorator('$exceptionHandler', extendExceptionHandler);
    }

    extendExceptionHandler.$inject = ['$delegate', '$injector'];

    function extendExceptionHandler($delegate, $injector) {
        return function(exception, cause) {
            $delegate(exception, cause);

            var toastr = $injector.get('toastr');

            var msg = exception.message || exception['error_description'] || exception.error;

            toastr.error(msg);
        };
    }

})();

