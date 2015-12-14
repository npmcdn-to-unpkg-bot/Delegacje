define(['durandal/app', 'knockout', 'jquery', 'services/dictionaries', 'factories/reportFactory', 'factories/expensesFactory', 'views/modals/expense'], function (app, ko, $, dictionaries, fReport, fExpense, mExpense) {

    var vm = {
        //report data
        report: null,

        //dictiopnaries
        VehicleTypes: dictionaries.VehicleTypes,

        //actions
        AddExpense: function() {
            app.showDialog(mExpense, null).then(function (dialogResult) {
                if (!dialogResult)
                    return;

                vm.report.Expenses.push(dialogResult);
            });
        },
        RemoveExpense: function(data) {
            vm.report.Expenses.remove(data);
        },

        //model lifecycle
        activate: function () {
            var blankReport = fReport.create();
            vm.report = blankReport;
        }
        //attached: function () {
        ////    $('input#input_text, textarea#textarea1').characterCounter();
        //},
        //canDeactivate: function () {
        //    //the router's activator calls this function to see if it can leave the screen
        //    //return app.showMessage('Are you sure you want to leave this page?', 'Navigate', ['Yes', 'No']);
        //}
    };

    return vm;

});