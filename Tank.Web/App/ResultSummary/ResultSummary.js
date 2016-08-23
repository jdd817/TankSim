angular.module("Tank.Web")
    .directive("resultSummary", [
        function () {
            return {
                restrict: 'E',
                templateUrl: $.url('app/ResultSummary/ResultSummary.html'),
                scope: {
                    summary: "="
                },
                link: function ($scope) {
                }
            };
        }]);