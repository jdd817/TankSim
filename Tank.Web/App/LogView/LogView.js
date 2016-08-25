angular.module("Tank.Web")
    .directive("logView", [
        function () {
            return {
                restrict: 'E',
                templateUrl: $.url('app/LogView/LogView.html'),
                scope: {
                    log: "="
                },
                link: function ($scope) {
                }
            };
        }]);