define(['knockout', 'services/localStorage', 'services/api'], function (ko, localStorage, api) {
    var vm = {
        Countries: localStorage.read("Countries"),
        VehicleTypes: localStorage.read("VehicleTypes"),
        ExpenseTypes: localStorage.read("ExpenseTypes"),
        ExpenseDocumentTypes: localStorage.read("ExpenseDocumentTypes"),
        MealTypes: localStorage.read("MealTypes"),

        Reload: function () {
            return api.get(api.url.Dictionaries()).then(function (result) {
                localStorage.save("Countries", result.Countries);
                localStorage.save("VehicleTypes", result.VehicleTypes);
                localStorage.save("ExpenseTypes", result.ExpenseTypes);
                localStorage.save("ExpenseDocumentTypes", result.ExpenseDocumentTypes);
                localStorage.save("MealTypes", result.MealTypes);
            });
        }
    }

    return vm;
});
