define(['knockout', 'services/mileageAllowanceFactory'], function (ko, fMileage) {
    function BusinessTrip() {
        this.Id = ko.observable();
        this.Title = ko.observable('');
        this.Date = ko.observable('');
        this.BusinessReason = ko.observable('');
        this.BusinessPurpose = ko.observable('');
        this.Notes = ko.observable('');
        this.UserId = ko.observable();
        this.Expenses = ko.observableArray();
        this.MileageAllowance = fMileage.create();
        this.Subsistences = ko.observable('');
    }

    var vm = {
        create: function () { return new BusinessTrip(); }
    }

    return vm;
});