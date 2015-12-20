requirejs.config({
    paths: {
        'text': '../lib/require/text',
        'durandal':'../lib/durandal/js',
        'plugins' : '../lib/durandal/js/plugins',
        'transitions' : '../lib/durandal/js/transitions',
        'knockout': '../lib/knockout/knockout-3.1.0',
        'jquery': '../lib/jquery/jquery-1.9.1'
    }
});

define(['durandal/system', 'durandal/app', 'durandal/viewLocator', 'services/dictionaries'], function (system, app, viewLocator, dictionaries) {
    //>>excludeStart("build", true);
    system.debug(true);
    //>>excludeEnd("build");

    app.title = 'Delegacje';

    app.configurePlugins({
        router:true,
        dialog: true
    });

    app.start().then(function() {
        viewLocator.useConvention('views', 'views');

        dictionaries.Reload().then(function (result) {
            app.setRoot('views/shell');
        })
    });
});