define(['plugins/router'], function (router) {
    return {
        router: router,
        activate: function () {
            router.map([
                { route: '', title: 'Nowy Raport', moduleId: 'views/report' },
                { route: 'MojeRaporty', title: 'Moje Raporty', moduleId: 'views/reportsList' }
            ]).buildNavigationModel();
            
            return router.activate();
        }
    };
});