define(['knockout', 'mapping'],  function (ko, mapping) {

    return {
        save: function (key, value) {
            localStorage[key] = ko.toJSON(value);
        },
        read: function (key) {
            return mapping.fromJSON(localStorage[key]);
        },
        remove: function (key) {
            localStorage.removeItem(key);
        }
    };
});