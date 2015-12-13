define(['knockout'], function (ko) {
    function MileageAlloawnce() {
        var model = this;

        model.Type = ko.observable();
        model.Date = ko.observable('');
        model.Distance = ko.observable('');
        model.Notes = ko.observable('');

        model.Amount = ko.computed(function () {
            if (model.Type() == undefined || model.Distance() == '')
                return '';

            return model.Type().Rate * model.Distance();
        })
    }

    var vm = {
        create: function () { return new MileageAlloawnce(); }
    }

    return vm;
});