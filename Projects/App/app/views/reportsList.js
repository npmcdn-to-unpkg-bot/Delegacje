define(['knockout'], function (ko) {

    var vm = {
        IsInitialized: ko.observable(false),
        Reports: ko.observableArray(),

        activate: function () {

        }
    };

    return vm;
});