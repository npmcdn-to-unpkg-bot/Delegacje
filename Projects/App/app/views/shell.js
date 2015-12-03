define(['plugins/router'], function (router) {
    return {
        router: router,
        activate: function () {
            router.map([
                { route: '', title:'Welcome', moduleId: 'views/report_details', nav: true }
            ]).buildNavigationModel();
            
            return router.activate();
        }
    };
});