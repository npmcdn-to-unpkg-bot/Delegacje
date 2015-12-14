define(['knockout'], function (ko) {
    function Expense() {
        this.Type = ko.observable();
        this.Date = ko.observable('');
        this.City = ko.observable('');
        this.Amount = ko.observable('');
        this.Country = ko.observable();
        this.Document = ko.observable();
        this.CurrencyCode = ko.observable('');
        this.ExchangeRate = ko.observable('');
        this.VATRate = ko.observable('');
        this.Notes = ko.observable('');
    }

    var vm = {
        create: function () { return new Expense(); }
    }

    return vm;
});