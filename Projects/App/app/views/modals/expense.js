define(['plugins/dialog', 'knockout', 'jquery', 'services/dictionaries', 'factories/expensesFactory'], function (dialog, ko, $, dictionaries, fExpense) {

    var vm = {
        //report data
        expense: null,

        //dictiopnaries
        ExpenseTypes: dictionaries.ExpenseTypes,
        Countries: dictionaries.Countries,
        DocumentTypes: dictionaries.DocumentTypes,

        //actions
        Confirm: function () {
            dialog.close(vm, vm.expense);
        },
        Cancel: function () {
            dialog.close(vm, null);
        },

        //model lifecycle
        activate: function () {
            var blankExpense = fExpense.create();
            vm.expense = blankExpense;
        }
        //attached: function () {
        //    //    $('input#input_text, textarea#textarea1').characterCounter();
        //},
        //canDeactivate: function () {
        //    //the router's activator calls this function to see if it can leave the screen
        //    //return app.showMessage('Are you sure you want to leave this page?', 'Navigate', ['Yes', 'No']);
        //}
    };

    return vm;

});