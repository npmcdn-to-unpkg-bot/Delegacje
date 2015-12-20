define(['knockout'],  function (ko) {

    return {
        save: function (key, value) {
            localStorage[key] = ko.toJSON(value);
        },
        read: function (key) {
            return ko.toJS(localStorage[key]);
        },
        remove: function (key) {
            localStorage.removeItem(key);
        }
    };
});