define( function () {

    return {
        save: function (key, value) {
            localStorage[key] = value;
        },
        read: function (key) {
            return localStorage[key];
        },
        remove: function (key) {
            localStorage.removeItem(key);
        }
    };
});