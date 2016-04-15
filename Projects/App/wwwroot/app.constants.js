(function () {
	'use strict';

	var serverUrl = '';
	if (window.location.hostname == 'localhost') {
		serverUrl = window.location.protocol + "//" + window.location.host + '/';
	} else {
		serverUrl = window.location.protocol + "//" + window.location.host + '/' + $.grep(window.location.pathname.split('/'), function (segment) { return segment != ''; }).join('/') + '/';
	}

    angular
        .module('app')
        .constant('appSettings', {
        	tokenName: 'delegacje-token',
        	baseUrl: serverUrl
        });
})();
