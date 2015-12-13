define(['knockout'], function (ko) {

    var vm = {
        VehicleTypes: [
            { Name: 'Motocykl', Rate: 1.5 },
            { Name: 'Samochód poj. 1', Rate: 3.0 },
            { Name: 'Samochód poj. 1', Rate: 5.0 }
        ],

        ExpenseTypes: [
            { Name: 'Expense 1', Id: 1 }
        ],

        Countries: [
            { Id: 1, Name: 'Polska', CurrencyName: 'PLN', CurrencyCode: 'PLN' },
            { Id: 2, Name: 'UK', CurrencyName: 'GBP', CurrencyCode: 'GBP' }
        ],

        DocumentTypes: [
            { Name: 'Type 1', Id: 1 }
        ]
    }

    return vm;
});