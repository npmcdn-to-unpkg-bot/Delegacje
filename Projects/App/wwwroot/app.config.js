(function () {
    'use strict';

    angular
        .module('app')
        .config(config)
        .run(run);

    config.$inject = [
        '$stateProvider', '$urlRouterProvider', '$httpProvider', '$logProvider', 'appSettings'
    ];

    function config($stateProvider, $urlRouterProvider, $httpProvider, $logProvider, appSettings) {
        $stateProvider
            .state('login', {
                url: '/login',
                templateUrl: 'wwwroot/authentication/login.template.html',
                controller: 'LoginController',
                controllerAs: 'vm'
            })
			.state('register', {
				url: '/register',
				templateUrl: 'wwwroot/authentication/register.template.html',
				controller: 'RegisterController',
				controllerAs: 'vm'
			})
            .state('landing', {
                url: '/landing',
                controller: 'LandingController',
                controllerAs: 'vm',
                templateUrl: 'wwwroot/system/landing.html',
            })
            .state('report-new', {
                url: '/report/new',
                controller: 'ReportController',
                controllerAs: 'vm',
                templateUrl: 'wwwroot/report/report.template.html'
            });

        $urlRouterProvider.otherwise('/landing');

        $logProvider.debugEnabled(appSettings.debugMode);

        $httpProvider.interceptors.push('httpInterceptor');
    }

    run.$inject = [ 'dictionariesService', 'userReportsService'];

    function run(dictionariesService, userReportsService) {
        dictionariesService.reload();
        userReportsService.reload();
    }

})();
