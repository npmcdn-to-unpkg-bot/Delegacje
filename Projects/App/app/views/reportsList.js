define(['knockout', 'services/api'], function (ko, api) {

    var vm = {
        IsInitialized: ko.observable(false),
        Reports: ko.observableArray(),

        activate: function () {
            api.get(api.url.BusinessTripsForUser())
                .then(function (result) {
                    vm.Reports(result);
                })
                .always(function () {
                    vm.IsInitialized(true);
                });
        }
    };

    return vm;
});