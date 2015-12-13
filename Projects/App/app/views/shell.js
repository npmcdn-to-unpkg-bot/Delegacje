define(['plugins/router'], function (router) {
    return {
        router: router,
        activate: function () {
            router.map([
                { route: '', title: 'Nowy Raport', moduleId: 'views/report' }
            ]).buildNavigationModel();
            
            return router.activate();
        }
    };
});