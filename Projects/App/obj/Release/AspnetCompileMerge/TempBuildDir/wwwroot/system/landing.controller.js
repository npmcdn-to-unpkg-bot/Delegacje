(function () {
    'use strict';

    angular
         .module('app')
         .controller('LandingController', LandingController);

    LandingController.$inject = ['userReportsService', 'ngDialog', 'authenticationService', '$state'];

    function LandingController(userReportsService, ngDialog, authenticationService, $state) {
        var clickedReport = null;

        var vm = this;
        vm.reports = userReportsService.reports;

        vm.popupVisible = false;
        vm.showPopup = function (event, report) {
            clickedReport = report;

            var targetTag = angular.element(document.querySelector('#table-menu'));
            targetTag.css({
                position: "fixed",
                display: "block",
                left: event.clientX - 290 + 'px',
                top: event.clientY + 10 + 'px'
            });

            vm.popupVisible = true;
        };
        vm.hidePopup = function () {
            vm.popupVisible = false;
        };

        vm.remove = function () {
            var warningDialog = ngDialog.open({
                template: 'wwwroot/report/popups/remove.warning.template.html'
            });

            warningDialog.closePromise.then(function (data) {
                if (data.value === true) {
                    userReportsService.remove(clickedReport.Id);
                }
            });
        };
        vm.copy = function () {
            userReportsService.copy(clickedReport.Id).then(function (result) {
                vm.reports().push(result.data);
            });
        };
        vm.edit = function () {
            $state.go('report-edit', { reportId: clickedReport.Id });
        };
        vm.print = function () {
            userReportsService.print(clickedReport.Id);
        };

        userReportsService.reload();

        if (!authenticationService.isAuthenticated()) {
            $state.go('login');
        }
    }
})();
