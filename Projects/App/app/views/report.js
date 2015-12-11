define(['knockout'], function (ko) {

    return {
        activate: function () {
        },
        attached: function () {
        //    $('input#input_text, textarea#textarea1').characterCounter();
        },
        canDeactivate: function () {
            //the router's activator calls this function to see if it can leave the screen
            //return app.showMessage('Are you sure you want to leave this page?', 'Navigate', ['Yes', 'No']);
        }
    };

});