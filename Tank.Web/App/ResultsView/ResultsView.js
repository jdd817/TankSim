angular.module("Tank.Web")
    .directive("resultsView", [
        function () {
            return {
                restrict: 'E',
                templateUrl: $.url('app/ResultsView/ResultsView.html'),
                scope: {
                },
                link: function ($scope) {
                }
            };
        }]);