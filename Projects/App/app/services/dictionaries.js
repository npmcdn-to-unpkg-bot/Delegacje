//define(['knockout', 'services/localStorage', 'services/ichorApi'], function (ko, localStorage, api) {
//    var vm = {
//        languageId: ko.observable(''),
//        ProductCategories: function (targetProperty) { ReadDictionary("ProductCategories", targetProperty); },
//        ProductionMethods: function (targetProperty) { ReadDictionary("ProductionMethods", targetProperty); },
//        Languages: function (targetProperty) { ReadDictionary("Languages", targetProperty); },
//        Countries: function (targetProperty) { ReadDictionary("Countries", targetProperty); },
//        Currencies: function (targetProperty) { ReadDictionary("Currencies", targetProperty); }
//    }

//    var validCacheDaysCount = 7;

//    function daydiff(first, second) {
//        return (second - first) / (1000 * 60 * 60 * 24);
//    };

//    function GetDictionaryUrl(dictionaryCode) {
//        switch (dictionaryCode) {
//            case "ProductCategories":
//                return api.url.Dictionary_ProductCategories();
//            case "ProductionMethods":
//                return api.url.Dictionary_ProductionMethods();
//            case "Languages":
//                return api.url.Dictionary_Languages();
//            case "Countries":
//                return api.url.Dictionary_Countries();
//            case "Currencies":
//                return api.url.Dictionary_Currencies();
//            default:
//                return null;
//        }
//    }

//    function ReadDictionary(dictionaryCode, targetProperty) {
//        //check if dictonary with this code exists in local storage
//        var dictionaryKey = dictionaryCode + vm.languageId();
//        var localStorageValue = localStorage.read(dictionaryKey);

//        //we have something in local sotrage, cool
//        if (localStorageValue) {
//            //parse to JS
//            localStorageValue = $.parseJSON(localStorageValue);

//            //now check if it's not too old
//            var valueDate = new Date(localStorageValue.Date);
//            var currentDate = new Date();
//            var daysPassed = daydiff(valueDate, currentDate);

//            if (daysPassed <= validCacheDaysCount) {
//                //awesome, it's there and it's not too old so we can fill up the target property and leave
//                targetProperty(localStorageValue.Value);
//                return;
//            }
//        }

//        //no local storage value was found of the one that's in there is too old
//        //we need to load it again
//        var url = GetDictionaryUrl(dictionaryCode);
//        var promise = api.get(url);

//        promise.done(function (result) {
//            targetProperty(result);

//            var valueJson = ko.toJSON({
//                Date: new Date(),
//                Value: result
//            });

//            localStorage.save(dictionaryKey, valueJson);
//        });
//    }

//    return vm;
//});






define(['knockout'], function (ko) {

    var vm = {
        VehicleTypes: [],

        ExpenseTypes: [],

        Countries: [],

        DocumentTypes: []
    }

    return vm;
});