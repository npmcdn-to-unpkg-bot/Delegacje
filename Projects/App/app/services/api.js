define(['durandal/app', 'knockout', 'jquery', 'services/urls'],
function (app, ko, $, urls) {

    var vm = {
        url: urls,
        token: ko.observable(),
        activeRequests: ko.observableArray(),

        callApi: function (methodUrl, jsonData, httpVerb) {
            var requestConstructor = {
                type: httpVerb,
                data: jsonData ? jsonData + "" : jsonData,
                url: methodUrl,
            };

            if (httpVerb != 'GET') {
                requestConstructor.contentType = 'application/json';
            }

            if (vm.token()) {
                requestConstructor.beforeSend = function (request) {
                    request.setRequestHeader("Authorization", "Bearer " + vm.token());
                };
            }

            var request = $.ajax(requestConstructor);
            request.url = requestConstructor.url;

            vm.activeRequests.push(request);
            request.fail(function (error) {
                vm.activeRequests.pop();

                if (error.status != undefined && error.status == 401) {
                    app.trigger('401');
                }
            });
            request.done(function () {
                vm.activeRequests.pop();
            })

            return request;
        },

        get: function (methodUrl) {
            if (arguments.length == 1) {
                return $.when(vm.callApi(methodUrl, undefined, 'GET'));
            } else {
                var calls = [];
                for (var i = 0; i < arguments.length; i++) {
                    calls.push(vm.callApi(arguments[i], undefined, 'GET'));
                }

                return $.when.apply(this, calls);
            }
        },

        post: function (methodUrl, jsonData) {
            return $.when(vm.callApi(methodUrl, jsonData, 'POST'));
        }


    }

    return vm;
});


